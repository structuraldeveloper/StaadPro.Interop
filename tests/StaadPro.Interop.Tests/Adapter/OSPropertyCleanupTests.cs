using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using NUnit.Framework;
using PackageVerification;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Adapters.Interfaces.Capabilities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Property;
namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSPropertyCleanupTests
    {
        private static readonly string[] Names = { "RemovePropertyFromPlate", "RemoveMaterialFromPlate", "RemoveMaterialFromSolid", "DeleteMemberReleaseSpec", "DeleteAllControlDependentRelations", "RemoveAllElementOffsetSpec", "RemovePropertyFromBeam", "DeleteProperty", "DeleteMemberSpec", "RemoveMemberReleaseSpecFromBeam", "RemoveMemberOffsetSpecFromBeam", "RemoveMemberTrussSpecFromBeam", "RemoveMemberInactiveSpecFromBeam", "RemoveMemberTensionSpecFromBeam", "RemoveMemberCompressionSpecFromBeam", "RemoveMemberIgnoreStiffSpecFromBeam", "RemoveMemberCableSpecFromBeam", "RemoveElementPlaneStressSpecFromPlate", "RemoveElementIgnoreInplaneRotnSpecFromPlate", "RemoveElementNodeReleaseSpecFromPlate" };
        private static readonly string[] CatalogNames = { "GetDefaultStandardProfileDBFolder", "IsStandardDatabaseSection", "GetStandardSectionDatabaseName", "GetStandardSectionTableName", "GetStandardSectionName" };
        private static readonly string[] Zero = { "DeleteAllControlDependentRelations", "RemovePropertyFromBeam", "RemoveMemberTrussSpecFromBeam", "RemoveMemberInactiveSpecFromBeam", "RemoveMemberTensionSpecFromBeam", "RemoveMemberCompressionSpecFromBeam", "RemoveMemberIgnoreStiffSpecFromBeam", "RemoveElementPlaneStressSpecFromPlate", "RemoveElementIgnoreInplaneRotnSpecFromPlate", "RemoveElementNodeReleaseSpecFromPlate" };
        public static IEnumerable<TestCaseData> Ports => Names.Select(n=>new TestCaseData(n));
        public static IEnumerable<TestCaseData> CatalogPorts => CatalogNames.Select(n=>new TestCaseData(n));
        private static MethodInfo Method(string n) => typeof(IOSProperty).GetMethod(n);
        private static object[] Args(string n) => Method(n).GetParameters().Select(p=>p.ParameterType==typeof(IEnumerable<int>)?(object)new[]{101,102,101}:p.ParameterType==typeof(MemberEnd)?MemberEnd.End:p.ParameterType==typeof(CableDefinitionKind)?CableDefinitionKind.UnstressedLength:p.Name=="nodeId"?(object)11:7).ToArray();
        private static object Invoke(object p, MethodInfo m, object[] args) { try{return m.Invoke(p,args);}catch(TargetInvocationException e){ExceptionDispatchInfo.Capture(e.InnerException).Throw();throw;} }
        private static PropertyMutationResult Run(IOSProperty p,string n,object[] args=null)=>(PropertyMutationResult)Invoke(p,Method(n),args??Args(n));
        private static object Query(IOSPropertyStandardCatalog c,string n)=>(object)Invoke(c,typeof(IOSPropertyStandardCatalog).GetMethod(n),n=="GetDefaultStandardProfileDBFolder"?new object[0]:new object[]{7});
        private static OpenStaadWrapper Wrap(object n,Version v=null)=>new OpenStaadWrapper(new PropertyCleanupFixture.Root{Property=n,NativeVersion=v??new Version(26,0,0,340)});
        private static void Respond(PropertyCleanupFixture.Native n,string op,params object[] values)=>n.Responses[op]=new Queue<object>(values);
        [Test]
        public void CommonAndOptionalSurfacesExposeExactlyTwentyFiveNewTypedOperations()
        {
            Assert.That(typeof(IOSProperty).GetMethods().Count(m=>!m.IsSpecialName),Is.EqualTo(245));Assert.That(CatalogNames.Length,Is.EqualTo(5));
            Assert.That(typeof(OSPropertyAdapter).GetInterfaces(),Does.Not.Contain(typeof(IOSPropertyStandardCatalog)));
            foreach(string n in Names){var m=Method(n);var actual=typeof(OSPropertyAdapter).GetMethod(n);Assert.That(actual.ReturnType,Is.EqualTo(m.ReturnType));Assert.That(actual.GetParameters().Select(p=>p.Name),Is.EqualTo(m.GetParameters().Select(p=>p.Name)));}
            foreach(var m in typeof(IOSPropertyStandardCatalog).GetMethods())Assert.That(m.GetParameters().Any(p=>p.IsOut||p.ParameterType.IsByRef||p.ParameterType==typeof(object)),Is.False);
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryMutationRetainsItsActualNativeAcknowledgementAndCallingThread(string name)
        {var n=new PropertyCleanupFixture.Native();using(var w=Wrap(n)){var r=Run(w.Property,name);Assert.That(r.IsSuccessful,Is.True);Assert.That(r.NativeFunction,Is.EqualTo(name));Assert.That(n.Calls,Is.EqualTo(new[]{name}));Assert.That(n.Threads.Single(),Is.EqualTo(Thread.CurrentThread.ManagedThreadId));}}
        [TestCaseSource(nameof(Ports))]
        public void EveryMutationRejectsDisposedBinding(string name)
        {var n=new PropertyCleanupFixture.Native();var w=Wrap(n);var p=w.Property;w.Dispose();Assert.Throws<InvalidOperationException>(()=>Run(p,name));Assert.That(n.Calls,Is.Empty);}
        [TestCaseSource(nameof(Ports))]
        public void EveryMutationStopsAfterAReplacedBindingWithoutRetry(string name)
        {var n=new PropertyCleanupFixture.Native();var root=new PropertyCleanupFixture.Root{Property=n};using(var w=new OpenStaadWrapper(root)){var p=w.Property;n.OnCall=op=>{n.OnCall=null;root.Property=new PropertyCleanupFixture.Native();w.RefreshComObjects();};Assert.Throws<InvalidOperationException>(()=>Run(p,name));Assert.That(n.Calls.Count,Is.EqualTo(1));}}
        [TestCaseSource(nameof(Ports))]
        public void EveryMutationPropagatesOriginalTransportFailureWithoutRetry(string name)
        {var n=new PropertyCleanupFixture.Native();var error=new COMException("transport");n.OnCall=op=>throw error;using(var w=Wrap(n)){Assert.That(Assert.Throws<COMException>(()=>Run(w.Property,name)),Is.SameAs(error));Assert.That(n.Calls.Count,Is.EqualTo(1));}}
        [TestCaseSource(nameof(Ports))]
        public void EachMutationUsesItsOwnFailurePolicyRatherThanAGenericBooleanConversion(string name)
        {var n=new PropertyCleanupFixture.Native();Respond(n,name,Zero.Contains(name)?-1:0);using(var w=Wrap(n)){var r=Run(w.Property,name);Assert.That(r.Status,Is.EqualTo(name=="RemoveAllElementOffsetSpec"?PropertyMutationStatus.NoSpecification:PropertyMutationStatus.NativeFailure));Assert.That(r.IsSuccessful,Is.False);Assert.That(r.NativeReturnCode,Is.EqualTo(Zero.Contains(name)?-1:0));Assert.That(r.NativeBooleanReturn,Is.Null);}}
        [TestCaseSource(nameof(Ports))]
        public void MalformedAcknowledgementsNeverBecomeSuccessfulMutations(string name)
        {var n=new PropertyCleanupFixture.Native();Respond(n,name,null,"1",1.0,DBNull.Value,8);using(var w=Wrap(n))for(int i=0;i<5;i++)Assert.Throws<InvalidOperationException>(()=>Run(w.Property,name));}
        [TestCaseSource(nameof(Ports))]
        public void NewXmlContractsMatchBothSurfacesAndKeepMutationsGuarded(string name)
        {var docs=XDocument.Load(Path.ChangeExtension(typeof(IOSProperty).Assembly.Location,".xml"));foreach(var owner in new[]{"Adapters.Interfaces.IOSProperty","Adapters.Models.OSPropertyAdapter"}){var prefix="M:StaadPro.Interop."+owner+"."+name;var m=docs.Descendants("member").Single(x=>(string)x.Attribute("name")==prefix||((string)x.Attribute("name")).StartsWith(prefix+"("));foreach(var tag in new[]{"summary","returns","remarks","example"})Assert.That(m.Element(tag)?.Value,Is.Not.Null.And.Not.Empty);Assert.That(m.Element("example").Value,Does.Contain("bool applyChanges = false"));Assert.That(m.Elements("param").Select(p=>(string)p.Attribute("name")),Is.EqualTo(Method(name).GetParameters().Select(p=>p.Name)));}}
        [Test]
        public void SeparateRequestedDefinitionEndCableAndNodeIdentitiesAreRetained()
        {var n=new PropertyCleanupFixture.Native();using(var w=Wrap(n)){PropertyCleanupFixture.VerifyCalls(w.Property,n);var p=w.Property;Assert.That(p.DeleteProperty(8).PropertyId,Is.EqualTo(8));Assert.That(p.DeleteMemberSpec(9).SpecificationId,Is.EqualTo(9));var release=p.DeleteMemberReleaseSpec(101,MemberEnd.Start);Assert.That(release.MemberEnd,Is.EqualTo(MemberEnd.Start));Assert.That(release.SpecificationId,Is.Null);var cable=p.RemoveMemberCableSpecFromBeam(101,CableDefinitionKind.UnstressedLength);Assert.That(cable.MemberEnd,Is.Null);Assert.That(cable.CableDefinitionKind,Is.EqualTo(CableDefinitionKind.UnstressedLength));var node=p.RemoveElementNodeReleaseSpecFromPlate(101,11);Assert.That(node.TargetIds,Is.EqualTo(new[]{101}));Assert.That(node.NodeId,Is.EqualTo(11));Assert.That(node.PlateNodeIndex,Is.Null);}}
        [Test]
        public void BulkInputsAreBoundedEnumeratedOnceAndOwnedIncludingDuplicates()
        {
            var n = new PropertyCleanupFixture.Native();
            n.OnCall = op => ((int[])n.Arguments.Last()[0])[0] = 999;
            using (var w = Wrap(n))
            {
                int enumerations = 0;
                var r = w.Property.RemoveMaterialFromPlate(SingleEnumeration(() => enumerations++));
                Assert.That(enumerations, Is.EqualTo(1));
                Assert.That(r.TargetIds, Is.EqualTo(new[] { 101, 102, 101 }));
                ((int[])n.Arguments.Single()[0])[0] = 888;
                Assert.That(r.TargetIds[0], Is.EqualTo(101));
                Assert.Throws<ArgumentException>(() => w.Property.RemoveMaterialFromSolid(Enumerable.Repeat(1, 100001)));
                Assert.Throws<ArgumentException>(() => w.Property.RemoveMaterialFromPlate(new int[0]));
                Assert.Throws<ArgumentException>(() => w.Property.RemoveMaterialFromPlate(new[] { 0 }));
                Assert.Throws<ArgumentNullException>(() => w.Property.RemoveMaterialFromSolid(null));
                Assert.That(n.Calls.Count, Is.EqualTo(1));
            }
        }
        private static IEnumerable<int> SingleEnumeration(Action start){start();yield return 101;yield return 102;yield return 101;}
        [Test]
        public void InvalidScalarAndSelectorInputsAreRejectedBeforeNativeMutation()
        {var n=new PropertyCleanupFixture.Native();using(var w=Wrap(n)){foreach(string name in Names.Where(x=>Method(x).GetParameters().Any(p=>p.ParameterType==typeof(int)))){var args=Args(name);int i=Array.FindIndex(Method(name).GetParameters(),p=>p.ParameterType==typeof(int));args[i]=0;Assert.Throws<ArgumentOutOfRangeException>(()=>Run(w.Property,name,args));}Assert.Throws<ArgumentOutOfRangeException>(()=>w.Property.DeleteMemberReleaseSpec(1,(MemberEnd)2));Assert.Throws<ArgumentOutOfRangeException>(()=>w.Property.RemoveMemberCableSpecFromBeam(1,(CableDefinitionKind)2));Assert.Throws<ArgumentOutOfRangeException>(()=>w.Property.RemoveElementNodeReleaseSpecFromPlate(1,0));Assert.That(n.Calls,Is.Empty);}}
        [Test]
        public void BooleanAndIntegralEvidenceAreNeverFabricatedOrConfused()
        {var n=new PropertyCleanupFixture.Native();using(var w=Wrap(n)){Respond(n,"DeleteProperty",true,false,1,0,-1);Assert.That(w.Property.DeleteProperty(1).NativeBooleanReturn,Is.True);Assert.That(w.Property.DeleteProperty(1).NativeBooleanReturn,Is.False);Assert.That(w.Property.DeleteProperty(1).NativeReturnCode,Is.EqualTo(1));Assert.That(w.Property.DeleteProperty(1).Status,Is.EqualTo(PropertyMutationStatus.NativeFailure));Assert.Throws<InvalidOperationException>(()=>w.Property.DeleteProperty(1));Respond(n,"RemovePropertyFromBeam",true);Assert.Throws<InvalidOperationException>(()=>w.Property.RemovePropertyFromBeam(1));Respond(n,"RemoveAllElementOffsetSpec",false);Assert.Throws<InvalidOperationException>(()=>w.Property.RemoveAllElementOffsetSpec());}}
        [TestCase("24.0.2.354",PropertyCapabilityAvailability.DisabledByVersionPolicy)]
        [TestCase("25.0.0.539",PropertyCapabilityAvailability.ContractUnverified)]
        [TestCase("26.0.0.341",PropertyCapabilityAvailability.ContractUnverified)]
        [TestCase("27.0.0.340",PropertyCapabilityAvailability.DisabledByVersionPolicy)]
        [TestCase("25.0.1.424",PropertyCapabilityAvailability.Available)]
        [TestCase("26.0.0.340",PropertyCapabilityAvailability.Available)]
        public void CatalogAcquisitionUsesExactNativeVersionWithoutCallingDomainMethods(string version,PropertyCapabilityAvailability expected)
        {var n=new PropertyCleanupFixture.Native();using(var w=Wrap(n,Version.Parse(version))){w.ApplicationVersion.Major=26;var r=w.Property.Capabilities.GetStandardCatalog();Assert.That(r.Availability,Is.EqualTo(expected));Assert.That(r.Capability!=null,Is.EqualTo(r.IsAvailable));Assert.That(r.NativeVersion,Is.EqualTo(Version.Parse(version)));Assert.That(n.Calls,Is.Empty);}}
        public sealed class Missing { public object GetDefaultStandardProfileDBFolder()=>throw new Exception("Must not invoke"); }
        public sealed class Wrong : PropertyCleanupFixture.Native { public object GetStandardSectionName(int id)=>throw new Exception("Must not invoke"); }
        [Test]
        public void MissingAndAmbiguousCatalogMetadataWithholdTheWholeCapability()
        {using(var w=Wrap(new Missing())){var r=w.Property.Capabilities.GetStandardCatalog();Assert.That(r.Availability,Is.EqualTo(PropertyCapabilityAvailability.NativeMemberAbsent));Assert.That(r.Capability,Is.Null);}using(var w=Wrap(new Wrong()))Assert.That(w.Property.Capabilities.GetStandardCatalog().Availability,Is.EqualTo(PropertyCapabilityAvailability.ContractUnverified));}
        [Test]
        public void UnknownNativeVersionCannotGrantTheNewerCapability()
        {using(var w=Wrap(new PropertyCleanupFixture.Native())){typeof(OpenStaadWrapper).GetProperty("StaadVersion").SetValue(w,null);Assert.That(w.Property.Capabilities.GetStandardCatalog().Availability,Is.EqualTo(PropertyCapabilityAvailability.UnknownVersion));}}
        [TestCaseSource(nameof(CatalogPorts))]
        public void CatalogCallsRunOnCallerAndRejectStaleOrDisposedBindings(string name)
        {var n=new PropertyCleanupFixture.Native();var root=new PropertyCleanupFixture.Root{Property=n};var w=new OpenStaadWrapper(root);var c=w.Property.Capabilities.GetStandardCatalog().Capability;Query(c,name);Assert.That(n.Threads.Single(),Is.EqualTo(Thread.CurrentThread.ManagedThreadId));root.Property=new PropertyCleanupFixture.Native();w.RefreshComObjects();Assert.Throws<InvalidOperationException>(()=>Query(c,name));w.Dispose();Assert.Throws<InvalidOperationException>(()=>Query(c,name));Assert.That(n.Calls.Count,Is.EqualTo(1));}
        [TestCaseSource(nameof(CatalogPorts))]
        public void CatalogCallsPropagateTransportFailureWithoutRetry(string name)
        {var n=new PropertyCleanupFixture.Native();using(var w=Wrap(n)){var c=w.Property.Capabilities.GetStandardCatalog().Capability;var error=new COMException("catalog");n.OnCall=op=>throw error;Assert.That(Assert.Throws<COMException>(()=>Query(c,name)),Is.SameAs(error));Assert.That(n.Calls.Count,Is.EqualTo(1));}}
        [Test]
        public void CatalogFalseAndExactTextRemainQualifiedDataWhileDocumentedAbsenceHasNoData()
        {var n=new PropertyCleanupFixture.Native();using(var w=Wrap(n)){var c=w.Property.Capabilities.GetStandardCatalog().Capability;Respond(n,"IsStandardDatabaseSection",false,0,true,1,-1);Assert.That(c.IsStandardDatabaseSection(7).Data.IsStandardDatabase,Is.False);Assert.That(c.IsStandardDatabaseSection(7).NativeReturnCode,Is.Zero);Assert.That(c.IsStandardDatabaseSection(7).NativeBooleanReturn,Is.True);Assert.That(c.IsStandardDatabaseSection(7).Data.PropertyId,Is.EqualTo(7));Assert.Throws<InvalidOperationException>(()=>c.IsStandardDatabaseSection(7));Respond(n,"GetStandardSectionName",""," EXACT ",null,DBNull.Value,"x\0y",5);var absent=c.GetStandardSectionName(7);Assert.That(absent.Status,Is.EqualTo(PropertyQueryStatus.EmptyOrUnavailable));Assert.Throws<InvalidOperationException>(()=>{var ignored=absent.Data;});Assert.That(c.GetStandardSectionName(7).Data.Text,Is.EqualTo(" EXACT "));for(int i=0;i<4;i++)Assert.Throws<InvalidOperationException>(()=>c.GetStandardSectionName(7));Respond(n,"GetDefaultStandardProfileDBFolder",null,DBNull.Value,"");for(int i=0;i<3;i++)Assert.That(c.GetDefaultStandardProfileDBFolder().Status,Is.EqualTo(PropertyQueryStatus.EmptyOrUnavailable));}}
        [Test]
        public void SeparateAdaptersAndCatalogAccessUseTheSharedPropertyGate()
        {var n=new PropertyCleanupFixture.Native();int active=0,maximum=0;n.OnCall=op=>{int a=Interlocked.Increment(ref active);maximum=Math.Max(a,maximum);Thread.Sleep(20);Interlocked.Decrement(ref active);};using(var w=Wrap(n)){var c=w.Property.Capabilities.GetStandardCatalog().Capability;Task.WaitAll(Task.Run(()=>w.Property.DeleteProperty(7)),Task.Run(()=>c.GetStandardSectionName(7)));Assert.That(maximum,Is.EqualTo(1));}}
    }
}
