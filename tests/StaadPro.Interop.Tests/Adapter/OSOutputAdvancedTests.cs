using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using NUnit.Framework;
using PackageVerification;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Output;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Output;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSOutputAdvancedTests
    {
        private static readonly string[] Names = { "GetMemberSteelDesignResults", "GetBucklingFactorCount", "GetBucklingModeDisplacements", "GetRelativeMemberDisplacement", "GetAbsoluteMemberDisplacement", "GetPhysicalMemberEndForces", "GetMemberAxialForceExtrema", "GetMemberForcesAtDistance", "GetPhysicalMemberForcesAtDistance", "GetMemberDeflectionAtDistance", "GetMatInfluenceAreas", "GetBasePressures", "GetSolidNormalStresses", "GetSolidShearStresses", "GetSolidPrincipalStresses", "GetNonlinearLoadStep", "GetSteelDesignParameterBlockCount", "GetExtractedModeCount", "GetModeFrequency", "GetModalNodeDisplacements", "GetModalMassParticipationFactors", "GetTimeHistoryIntegrationInfo", "GetTimeHistoryResponseAtTime", "GetTimeHistoryResponse", "GetMultipleSteelDesignResultAvailability" };
        private static readonly string[] NativeNames = { "GetMemberSteelDesignResults", "GetNoOfBucklingFactors", "GetBucklingModeDisplacementAtNode", "GetIntermediateMemberTransDisplacements", "GetIntermediateMemberAbsTransDisplacements", "GetPMemberEndForces", "GetMinMaxAxialForce", "GetIntermediateMemberForcesAtDistance", "GetPMemberIntermediateForcesAtDistance", "GetIntermediateDeflectionAtDistance", "GetMatInfluenceAreas", "GetBasePressures", "GetAllSolidNormalStresses", "GetAllSolidShearStresses", "GetAllSolidPrincipalStresses", "GetNLLoadStep", "GetSteelDesignParameterBlockCount", "GetNoOfModesExtracted", "GetModeFrequency", "GetModalDisplacementAtNode", "GetModalMassParticipationFactors", "GetTimeHistoryIntegrationStepInfo", "GetTimeHistoryResponseAtTime", "GetTimeHistoryResponse", "IsMultipleMemberSteelDesignResultsAvailable" };
        private static readonly string[] ArrayNames = { "GetBucklingModeDisplacements", "GetRelativeMemberDisplacement", "GetAbsoluteMemberDisplacement", "GetPhysicalMemberEndForces", "GetMemberForcesAtDistance", "GetPhysicalMemberForcesAtDistance", "GetSolidNormalStresses", "GetSolidShearStresses", "GetSolidPrincipalStresses" };
        private static string NativeName(string name) => NativeNames[Array.IndexOf(Names, name)];
        private static OpenStaadWrapper Wrap(OutputAdvancedFixture.Native native, int major = 26) => new OpenStaadWrapper(new OutputAdvancedFixture.Root { Output = native, Major = major });
        private static object[] Args(string name) => typeof(IOSOutput).GetMethod(name).GetParameters().Select(p =>
            p.ParameterType == typeof(MemberEnd) ? (object)MemberEnd.End :
            p.ParameterType == typeof(ResultCoordinateSystem) ? ResultCoordinateSystem.Local :
            p.ParameterType == typeof(ResponseDegreeOfFreedom) ? ResponseDegreeOfFreedom.TranslationX :
            p.ParameterType == typeof(TimeHistoryResponseKind) ? TimeHistoryResponseKind.Displacement :
            p.ParameterType == typeof(IEnumerable<int>) ? new[] {17, 19} :
            p.ParameterType == typeof(double) ? (object)0.5 :
            p.Name.StartsWith("maximum", StringComparison.Ordinal) ? 100 :
            p.Name.StartsWith("native", StringComparison.Ordinal) ? 0 :
            p.Name == "loadCaseId" ? 41 : 17).ToArray();
        private static object Invoke(IOSOutput output, string name, object[] args = null)
        {
            try { return typeof(IOSOutput).GetMethod(name).Invoke(output, args ?? Args(name)); }
            catch (TargetInvocationException ex) { ExceptionDispatchInfo.Capture(ex.InnerException).Throw(); throw; }
        }
        private static object Property(object result,string name) => result.GetType().GetProperty(name).GetValue(result);
        private static void Response(OutputAdvancedFixture.Native n,string op,params object[] values) => n.Responses[op]=new Queue<object>(values);
        private static void Failed(object result,OutputQueryStatus status)
        { Assert.That(Property(result,"Status"),Is.EqualTo(status)); Assert.That(Property(result,"IsSuccessful"),Is.False); Assert.Throws<TargetInvocationException>(()=>Property(result,"Data")); }

        [TestCaseSource(nameof(Names))]
        public void QualifiedRouteUsesExpectedNativeMemberAndCallerThread(string name)
        {
            foreach(int major in new[] {24,25,26})
            {
                var n=new OutputAdvancedFixture.Native(); using(var w=Wrap(n,major))
                {
                    var result=Invoke(w.Output,name);
                    Assert.That(Property(result,"IsSuccessful"),Is.True,name);
                    string op=NativeName(name);
                    Assert.That(Property(result,"NativeFunction"),Is.EqualTo(op));
                    Assert.That(n.Calls,Is.EqualTo(name=="GetTimeHistoryResponse"||name=="GetTimeHistoryResponseAtTime"?
                        new[]{"GetTimeHistoryIntegrationStepInfo",op,"GetTimeHistoryIntegrationStepInfo"}:new[]{op}));
                    Assert.That(n.Threads.All(t=>t==Thread.CurrentThread.ManagedThreadId),Is.True);
                }
            }
        }
        [TestCaseSource(nameof(Names))]
        public void DisposedBindingCannotInvokeNativeMember(string name)
        {
            var n=new OutputAdvancedFixture.Native();var w=Wrap(n);var output=w.Output;w.Dispose();
            Assert.Throws<InvalidOperationException>(()=>Invoke(output,name)); Assert.That(n.Calls,Is.Empty);
        }
        [TestCaseSource(nameof(Names))]
        public void ReboundBindingDuringNativeCallCannotPublishData(string name)
        {
            var n=new OutputAdvancedFixture.Native();var root=new OutputAdvancedFixture.Root{Output=n};
            using(var w=new OpenStaadWrapper(root))
            {var output=w.Output;n.OnCall=op=>{n.OnCall=null;root.Output=new OutputAdvancedFixture.Native();w.RefreshComObjects();};
                Assert.Throws<InvalidOperationException>(()=>Invoke(output,name));}
        }
        [TestCaseSource(nameof(Names))]
        public void TransportExceptionsKeepIdentityWithoutRetry(string name)
        {
            var n=new OutputAdvancedFixture.Native();var error=new COMException("fixture transport");n.OnCall=op=>throw error;
            using(var w=Wrap(n))Assert.That(Assert.Throws<COMException>(()=>Invoke(w.Output,name)),Is.SameAs(error));
            Assert.That(n.Calls.Count,Is.EqualTo(1));
        }
        [TestCaseSource(nameof(ArrayNames))]
        public void ExactArrayShapeTypeAndWrittenValuesAreRequired(string name)
        {
            foreach(Func<string,Array,object> replacement in new Func<string,Array,object>[] {
                (role,a)=>null,(role,a)=>new double[a.Length+1],(role,a)=>new double[a.Length-1],
                (role,a)=>new double[1,a.Length],(role,a)=>new object[a.Length],(role,a)=>new int[a.Length],
                (role,a)=>new float[a.Length],(role,a)=>Enumerable.Repeat(double.NaN,a.Length).ToArray(),
                (role,a)=>Enumerable.Repeat(double.PositiveInfinity,a.Length).ToArray() })
            {
                var n=new OutputAdvancedFixture.Native{ReplaceArray=replacement};
                using(var w=Wrap(n))Assert.Throws<InvalidOperationException>(()=>Invoke(w.Output,name),name);
            }
        }
        [TestCaseSource(nameof(ArrayNames))]
        public void OwnedArrayValuesAcceptNonzeroNativeLowerBounds(string name)
        {
            var n=new OutputAdvancedFixture.Native{ReplaceArray=(role,a)=>{
                var v=Array.CreateInstance(typeof(double),new[]{a.Length},new[]{-4});
                for(int i=0;i<a.Length;i++)v.SetValue(i+1.25,i-4);return v;}};
            using(var w=Wrap(n))Assert.That(Property(Invoke(w.Output,name),"IsSuccessful"),Is.True);
        }
        [TestCaseSource(nameof(Names))]
        public void InvalidParametersNeverReachNativeMember(string name)
        {
            var n=new OutputAdvancedFixture.Native();using(var w=Wrap(n))
            {
                var parameters=typeof(IOSOutput).GetMethod(name).GetParameters();
                for(int i=0;i<parameters.Length;i++)
                {
                    var args=Args(name);var p=parameters[i];
                    args[i]=p.ParameterType==typeof(IEnumerable<int>)?null:
                        p.ParameterType==typeof(double)?(object)double.NaN:
                        p.ParameterType.IsEnum?Enum.ToObject(p.ParameterType,99):-1;
                    Assert.That(()=>Invoke(w.Output,name,args),Throws.InstanceOf<ArgumentException>(),name+" "+p.Name);
                }
                Assert.That(n.Calls,Is.Empty);
            }
        }
        [Test]
        public void CountReturnsAreDataAndNeverBooleanFlags()
        {
            foreach(string name in new[]{"GetBucklingFactorCount","GetExtractedModeCount","GetSteelDesignParameterBlockCount"})
            foreach(object raw in new object[]{0,3,(short)2,5L,true,false,1.0,1m,"3",-1,null,uint.MaxValue})
            {
                var n=new OutputAdvancedFixture.Native();Response(n,NativeName(name),raw);
                using(var w=Wrap(n))
                {
                    if(raw is int i&&i>=0||raw is short||raw is long)
                    {var r=Invoke(w.Output,name);Assert.That(Property(r,"Data"),Is.EqualTo(Convert.ToInt32(raw)));Assert.That(Property(r,"NativeReturnCode"),Is.EqualTo(Convert.ToInt32(raw)));Assert.That(Property(r,"NativeBooleanReturn"),Is.Null);}
                    else Assert.Throws<InvalidOperationException>(()=>Invoke(w.Output,name));
                }
            }
        }
        [Test]
        public void NativeFailurePrecedesMalformedOutputForFlagQueries()
        {
            var excluded=new[]{"GetBucklingFactorCount","GetExtractedModeCount","GetSteelDesignParameterBlockCount","GetNonlinearLoadStep","GetTimeHistoryIntegrationInfo","GetTimeHistoryResponse","GetTimeHistoryResponseAtTime","GetMatInfluenceAreas","GetMultipleSteelDesignResultAvailability"};
            foreach(string name in Names.Except(excluded))
            {
                var n=new OutputAdvancedFixture.Native{WriteOutputs=false};Response(n,NativeName(name),false);
                using(var w=Wrap(n)){var r=Invoke(w.Output,name);Failed(r,OutputQueryStatus.NativeFailure);Assert.That(Property(r,"NativeBooleanReturn"),Is.EqualTo(false));}
            }
        }
        [Test]
        public void UnwrittenScalarAndByValueArrayResultsCannotLookLikeZero()
        {
            foreach(string name in Names.Except(new[]{"GetBucklingFactorCount","GetExtractedModeCount","GetSteelDesignParameterBlockCount","GetNonlinearLoadStep","GetMultipleSteelDesignResultAvailability"}))
            {var n=new OutputAdvancedFixture.Native{WriteOutputs=false};using(var w=Wrap(n))Assert.Throws<InvalidOperationException>(()=>Invoke(w.Output,name),name);}
        }
        [Test]
        public void FrequencyAndExtremaRejectMalformedAndInconsistentValues()
        {
            foreach(object value in new object[]{null,true,"2",1m,1,double.NaN,double.PositiveInfinity,-0.1})
            {var n=new OutputAdvancedFixture.Native();n.Scalars["frequency"]=value;using(var w=Wrap(n))Assert.Throws<InvalidOperationException>(()=>w.Output.GetModeFrequency(0));}
            foreach(var role in new[]{"minimum","minimumPosition","maximum","maximumPosition"})
            {var n=new OutputAdvancedFixture.Native();n.Scalars[role]=double.NaN;using(var w=Wrap(n))Assert.Throws<InvalidOperationException>(()=>w.Output.GetMemberAxialForceExtrema(17,41));}
            var bad=new OutputAdvancedFixture.Native();bad.Scalars["minimum"]=20.0;using(var w=Wrap(bad))Assert.Throws<InvalidOperationException>(()=>w.Output.GetMemberAxialForceExtrema(17,41));
        }
        [TestCase(0.0,OutputQueryStatus.Success)]
        [TestCase(0.75,OutputQueryStatus.Success)]
        [TestCase(-1.0,OutputQueryStatus.NativeFailure)]
        [TestCase(-8002.0,OutputQueryStatus.NotFound)]
        [TestCase(-9911.0,OutputQueryStatus.ResultsUnavailable)]
        public void NonlinearLoadStepKeepsDocumentedSentinels(double raw,OutputQueryStatus expected)
        {
            var n=new OutputAdvancedFixture.Native();Response(n,"GetNLLoadStep",raw);
            using(var w=Wrap(n)){var r=w.Output.GetNonlinearLoadStep(41);Assert.That(r.Status,Is.EqualTo(expected));if(r.IsSuccessful)Assert.That(r.Data,Is.EqualTo(raw));else Failed(r,expected);}
        }
        [Test]
        public void NarrowAvailabilityAndBucklingFlagsDoNotTreatMinusOneAsTrue()
        {
            foreach(object raw in new object[]{false,0,true,1,-1,2,1.0,null})
            {
                var n=new OutputAdvancedFixture.Native();Response(n,"IsMultipleMemberSteelDesignResultsAvailable",raw);Response(n,"GetBucklingModeDisplacementAtNode",raw);
                using(var w=Wrap(n))
                {
                    if(raw is bool||raw is int i&&(i==0||i==1))
                    {Assert.That(w.Output.GetMultipleSteelDesignResultAvailability().Data,Is.EqualTo(raw is bool b?b:(int)raw==1));Assert.That(w.Output.GetBucklingModeDisplacements(0,17).IsSuccessful,Is.EqualTo(raw is bool c?c:(int)raw==1));}
                    else{Assert.Throws<InvalidOperationException>(()=>w.Output.GetMultipleSteelDesignResultAvailability());Assert.Throws<InvalidOperationException>(()=>w.Output.GetBucklingModeDisplacements(0,17));}
                }
            }
        }
        [Test]
        public void SteelDesignDecisionPreservesUnknownStatusesAndOnlyNativeActions()
        {
            foreach(object status in new object[]{" PASS ","fail","UNKNOWN",0,1,-1,0.5,true,false})
            {
                var n=new OutputAdvancedFixture.Native();n.Scalars["designStatus"]=status;
                using(var w=Wrap(n))
                {
                    var d=w.Output.GetMemberSteelDesignResults(17).Data;
                    Assert.That(d.DesignStatus,Is.EqualTo(status is string s&&s.Trim().ToUpperInvariant()=="PASS"?SteelDesignStatus.Passed:status is string t&&t.Trim().ToUpperInvariant()=="FAIL"?SteelDesignStatus.Failed:SteelDesignStatus.Unknown));
                    Assert.That(d.NativeStatusText,Is.EqualTo(status as string));
                    Assert.That(d.NativeStatusBoolean,Is.EqualTo(status is bool b?(bool?)b:null));
                    Assert.That(new[]{d.DesignForces.Fx,d.DesignForces.My,d.DesignForces.Mz},Is.EqualTo(new[]{1.25,2.25,3.25}));
                    Assert.That(d.CriticalLoadCaseId,Is.EqualTo(1));Assert.That(d.CriticalSection,Is.EqualTo(.5));Assert.That(d.UtilizationRatio,Is.EqualTo(.75));
                    Assert.That(d.DesignForces.GetType().GetProperty("Fy"),Is.Null);
                }
            }
        }
        [Test]
        public void NativeCoordinatesAndIndicesAreForwardedWithoutInventedConversion()
        {
            var n=new OutputAdvancedFixture.Native();using(var w=Wrap(n))
            {
                var physical=w.Output.GetPhysicalMemberEndForces(17,MemberEnd.End,41,ResultCoordinateSystem.Global);
                Assert.That(n.Arguments.Last(),Is.EqualTo(new object[]{17,1,41,1}));Assert.That(physical.MemberEnd,Is.EqualTo(MemberEnd.End));
                w.Output.GetSolidNormalStresses(17,0,41);Assert.That(n.Arguments.Last(),Is.EqualTo(new object[]{17,0,41}));
                w.Output.GetModalNodeDisplacements(0,17);Assert.That(n.Arguments.Last(),Is.EqualTo(new object[]{0,17}));
                var relative=w.Output.GetRelativeMemberDisplacement(17,.5,41).Data;
                var absolute=w.Output.GetAbsoluteMemberDisplacement(17,.5,41).Data;
                Assert.That(n.Calls.Skip(n.Calls.Count-2),Is.EqualTo(new[]{"GetIntermediateMemberTransDisplacements","GetIntermediateMemberAbsTransDisplacements"}));
                Assert.That(relative,Is.EqualTo(absolute));Assert.That(n.Arguments.Last(),Is.EqualTo(new object[]{17,.5,41}));
            }
        }
        private sealed class Once : IEnumerable<int>
        {
            public int Enumerations;public IEnumerator<int> GetEnumerator(){if(++Enumerations>1)throw new Exception("Enumerated twice");yield return 19;yield return 17;yield return 19;}IEnumerator IEnumerable.GetEnumerator()=>GetEnumerator();
        }
        [Test]
        public void MatAndBasePressureSnapshotSelectionPreserveOrderDuplicatesAndOwnedData()
        {
            var n=new OutputAdvancedFixture.Native();using(var w=Wrap(n))
            {
                var ids=new Once();var mat=w.Output.GetMatInfluenceAreas(ids).Data;Assert.That(ids.Enumerations,Is.EqualTo(1));
                Assert.That(mat.Select(x=>x.NodeId),Is.EqualTo(new[]{19,17,19}));
                Assert.That(mat.Select(x=>x.AreaYZ),Is.EqualTo(new[]{1.25,2.25,3.25}));
                var pressures=w.Output.GetBasePressures(41,new[]{19,17,19}).Data;Assert.That(pressures.Select(x=>x.GlobalX),Is.EqualTo(new[]{1.25,2.25,3.25}));
                foreach(Array array in n.Arrays.Values)for(int i=0;i<array.Length;i++)array.SetValue(99.0,i);
                Assert.That(mat[0].AreaYZ,Is.EqualTo(1.25));Assert.That(pressures[2].GlobalZ,Is.EqualTo(3.25));
                Assert.Throws<NotSupportedException>(()=>((IList<MatInfluenceArea>)mat).Clear());
                n.Calls.Clear();Assert.That(w.Output.GetMatInfluenceAreas(new int[0]).Data,Is.Empty);Assert.That(w.Output.GetBasePressures(41,new int[0]).Data,Is.Empty);Assert.That(n.Calls,Is.Empty);
                Assert.Throws<ArgumentException>(()=>w.Output.GetMatInfluenceAreas(new[]{1,2},1));
            }
        }
        [Test]
        public void MatZeroSuccessAndActualFalseHaveDifferentMeanings()
        {
            foreach(object raw in new object[]{0,true,false,-107,-108,1,-1,0.0})
            {
                var n=new OutputAdvancedFixture.Native();Response(n,"GetMatInfluenceAreas",raw);
                using(var w=Wrap(n))
                {
                    if(raw is bool||raw is int i&&(i==0||i==-107||i==-108))
                    {var r=w.Output.GetMatInfluenceAreas(new[]{17});Assert.That(r.IsSuccessful,Is.EqualTo(raw is bool b?b:(int)raw==0));}
                    else Assert.Throws<InvalidOperationException>(()=>w.Output.GetMatInfluenceAreas(new[]{17}));
                }
            }
        }
        [Test]
        public void RewrittenNativeSelectionCannotMisattributeNodeResults()
        {
            var n=new OutputAdvancedFixture.Native{RewriteInputIds=ids=>ids[0]=999};using(var w=Wrap(n))
            {Failed(w.Output.GetMatInfluenceAreas(new[]{17}),OutputQueryStatus.StateChanged);Failed(w.Output.GetBasePressures(41,new[]{17}),OutputQueryStatus.StateChanged);}
        }
        [TestCase(0,OutputQueryStatus.ResultsUnavailable)]
        [TestCase(-1,OutputQueryStatus.NativeFailure)]
        public void MissingIntegrationPreventsDependentNativeCalls(int count,OutputQueryStatus status)
        {
            var n=new OutputAdvancedFixture.Native{SampleCount=count,WriteOutputs=false};using(var w=Wrap(n))
            {Failed(w.Output.GetTimeHistoryIntegrationInfo(),status);Failed(w.Output.GetTimeHistoryResponse(17,ResponseDegreeOfFreedom.TranslationX,TimeHistoryResponseKind.Displacement),status);}
            Assert.That(n.Calls.All(x=>x=="GetTimeHistoryIntegrationStepInfo"),Is.True);
        }
        [Test]
        public void IntegrationAndSeriesKeepExactSampleCountAndEnforceAllocationBudget()
        {
            var n=new OutputAdvancedFixture.Native();using(var w=Wrap(n))
            {
                var r=w.Output.GetTimeHistoryResponse(17,ResponseDegreeOfFreedom.RotationZ,TimeHistoryResponseKind.Acceleration).Data;
                Assert.That(r.Samples.Select(s=>s.TimeSeconds),Is.EqualTo(new[]{0.0,.25,.5}));
                Assert.That(r.Samples.Select(s=>s.Value),Is.EqualTo(new[]{1.25,2.25,3.25}));
                Assert.That(r.Integration.LastSampleTimeSeconds,Is.EqualTo(.5));
                Assert.That(n.Arguments[1],Is.EqualTo(new object[]{0,17,6,2}));
                ((double[])n.Arrays["history"])[0]=99;Assert.That(r.Samples[0].Value,Is.EqualTo(1.25));
                n.Calls.Clear();Failed(w.Output.GetTimeHistoryResponse(17,ResponseDegreeOfFreedom.TranslationX,TimeHistoryResponseKind.Displacement,2),OutputQueryStatus.LimitExceeded);
                Assert.That(n.Calls,Is.EqualTo(new[]{"GetTimeHistoryIntegrationStepInfo"}));
                Assert.Throws<ArgumentOutOfRangeException>(()=>w.Output.GetTimeHistoryResponseAtTime(17,ResponseDegreeOfFreedom.TranslationX,TimeHistoryResponseKind.Displacement,.501));
                Assert.That(w.Output.GetTimeHistoryResponseAtTime(17,ResponseDegreeOfFreedom.TranslationX,TimeHistoryResponseKind.Displacement,0).Data.TimeSeconds,Is.Zero);
            }
        }
        [TestCase(0,OutputQueryStatus.ResultsUnavailable)]
        [TestCase(-1,OutputQueryStatus.NativeFailure)]
        public void TimeHistoryFlagsDoNotTreatMinusOneAsVariantTrue(int code,OutputQueryStatus expected)
        {
            var n=new OutputAdvancedFixture.Native();Response(n,"GetTimeHistoryResponse",code);Response(n,"GetTimeHistoryResponseAtTime",code);
            using(var w=Wrap(n))
            {Failed(w.Output.GetTimeHistoryResponse(17,ResponseDegreeOfFreedom.TranslationX,TimeHistoryResponseKind.Displacement),expected);
             Failed(w.Output.GetTimeHistoryResponseAtTime(17,ResponseDegreeOfFreedom.TranslationX,TimeHistoryResponseKind.Displacement,.25),expected);}
        }
        [Test]
        public void ChangingTimeGridCannotPublishMisalignedHistory()
        {
            foreach(bool changeCount in new[]{true,false})
            {
                var n=new OutputAdvancedFixture.Native();n.OnCall=op=>{if(op=="GetTimeHistoryResponse"||op=="GetTimeHistoryResponseAtTime"){if(changeCount)n.SampleCount=4;else n.Interval=.5;}};
                using(var w=Wrap(n))Failed(w.Output.GetTimeHistoryResponse(17,ResponseDegreeOfFreedom.TranslationX,TimeHistoryResponseKind.Displacement),OutputQueryStatus.StateChanged);
                n.SampleCount=3;n.Interval=.25;
                using(var w=Wrap(n))Failed(w.Output.GetTimeHistoryResponseAtTime(17,ResponseDegreeOfFreedom.TranslationX,TimeHistoryResponseKind.Displacement,.25),OutputQueryStatus.StateChanged);
            }
        }
        [Test]
        public void IntegrationRejectsNonpositiveOrNonfiniteIntervalsAndNonIntegralCounts()
        {
            foreach(double interval in new[]{0.0,-.25,double.NaN,double.PositiveInfinity,double.MaxValue})
            {var n=new OutputAdvancedFixture.Native{Interval=interval};using(var w=Wrap(n))Assert.Throws<InvalidOperationException>(()=>w.Output.GetTimeHistoryIntegrationInfo());}
            foreach(object count in new object[]{true,1.0,"3",-2,null})
            {var n=new OutputAdvancedFixture.Native();Response(n,"GetTimeHistoryIntegrationStepInfo",count);using(var w=Wrap(n))Assert.Throws<InvalidOperationException>(()=>w.Output.GetTimeHistoryIntegrationInfo());}
        }
    }
}
