using System;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using NUnit.Framework;
using PackageVerification;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Enums.Output;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSOutputSteelBlockTests
    {
        private static readonly string[] Names={"GetSteelDesignParameterBlockName", "GetMemberSteelUtilizationRatio", "GetMaximumMemberSteelUtilizationRatio", "GetMemberSteelDesignResults", "GetMinimumSteelDesignFailureRatio", "GetMaximumSteelDesignFailureRatio"};
        private static readonly string[] NativeNames={"GetSteelDesignParameterBlockNameByIndex", "GetMultipleMemberSteelDesignRatio", "GetMultipleMemberSteelDesignMaxRatio", "GetMultipleMemberSteelDesignResults", "GetMemberSteelDesignMinFailureRatio", "GetMemberSteelDesignMaxFailureRatio"};
        private static MethodInfo Method(string name)=>typeof(IOSOutput).GetMethods().Single(m=>m.Name==name && (name=="GetMemberSteelUtilizationRatio"||name=="GetMemberSteelDesignResults"?m.GetParameters().Length==2:true));
        private static object[] Args(string name)=>Method(name).GetParameters().Select(p=>p.ParameterType==typeof(string)?(object)" PARAMETER 1 ":p.Name=="parameterBlockIndex"?0:17).ToArray();
        private static object Invoke(IOSOutput output,string name,object[] args=null)
        {try{return Method(name).Invoke(output,args??Args(name));}catch(TargetInvocationException e){ExceptionDispatchInfo.Capture(e.InnerException).Throw();throw;}}
        private static object Value(object result,string name)=>result.GetType().GetProperty(name).GetValue(result);
        private static OpenStaadWrapper Wrap(OutputSteelBlockFixture.Native n,int major=26)=>new OpenStaadWrapper(new OutputSteelBlockFixture.Root{Output=n,Major=major});
        [TestCaseSource(nameof(Names))]
        public void ExactNativePacketPreservesMemberBlockAndCallerApartmentAcrossReviewedYears(string name)
        {
            foreach(int major in new[]{24,25,26})
            {
                var n=new OutputSteelBlockFixture.Native();using(var w=Wrap(n,major))
                {
                    object result=Invoke(w.Output,name);Assert.That(Value(result,"IsSuccessful"),Is.True);
                    Assert.That(n.Calls,Is.EqualTo(new[]{NativeNames[Array.IndexOf(Names,name)]}));
                    Assert.That(n.Threads,Is.EqualTo(new[]{Thread.CurrentThread.ManagedThreadId}));
                    bool named=Method(name).GetParameters().Any(p=>p.ParameterType==typeof(string));
                    Assert.That(Value(result,"ParameterBlockName"),Is.EqualTo(named?" PARAMETER 1 ":null));
                    Assert.That(Value(result,"EntityId"),Is.EqualTo(Method(name).GetParameters().Any(p=>p.Name=="memberId")?(int?)17:null));
                    Assert.That(Value(result,"NativeBooleanReturn"),Is.EqualTo(true));Assert.That(Value(result,"NativeReturnCode"),Is.Null);
                    if(named)Assert.That(n.Arguments.Single(),Is.EqualTo(new object[]{" PARAMETER 1 ",17}));
                }
            }
        }
        [TestCaseSource(nameof(Names))]
        public void FalsePrecedesUnwrittenOutputsAndDisallowsPayload(string name)
        {
            var n=new OutputSteelBlockFixture.Native{Flag=false,WriteOutputs=false};using(var w=Wrap(n))
            {object r=Invoke(w.Output,name);Assert.That(Value(r,"Status"),Is.EqualTo(OutputQueryStatus.NativeFailure));Assert.Throws<TargetInvocationException>(()=>Value(r,"Data"));}
        }
        [TestCaseSource(nameof(Names))]
        public void SuccessRequiresActuallyWrittenQualifiedOutputs(string name)
        {using(var w=Wrap(new OutputSteelBlockFixture.Native{WriteOutputs=false}))Assert.Throws<InvalidOperationException>(()=>Invoke(w.Output,name));}
        [TestCaseSource(nameof(Names))]
        public void DisposedAndReplacedBindingsCannotReturnResults(string name)
        {
            var n=new OutputSteelBlockFixture.Native();var root=new OutputSteelBlockFixture.Root{Output=n};
            using(var w=new OpenStaadWrapper(root))
            {
                var output=w.Output;n.OnCall=op=>{n.OnCall=null;root.Output=new OutputSteelBlockFixture.Native();w.RefreshComObjects();};
                Assert.Throws<InvalidOperationException>(()=>Invoke(output,name));Assert.That(n.Calls.Count,Is.EqualTo(1));
                var fresh=w.Output;w.Dispose();Assert.Throws<InvalidOperationException>(()=>Invoke(fresh,name));
            }
        }
        [TestCaseSource(nameof(Names))]
        public void OriginalTransportErrorsPropagateWithoutRetry(string name)
        {
            var n=new OutputSteelBlockFixture.Native();var failure=new COMException("transport");n.OnCall=op=>throw failure;
            using(var w=Wrap(n))Assert.That(Assert.Throws<COMException>(()=>Invoke(w.Output,name)),Is.SameAs(failure));
            Assert.That(n.Calls.Count,Is.EqualTo(1));
        }
        [TestCaseSource(nameof(Names))]
        public void UnknownFlagsAreRejectedRatherThanCoerced(string name)
        {
            foreach(object raw in new object[]{null,2,-2,1.0,1m,"true"})
            {using(var w=Wrap(new OutputSteelBlockFixture.Native{Flag=raw}))Assert.Throws<InvalidOperationException>(()=>Invoke(w.Output,name));}
        }
        [Test]
        public void NamesAndSelectorsAreValidatedBeforeNativeAccessWithoutNormalizingInput()
        {
            var n=new OutputSteelBlockFixture.Native();using(var w=Wrap(n))
            {
                Assert.Throws<ArgumentOutOfRangeException>(()=>w.Output.GetSteelDesignParameterBlockName(-1));
                foreach(string value in new[]{null,"","   ","x\0y"})
                {Assert.That(()=>w.Output.GetMemberSteelDesignResults(17,value),Throws.InstanceOf<ArgumentException>());Assert.That(()=>w.Output.GetMemberSteelUtilizationRatio(17,value),Throws.InstanceOf<ArgumentException>());}
                Assert.Throws<ArgumentOutOfRangeException>(()=>w.Output.GetMemberSteelUtilizationRatio(0,"BLOCK"));
                Assert.Throws<ArgumentOutOfRangeException>(()=>w.Output.GetMaximumMemberSteelUtilizationRatio(0));
                Assert.That(n.Calls,Is.Empty);
            }
        }
        [Test]
        public void RatiosRejectSingleBlockSentinelsCoercionAndNonfiniteValues()
        {
            foreach(string name in new[]{"GetMemberSteelUtilizationRatio","GetMaximumMemberSteelUtilizationRatio","GetMinimumSteelDesignFailureRatio","GetMaximumSteelDesignFailureRatio","GetMemberSteelDesignResults"})
            foreach(object ratio in new object[]{-999.0,-1.0,true,1,".5",1m,double.NaN,double.PositiveInfinity,null})
            {using(var w=Wrap(new OutputSteelBlockFixture.Native{Ratio=ratio}))Assert.Throws<InvalidOperationException>(()=>Invoke(w.Output,name),name);}
        }
        [Test]
        public void BlockPacketHasOnlyNativeFieldsAndDoesNotGuessUnknownDesignStatus()
        {
            foreach(string text in new[]{"PASS"," fail ","NOT CHECKED"})
            {
                var n=new OutputSteelBlockFixture.Native{Status=text};using(var w=Wrap(n))
                {
                    var d=w.Output.GetMemberSteelDesignResults(17," PARAMETER 1 ").Data;
                    Assert.That(d.NativeStatusText,Is.EqualTo(text));Assert.That(d.ParameterBlockName,Is.EqualTo(" PARAMETER 1 "));
                    Assert.That(d.DesignStatus,Is.EqualTo(text=="PASS"?SteelDesignStatus.Passed:text==" fail "?SteelDesignStatus.Failed:SteelDesignStatus.Unknown));
                    Assert.That(d.CriticalLoadCaseId,Is.EqualTo(41));
                    foreach(string unavailable in new[]{"CriticalSection","DesignForces","SlendernessRatio"})
                        Assert.That(d.GetType().GetProperty(unavailable),Is.Null);
                }
            }
        }
        [Test]
        public void BlockNamesAndDesignTextCannotUseNullOrUnwrittenSentinels()
        {
            foreach(object name in new object[]{null,1,true,""," ","bad\0name"})
            {using(var w=Wrap(new OutputSteelBlockFixture.Native{BlockName=name}))Assert.Throws<InvalidOperationException>(()=>w.Output.GetSteelDesignParameterBlockName(0));}
            using(var w=Wrap(new OutputSteelBlockFixture.Native{Status=1}))Assert.Throws<InvalidOperationException>(()=>w.Output.GetMemberSteelDesignResults(17,"BLOCK"));
        }
    }
}
