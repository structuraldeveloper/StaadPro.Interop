using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Linq;
using NUnit.Framework;
using PackageVerification;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Support;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Support;
namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSSupportFoundationTests
    {
        private static readonly string[] Names={"GetSupportedNodeCount", "GetSupportedNodeIds", "GetSupportType", "GetSupportInformation", "GetSupportUniqueId", "GetElasticMatDefinitionCount", "GetElasticMat", "GetElasticMatAssignedNodeIds", "GetPlateMatDefinitionCount", "GetElasticFootingDefinitionCount", "GetElasticFooting", "GetElasticFootingAssignedNodeIds", "CreateFixedSupport", "CreatePinnedSupport", "CreateFixedButSupport", "AssignSupportToNodes", "RemoveSupportFromNode", "DeleteSupport", "SetSupportUniqueId"};
        private static readonly string[] NativeNames={"GetSupportCount", "GetSupportNodes", "GetSupportType", "GetSupportInformation", "GetSupportUniqueID", "GetCountOfElasticMat", "GetElasticMatDetail", "GetElasticMatAssignmentList", "GetCountOfPlateMat", "GetCountOfElasticFooting", "GetElasticFootingDetail", "GetElasticFootingAssignmentList", "CreateSupportFixed", "CreateSupportPinned", "CreateSupportFixedBut", "AssignSupportToNode", "RemoveSupportFromNode", "DeleteSupport", "SetSupportUniqueID"};
        private static readonly Guid Id=Guid.Parse("dfe2cb8e-1b42-487f-a02c-953ac006d64a");
        private static OpenStaadWrapper Wrap(SupportFoundationFixture.Native n,int major=26)=>new OpenStaadWrapper(new SupportFoundationFixture.Root{Support=n,Major=major});
        private static object[] Args(string name)=>typeof(IOSSupport).GetMethod(name).GetParameters().Select(p=>
            p.ParameterType==typeof(Guid)?(object)Id:p.ParameterType==typeof(IEnumerable<int>)?new[]{11,12}:
            p.ParameterType==typeof(FixedButSupportDefinition)?new FixedButSupportDefinition(new Releases{My=true},new Springs{KFx=100}):
            p.Name=="maximumResults"?100:p.Name=="nodeId"?11:4).ToArray();
        private static object Invoke(IOSSupport support,string name,object[] args=null)
        {try{return typeof(IOSSupport).GetMethod(name).Invoke(support,args??Args(name));}catch(TargetInvocationException e){ExceptionDispatchInfo.Capture(e.InnerException).Throw();throw;}}
        private static object Value(object result,string name)=>result.GetType().GetProperty(name).GetValue(result);
        private static void Return(SupportFoundationFixture.Native n,string op,params object[] responses)=>n.Returns[op]=new Queue<object>(responses);
        private static void Failed(object r,SupportQueryStatus status){Assert.That(Value(r,"Status"),Is.EqualTo(status));Assert.That(Value(r,"IsSuccessful"),Is.False);Assert.Throws<TargetInvocationException>(()=>Value(r,"Data"));}
        [TestCaseSource(nameof(Names))]
        public void ReviewedRoutesUseExactNativeMemberOnCallerApartment(string name)
        {
            foreach(int major in new[]{24,25,26})
            {
                var n=new SupportFoundationFixture.Native();using(var w=Wrap(n,major))
                {
                    var result=Invoke(w.Support,name);string op=NativeNames[Array.IndexOf(Names,name)];
                    Assert.That(n.Calls.Contains(op),Is.True);
                    Assert.That(n.Threads.All(t=>t==Thread.CurrentThread.ManagedThreadId),Is.True);
                    Assert.That(Value(result,"NativeFunction"),Is.EqualTo(op));
                    Assert.That(Value(result,"IsSuccessful"),Is.EqualTo(name!="SetSupportUniqueId"));
                    if(name=="SetSupportUniqueId"){Assert.That(n.Arguments.Single(),Is.EqualTo(new object[]{4,Id.ToString("D")}));Assert.That(Value(result,"NativeReturnCode"),Is.Null);}
                    if(name=="GetSupportedNodeIds")Assert.That(n.Calls,Is.EqualTo(new[]{"GetSupportCount","GetSupportNodes","GetSupportCount"}));
                    if(name=="AssignSupportToNodes"){Assert.That(n.Arguments.Single()[0],Is.EqualTo(new[]{11,12}));Assert.That(n.Arguments.Single()[1],Is.EqualTo(4));}
                }
            }
        }
        [TestCaseSource(nameof(Names))]
        public void DisposedBindingCannotInvokeNativeMember(string name)
        {var n=new SupportFoundationFixture.Native();var w=Wrap(n);var support=w.Support;w.Dispose();Assert.Throws<InvalidOperationException>(()=>Invoke(support,name));Assert.That(n.Calls,Is.Empty);}
        [TestCaseSource(nameof(Names))]
        public void RebindingDuringOperationCannotPublishResult(string name)
        {
            var n=new SupportFoundationFixture.Native();var root=new SupportFoundationFixture.Root{Support=n};
            using(var w=new OpenStaadWrapper(root))
            {var support=w.Support;n.OnCall=op=>{n.OnCall=null;root.Support=new SupportFoundationFixture.Native();w.RefreshComObjects();};Assert.Throws<InvalidOperationException>(()=>Invoke(support,name));}
        }
        [TestCaseSource(nameof(Names))]
        public void TransportExceptionsPreserveIdentityWithoutRetry(string name)
        {
            var n=new SupportFoundationFixture.Native();var failure=new COMException("native");n.OnCall=op=>throw failure;
            using(var w=Wrap(n))Assert.That(Assert.Throws<COMException>(()=>Invoke(w.Support,name)),Is.SameAs(failure));Assert.That(n.Calls.Count,Is.EqualTo(1));
        }
        [TestCaseSource(nameof(Names))]
        public void EveryPublicContractHasMatchingDetailedXmlAndTypedParameters(string name)
        {
            var method=typeof(IOSSupport).GetMethod(name);var adapter=typeof(OSSupportAdapter).GetMethod(name);
            Assert.That(adapter.ReturnType,Is.EqualTo(method.ReturnType));
            Assert.That(method.GetParameters().Any(p=>p.ParameterType.IsByRef||p.ParameterType==typeof(object)),Is.False);
            var xml=XDocument.Load(System.IO.Path.ChangeExtension(typeof(IOSSupport).Assembly.Location,".xml"));
            var entries=new List<XElement>();
            foreach(var owner in new[]{typeof(IOSSupport),typeof(OSSupportAdapter)})
            {
                string prefix="M:"+owner.FullName+"."+name;
                var entry=xml.Descendants("member").Single(x=>((string)x.Attribute("name")).Split('(')[0]==prefix);entries.Add(entry);
                foreach(string tag in new[]{"summary","returns","remarks","example"})Assert.That(entry.Element(tag)?.Value,Is.Not.Null.And.Not.Empty);
                Assert.That(entry.Element("remarks").Value,Does.Contain("When to use:").And.Contain("COM apartment"));
                Assert.That(entry.Elements("param").Select(x=>(string)x.Attribute("name")),Is.EqualTo(method.GetParameters().Select(p=>p.Name)));
            }
            Assert.That(entries[0].Nodes().Select(x=>x.ToString()),Is.EqualTo(entries[1].Nodes().Select(x=>x.ToString())));
        }
        [TestCaseSource(nameof(Names))]
        public void InvalidParametersAreRejectedBeforeNativeAccess(string name)
        {
            var n=new SupportFoundationFixture.Native();using(var w=Wrap(n))
            {
                var pars=typeof(IOSSupport).GetMethod(name).GetParameters();
                for(int i=0;i<pars.Length;i++)
                {
                    var args=Args(name);args[i]=pars[i].ParameterType==typeof(Guid)?(object)Guid.Empty:pars[i].ParameterType==typeof(FixedButSupportDefinition)||pars[i].ParameterType==typeof(IEnumerable<int>)?null:-1;
                    Assert.That(()=>Invoke(w.Support,name,args),Throws.InstanceOf<ArgumentException>(),name+" "+pars[i].Name);
                }
                Assert.That(n.Calls,Is.Empty);
            }
        }
        [Test]
        public void PublicResultModelsAndEnumsShipSummaryXml()
        {
            var assembly=typeof(IOSSupport).Assembly;var xml=XDocument.Load(System.IO.Path.ChangeExtension(assembly.Location,".xml")).Descendants("member").ToDictionary(x=>(string)x.Attribute("name"));
            foreach(var type in assembly.GetExportedTypes().Where(t=>t.Namespace==typeof(SupportInformation).Namespace||t.Namespace==typeof(SupportQueryStatus).Namespace||new[]{typeof(SupportType),typeof(FoundationSpringType),typeof(FoundationSupportDirection)}.Contains(t)))
            {
                Assert.That(xml["T:"+type.FullName].Element("summary")?.Value,Is.Not.Null.And.Not.Empty);
                foreach(var p in type.GetProperties(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly))Assert.That(xml["P:"+type.FullName+"."+p.Name].Element("summary")?.Value,Is.Not.Null.And.Not.Empty);
                if(type.IsEnum)foreach(string n in Enum.GetNames(type))Assert.That(xml["F:"+type.FullName+"."+n].Element("summary")?.Value,Is.Not.Null.And.Not.Empty);
            }
            Assert.That(typeof(IOSSupport).GetMethods().Count(m=>!m.IsSpecialName),Is.EqualTo(19));
        }
        [Test]
        public void CountsRejectFlagsAndMalformedNumericCoercionWhileZeroIsData()
        {
            foreach(string op in new[]{"GetSupportCount","GetCountOfElasticMat","GetCountOfPlateMat","GetCountOfElasticFooting"})
            foreach(object raw in new object[]{0,3,true,false,-1,1.0,1m,"2",null,uint.MaxValue})
            {
                var n=new SupportFoundationFixture.Native();Return(n,op,raw);using(var w=Wrap(n))
                {
                    string method=Names[Array.IndexOf(NativeNames,op)];
                    if(raw is int i&&i>=0)Assert.That(Value(Invoke(w.Support,method),"Data"),Is.EqualTo(raw));else Assert.Throws<InvalidOperationException>(()=>Invoke(w.Support,method));
                }
            }
        }
        [Test]
        public void NoSupportUnknownTypeAndGeneralErrorHaveDistinctOutcomes()
        {
            foreach(int code in new[]{0,1,3,13,77,-1})
            {
                var n=new SupportFoundationFixture.Native{TypeCode=code,WriteOutputs=code>0&&code<=13};using(var w=Wrap(n))
                {
                    var type=w.Support.GetSupportType(11);var info=w.Support.GetSupportInformation(11);
                    if(code==-1){Failed(type,SupportQueryStatus.NativeFailure);Failed(info,SupportQueryStatus.NativeFailure);}
                    else{Assert.That(type.Data.NativeTypeCode,Is.EqualTo(code));Assert.That(type.Data.KnownType,Is.EqualTo(code<=13?(SupportType?)code:null));
                        if(code==0)Failed(info,SupportQueryStatus.NoSupport);else if(code==77)Failed(info,SupportQueryStatus.UnsupportedNativeType);else Assert.That(info.IsSuccessful,Is.True);}
                }
            }
            using(var w=Wrap(new SupportFoundationFixture.Native{TypeCode=true}))Assert.Throws<InvalidOperationException>(()=>w.Support.GetSupportInformation(11));
        }
        [Test]
        public void SupportStatesRetainSpringModeAndOwnNativeBuffers()
        {
            var n=new SupportFoundationFixture.Native();using(var w=Wrap(n))
            {
                var d=w.Support.GetSupportInformation(11).Data;Assert.That(d.Fx.State,Is.EqualTo(SupportDofState.Spring));Assert.That(d.Fy.State,Is.EqualTo(SupportDofState.Fixed));Assert.That(d.Fz.State,Is.EqualTo(SupportDofState.Released));
                Assert.That(d.Mz.SpringStiffness,Is.EqualTo(5.25));((double[])n.Buffers["springs"])[5]=99;Assert.That(d.Mz.SpringStiffness,Is.EqualTo(5.25));
            }
        }
        [Test]
        public void SuccessfulReadbackRequiresWrittenCorrectlyShapedBuffers()
        {
            foreach(string name in new[]{"GetSupportedNodeIds","GetSupportInformation","GetElasticMatAssignedNodeIds","GetElasticFootingAssignedNodeIds"})
            {
                using(var w=Wrap(new SupportFoundationFixture.Native{WriteOutputs=false}))Assert.Throws<InvalidOperationException>(()=>Invoke(w.Support,name),name);
                foreach(Func<string,Array,object> replace in new Func<string,Array,object>[] {
                    (role,a)=>null,(role,a)=>new int[a.Length+1],(role,a)=>new double[1,a.Length],(role,a)=>new object[a.Length],(role,a)=>new float[a.Length]})
                    using(var w=Wrap(new SupportFoundationFixture.Native{ReplaceArray=replace}))Assert.Throws<InvalidOperationException>(()=>Invoke(w.Support,name),name);
            }
        }
        [Test]
        public void NodeListsSupportNonzeroNativeLowerBoundsAndOwnedReadOnlyStorage()
        {
            var n=new SupportFoundationFixture.Native{ReplaceArray=(role,a)=>{
                var arr=Array.CreateInstance(typeof(int),new[]{a.Length},new[]{5});for(int i=0;i<a.Length;i++)arr.SetValue(11+i,i+5);return arr;}};
            using(var w=Wrap(n))
            {
                var ids=w.Support.GetSupportedNodeIds().Data;Assert.That(ids,Is.EqualTo(new[]{11,12}));Assert.Throws<NotSupportedException>(()=>((IList<int>)ids).Clear());
            }
        }
        [Test]
        public void CountRacesAndAllocationLimitsNeverPublishMixedOrOversizedLists()
        {
            var n=new SupportFoundationFixture.Native();Return(n,"GetSupportCount",2,3);using(var w=Wrap(n))Failed(w.Support.GetSupportedNodeIds(),SupportQueryStatus.StateChanged);
            n=new SupportFoundationFixture.Native{NodeCount=5};using(var w=Wrap(n)){Failed(w.Support.GetSupportedNodeIds(2),SupportQueryStatus.LimitExceeded);Assert.That(n.Calls,Is.EqualTo(new[]{"GetSupportCount"}));}
            n=new SupportFoundationFixture.Native{NodeCount=0};using(var w=Wrap(n)){Assert.That(w.Support.GetSupportedNodeIds().Data,Is.Empty);Assert.That(n.Calls,Is.EqualTo(new[]{"GetSupportCount","GetSupportCount"}));}
            n=new SupportFoundationFixture.Native();Return(n,"GetSupportNodes",-1);using(var w=Wrap(n))Failed(w.Support.GetSupportedNodeIds(),SupportQueryStatus.NativeFailure);
            n=new SupportFoundationFixture.Native();Return(n,"GetSupportNodes",true);using(var w=Wrap(n))Assert.Throws<InvalidOperationException>(()=>w.Support.GetSupportedNodeIds());
        }
        [Test]
        public void FoundationDetailsKeepDimensionsDirectionFlagsAndModulus()
        {
            using(var w=Wrap(new SupportFoundationFixture.Native()))
            {var mat=w.Support.GetElasticMat(4).Data;Assert.That(mat.Direction,Is.EqualTo(FoundationSupportDirection.YOnly));Assert.That(mat.PrintInfluence,Is.True);Assert.That(mat.SpringType,Is.EqualTo(FoundationSpringType.CompressionOnly));Assert.That(mat.SubgradeModulus,Is.EqualTo(125));var foot=w.Support.GetElasticFooting(4).Data;Assert.That(foot.Length,Is.EqualTo(2.5));Assert.That(foot.Width,Is.EqualTo(3));}
            foreach(string field in new[]{"direction","subgrade","print","springType","assignmentCount"})
            {var n=new SupportFoundationFixture.Native();n.Values[field]=double.NaN;using(var w=Wrap(n))Assert.Throws<InvalidOperationException>(()=>w.Support.GetElasticMat(4),field);}
            foreach(string field in new[]{"length","width","direction","subgrade","assignmentCount"})
            {var n=new SupportFoundationFixture.Native();n.Values[field]=double.NaN;using(var w=Wrap(n))Assert.Throws<InvalidOperationException>(()=>w.Support.GetElasticFooting(4),field);}
        }
        [TestCase(true)]
        [TestCase(false)]
        public void FoundationListsRequireStableFullDetailAndRetainFamilyIdentity(bool mat)
        {
            string list=mat?"GetElasticMatAssignmentList":"GetElasticFootingAssignmentList";var n=new SupportFoundationFixture.Native();
            n.OnCall=op=>{if(op==list)n.Values[mat?"subgrade":"width"]=999.0;};
            using(var w=Wrap(n))Failed(mat?w.Support.GetElasticMatAssignedNodeIds(4):w.Support.GetElasticFootingAssignedNodeIds(4),SupportQueryStatus.StateChanged);
            n=new SupportFoundationFixture.Native{AssignmentCount=3};using(var w=Wrap(n)){Failed(mat?w.Support.GetElasticMatAssignedNodeIds(4,2):w.Support.GetElasticFootingAssignedNodeIds(4,2),SupportQueryStatus.LimitExceeded);Assert.That(n.Calls.Count,Is.EqualTo(1));}
            n=new SupportFoundationFixture.Native{AssignmentCount=0};using(var w=Wrap(n)){var r=mat?w.Support.GetElasticMatAssignedNodeIds(4):w.Support.GetElasticFootingAssignedNodeIds(4);Assert.That(r.Data,Is.Empty);Assert.That(n.Calls.Contains(list),Is.False);}
        }
        [Test]
        public void FailurePrecedesUnwrittenFoundationOutputsAndDoesNotBecomeZeroData()
        {
            var n=new SupportFoundationFixture.Native{WriteOutputs=false};Return(n,"GetElasticMatDetail",false);Return(n,"GetElasticFootingDetail",0);
            using(var w=Wrap(n)){Failed(w.Support.GetElasticMat(4),SupportQueryStatus.NativeFailure);Failed(w.Support.GetElasticFooting(4),SupportQueryStatus.NativeFailure);}
        }
        [Test]
        public void GuidObservationAndVoidMutationKeepScopeAndEvidence()
        {
            var n=new SupportFoundationFixture.Native();using(var w=Wrap(n))
            {
                var g=w.Support.GetSupportUniqueId(4).Data;Assert.That(g.Value,Is.EqualTo(Id));Assert.That(g.NativeText,Is.EqualTo(Id.ToString("D").ToUpperInvariant()));
                var r=w.Support.SetSupportUniqueId(4,Id);Assert.That(r.Status,Is.EqualTo(SupportMutationStatus.InvokedWithoutAcknowledgement));Assert.That(r.IsSuccessful,Is.False);Assert.That(r.NativeBooleanReturn,Is.Null);Assert.That(r.NativeReturnCode,Is.Null);Assert.That(r.RequestedUniqueId,Is.EqualTo(Id));Assert.That(r.SupportId,Is.EqualTo(4));
            }
            foreach(object raw in new object[]{null,DBNull.Value,""}){n=new SupportFoundationFixture.Native();n.Values["guid"]=raw;using(var w=Wrap(n))Failed(w.Support.GetSupportUniqueId(4),SupportQueryStatus.EmptyOrUnavailable);}
            foreach(object raw in new object[]{true,5,"not-a-guid","x\0y"}){n=new SupportFoundationFixture.Native();n.Values["guid"]=raw;using(var w=Wrap(n))Assert.Throws<InvalidOperationException>(()=>w.Support.GetSupportUniqueId(4));}
        }
        [Test]
        public void CreatedReferencesCannotBeConfusedWithSuccessFlags()
        {
            foreach(string name in new[]{"CreateFixedSupport","CreatePinnedSupport","CreateFixedButSupport"})
            foreach(object raw in new object[]{4,-1,0,true,false,4.0,1m,null,-2})
            {
                var n=new SupportFoundationFixture.Native{CreatedId=raw};using(var w=Wrap(n))
                {
                    if(raw is int i&&i==4){var r=(SupportMutationResult)Invoke(w.Support,name);Assert.That(r.SupportId,Is.EqualTo(4));Assert.That(r.NativeReturnCode,Is.EqualTo(4));}
                    else if(raw is int failure&&failure==-1)Assert.That(((SupportMutationResult)Invoke(w.Support,name)).Status,Is.EqualTo(SupportMutationStatus.NativeFailure));
                    else Assert.Throws<InvalidOperationException>(()=>Invoke(w.Support,name));
                }
            }
        }
        [Test]
        public void FixedButRequestSnapshotsExistingPacketsAndMarshalsDoubleFlags()
        {
            var flags=new Releases{My=true,Mz=true};var springs=new Springs{KFx=100};
            var definition=new FixedButSupportDefinition(flags,springs);flags.My=false;springs.KFx=999;
            var n=new SupportFoundationFixture.Native();using(var w=Wrap(n))
            {w.Support.CreateFixedButSupport(definition);Assert.That(n.Arguments.Single()[0],Is.TypeOf<double[]>());Assert.That(n.Arguments.Single()[0],Is.EqualTo(new[]{0.0,0,0,0,1,1}));Assert.That(n.Arguments.Single()[1],Is.EqualTo(new[]{100.0,0,0,0,0,0}));}
            var copy=definition.ToSpringConstants();copy.KFx=5;Assert.That(definition.ToSpringConstants().KFx,Is.EqualTo(100));
            Assert.Throws<ArgumentNullException>(()=>new FixedButSupportDefinition(flags,new Springs{KFx=null}));
            Assert.Throws<ArgumentOutOfRangeException>(()=>new FixedButSupportDefinition(flags,new Springs{KFx=-1}));
            Assert.Throws<ArgumentOutOfRangeException>(()=>new FixedButSupportDefinition(flags,new Springs{KFx=double.NaN}));
        }
        private sealed class Once:IEnumerable<int>
        {public int Enumerations;public IEnumerator<int> GetEnumerator(){if(++Enumerations>1)throw new Exception("Twice");yield return 12;yield return 11;yield return 12;}IEnumerator IEnumerable.GetEnumerator()=>GetEnumerator();}
        [Test]
        public void AssignmentSnapshotsOnePassSelectionAndPreservesOrderDuplicatesWithoutOverwritingCaller()
        {
            var n=new SupportFoundationFixture.Native();using(var w=Wrap(n))
            {
                var nodes=new Once();var r=w.Support.AssignSupportToNodes(4,nodes);Assert.That(nodes.Enumerations,Is.EqualTo(1));Assert.That(r.NodeIds,Is.EqualTo(new[]{12,11,12}));
                ((int[])n.Arguments.Single()[0])[0]=99;Assert.That(r.NodeIds[0],Is.EqualTo(12));
                Assert.Throws<ArgumentException>(()=>w.Support.AssignSupportToNodes(4,new[]{11,12},1));
                n.Calls.Clear();var empty=w.Support.AssignSupportToNodes(4,new int[0]);Assert.That(empty.Status,Is.EqualTo(SupportMutationStatus.NoOperation));Assert.That(empty.IsSuccessful,Is.False);Assert.That(n.Calls,Is.Empty);
            }
        }
        [Test]
        public void ZeroSuccessMutationsAndBooleanRemovalRetainDifferentNativeConventions()
        {
            foreach(string name in new[]{"AssignSupportToNodes","DeleteSupport"})
            foreach(object raw in new object[]{0,-1,true,false,1,0.0,null})
            {
                var n=new SupportFoundationFixture.Native();Return(n,NativeNames[Array.IndexOf(Names,name)],raw);using(var w=Wrap(n))
                {if(raw is int code&&(code==0||code==-1)){var r=(SupportMutationResult)Invoke(w.Support,name);Assert.That(r.IsSuccessful,Is.EqualTo(code==0));Assert.That(r.NativeReturnCode,Is.EqualTo(code));Assert.That(r.NativeBooleanReturn,Is.Null);}else Assert.Throws<InvalidOperationException>(()=>Invoke(w.Support,name));}
            }
            foreach(object raw in new object[]{true,false,1,0,-1,1.0})
            {
                var n=new SupportFoundationFixture.Native();Return(n,"RemoveSupportFromNode",raw);using(var w=Wrap(n))
                {if(raw is bool||raw is int code&&(code==0||code==1))Assert.That(w.Support.RemoveSupportFromNode(11).IsSuccessful,Is.EqualTo(raw is bool b?b:(int)raw==1));else Assert.Throws<InvalidOperationException>(()=>w.Support.RemoveSupportFromNode(11));}
            }
        }
    }
}
