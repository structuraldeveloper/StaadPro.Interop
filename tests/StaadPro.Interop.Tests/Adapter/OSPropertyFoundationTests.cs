using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Media.Media3D;
using System.Xml.Linq;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Property;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSPropertyFoundationTests
    {
        public sealed class Root
        {
            public object Property { get; set; }
            public object Geometry => null;
            public int Major = 26;
            public object GetApplicationVersion(ref object major, ref object update, ref object revision, ref object build)
            { major = Major; update = 0; revision = 0; build = 340; return Major + ".00.00.340"; }
        }
        public sealed class Native
        {
            public readonly List<string> Calls = new List<string>();
            public readonly List<object[]> Arguments = new List<object[]>();
            public readonly List<int> Threads = new List<int>();
            public readonly Dictionary<string, Queue<object>> Responses = new Dictionary<string, Queue<object>>();
            public readonly Dictionary<string, Exception> Errors = new Dictionary<string, Exception>();
            public readonly Dictionary<string, object> Buffers = new Dictionary<string, object>();
            public Action<string> OnCall;
            public Func<string, object, object> ReplaceBuffer, ReplaceScalar;
            public int Count = 2;
            public int[] ReleaseModes = { 0, 1, -1, -3, -2, -2 };
            public double[] ReleaseValues = { double.NaN, double.NaN, 12.5, double.NaN, 0.25, 0.5 };
            public double[] DirectionalFactors = { double.NaN, 0.8, 0.9 };
            public object CommonFactor = 0.75, Assignment = 2.0, OffsetAxis = 2;
            public object R(string name, object fallback, params object[] args)
            {
                Calls.Add(name); Arguments.Add(args); Threads.Add(Thread.CurrentThread.ManagedThreadId);
                OnCall?.Invoke(name); if (Errors.TryGetValue(name, out var error)) throw error;
                return Responses.TryGetValue(name, out var queue) && queue.Count > 0 ? queue.Dequeue() : fallback;
            }
            private object S(string role, object value) => ReplaceScalar == null ? value : ReplaceScalar(role, value);
            private void Fill(string role, ref object output, Array values)
            {
                var a = (Array)output; Assert.That(a.GetLowerBound(0), Is.Zero); Assert.That(a.Rank, Is.EqualTo(1));
                if (a.GetType().GetElementType() == typeof(double)) Assert.That(a.Cast<double>().All(double.IsNaN), Is.True);
                else Assert.That(a.Cast<int>().All(x => x == 0 || x == int.MinValue), Is.True);
                if (ReplaceBuffer != null) output = ReplaceBuffer(role, output);
                else for (int i = 0; i < a.Length; i++) a.SetValue(values.GetValue(i), i);
                Buffers[role] = output;
            }
            public object GetBetaAngle(int id) => R(nameof(GetBetaAngle), -12.5, id);
            public object GetElementOffsetSpecCount() => R(nameof(GetElementOffsetSpecCount), Count);
            public object GetMemberReleaseSpec(int id, int end, ref object modes, ref object springs)
            { var result = R(nameof(GetMemberReleaseSpec), 1, id, end); Fill("modes", ref modes, ReleaseModes); Fill("springs", ref springs, ReleaseValues); return result; }
            public object GetMemberReleaseSpecEx(int id, int end, ref object modes, ref object springs, ref object common, ref object directional)
            { var result = R(nameof(GetMemberReleaseSpecEx), 1, id, end); Fill("modes", ref modes, ReleaseModes); Fill("springs", ref springs, ReleaseValues); common = S("common", CommonFactor); Fill("directional", ref directional, DirectionalFactors); return result; }
            public object GetMemberLocalOffSet(int id, int end, ref object x, ref object y, ref object z)
            { var result = R(nameof(GetMemberLocalOffSet), 0, id, end); Offset(ref x, ref y, ref z); return result; }
            public object GetMemberGlobalOffSet(int id, int end, ref object x, ref object y, ref object z)
            { var result = R(nameof(GetMemberGlobalOffSet), 0, id, end); Offset(ref x, ref y, ref z); return result; }
            private void Offset(ref object x, ref object y, ref object z) { x = S("x", 1.0); y = S("y", -2.0); z = S("z", 3.0); }
            public object GetElementLocalOffset(int id, int corner, ref object x, ref object y, ref object z)
            { var result = R(nameof(GetElementLocalOffset), 0, id, corner); Offset(ref x, ref y, ref z); return result; }
            public object GetElementGlobalOffset(int id, int corner, ref object x, ref object y, ref object z)
            { var result = R(nameof(GetElementGlobalOffset), 0, id, corner); Offset(ref x, ref y, ref z); return result; }
            public object GetElementOffsetSpec(int id, int corner, ref object axis, ref object x, ref object y, ref object z)
            { var result = R(nameof(GetElementOffsetSpec), 1, id, corner); axis = S("axis", OffsetAxis); Offset(ref x, ref y, ref z); return result; }
            public object GetPlateSectionPropertyRefNo(int id) => R(nameof(GetPlateSectionPropertyRefNo), 37, id);
            public object GetBeamProperty(int id, ref object w, ref object d, ref object ax, ref object ay, ref object az, ref object ix, ref object iy, ref object iz)
            { var result = R(nameof(GetBeamProperty), 1, id); Section(ref w, ref d, ref ax, ref ay, ref az, ref ix, ref iy, ref iz); return result; }
            public object GetBeamPropertyAll(int id, ref object w, ref object d, ref object ax, ref object ay, ref object az, ref object ix, ref object iy, ref object iz, ref object flange, ref object web)
            { var result = R(nameof(GetBeamPropertyAll), 1, id); Section(ref w, ref d, ref ax, ref ay, ref az, ref ix, ref iy, ref iz); flange = S("flange", 9.0); web = S("web", 10.0); return result; }
            private void Section(ref object w, ref object d, ref object ax, ref object ay, ref object az, ref object ix, ref object iy, ref object iz)
            { w = S("width", 1.0); d = S("depth", 2.0); ax = S("area", 3.0); ay = S("shearY", 4.0); az = S("shearZ", 5.0); ix = S("ix", 6.0); iy = S("iy", 7.0); iz = S("iz", 8.0); }
            private void Basic(ref object e, ref object poisson, ref object density, ref object alpha, ref object damping)
            { e = S("e", 1.0); poisson = S("poisson", 2.0); density = S("density", 3.0); alpha = S("alpha", 4.0); damping = S("damping", 5.0); }
            private void Strength(ref object fy, ref object fu, ref object ry, ref object rt, ref object fcu)
            { fy = S("fy", 7.0); fu = S("fu", 8.0); ry = S("ry", 9.0); rt = S("rt", 10.0); fcu = S("fcu", 11.0); }
            public object GetBeamConstants(int id, ref object e, ref object poisson, ref object density, ref object alpha, ref object damping)
            { var result = R(nameof(GetBeamConstants), true, id); Basic(ref e, ref poisson, ref density, ref alpha, ref damping); return result; }
            public object GetBeamMaterialName(int id) => R(nameof(GetBeamMaterialName), " STEEL ", id);
            public object GetElementMaterialName(int id) => R(nameof(GetElementMaterialName), " CONCRETE ", id);
            public object GetIsotropicMaterialCount() => R(nameof(GetIsotropicMaterialCount), Count);
            public object GetOrthotropic2DMaterialCount() => R(nameof(GetOrthotropic2DMaterialCount), Count);
            public object GetIsotropicMaterialAssignedBeamCount(string name) => R(nameof(GetIsotropicMaterialAssignedBeamCount), Count, name);
            public object GetIsotropicMaterialAssignedBeamList(string name, ref object ids)
            { var result = R(nameof(GetIsotropicMaterialAssignedBeamList), 0, name); Fill("ids", ref ids, Enumerable.Range(0,Count).Select(i=>i==0?41:17).ToArray()); return result; }
            public object GetMaterialProperty(string name, ref object e, ref object poisson, ref object density, ref object alpha, ref object damping)
            { var result = R(nameof(GetMaterialProperty), 0, name); Basic(ref e, ref poisson, ref density, ref alpha, ref damping); return result; }
            public object GetMaterialPropertyEx(string name, ref object e, ref object poisson, ref object density, ref object alpha, ref object damping, ref object fy, ref object fu, ref object ry, ref object rt, ref object fcu)
            { var result = R(nameof(GetMaterialPropertyEx), true, name); Basic(ref e, ref poisson, ref density, ref alpha, ref damping); Strength(ref fy, ref fu, ref ry, ref rt, ref fcu); return result; }
            public object GetIsotropicMaterialProperties(int index, ref object e, ref object poisson, ref object g, ref object density, ref object alpha, ref object damping)
            { var result = R(nameof(GetIsotropicMaterialProperties), "NATIVE STEEL", index); Basic(ref e, ref poisson, ref density, ref alpha, ref damping); g=S("g",6.0); return result; }
            public object GetIsotropicMaterialPropertiesEx(int index, ref object e, ref object poisson, ref object g, ref object density, ref object alpha, ref object damping, ref object fy, ref object fu, ref object ry, ref object rt, ref object fcu)
            { var result = R(nameof(GetIsotropicMaterialPropertiesEx), "NATIVE STEEL", index); Basic(ref e, ref poisson, ref density, ref alpha, ref damping); g=S("g",6.0); Strength(ref fy, ref fu, ref ry, ref rt, ref fcu); return result; }
            public object GetIsotropicMaterialPropertiesAssigned(int number, ref object e, ref object poisson, ref object g, ref object density, ref object alpha, ref object damping, ref object assigned)
            { var result = R(nameof(GetIsotropicMaterialPropertiesAssigned), "NATIVE STEEL", number); Basic(ref e, ref poisson, ref density, ref alpha, ref damping); g=S("g",6.0); assigned=S("assigned",Assignment); return result; }
            public object GetOrthotropic2DMaterialProperties(int number, ref object e, ref object poisson, ref object g, ref object density, ref object alpha, ref object damping)
            { var result = R(nameof(GetOrthotropic2DMaterialProperties), "NATIVE ORTHO", number); Fill("e",ref e,new[]{1.0,2.0});Fill("poisson",ref poisson,new[]{3.0,4.0});Fill("g",ref g,new[]{5.0,6.0,7.0});Fill("density",ref density,new[]{8.0,9.0});Fill("alpha",ref alpha,new[]{10.0,11.0});Fill("damping",ref damping,new[]{12.0,13.0});return result; }
        }
        private static readonly string[] Names = { "GetBetaAngle", "GetElementOffsetSpecificationCount", "GetMemberReleaseSpecificationExtended", "GetMemberReleaseSpecification", "GetMemberLocalOffset", "GetMemberGlobalOffset", "GetMemberSectionProperties", "GetMemberSectionPropertiesExtended", "GetMemberMaterialConstants", "GetPlateLocalOffset", "GetPlateGlobalOffset", "GetPlateOffsetSpecification", "GetPlateSectionPropertyId", "GetIsotropicMaterialAssignedMemberCount", "GetIsotropicMaterialAssignedMemberIds", "GetMaterialProperty", "GetBeamMaterialName", "GetElementMaterialName", "GetIsotropicMaterialCount", "GetIsotropicMaterialProperty", "GetExtendedMaterialProperty", "GetExtendedIsotropicMaterialProperty", "GetIsotropicMaterialPropertiesAssigned", "GetOrthotropic2DMaterialCount", "GetOrthotropic2DMaterialProperties" };
        private static readonly string[] NativeNames = { "GetBetaAngle", "GetElementOffsetSpecCount", "GetMemberReleaseSpecEx", "GetMemberReleaseSpec", "GetMemberLocalOffSet", "GetMemberGlobalOffSet", "GetBeamProperty", "GetBeamPropertyAll", "GetBeamConstants", "GetElementLocalOffset", "GetElementGlobalOffset", "GetElementOffsetSpec", "GetPlateSectionPropertyRefNo", "GetIsotropicMaterialAssignedBeamCount", "GetIsotropicMaterialAssignedBeamList", "GetMaterialProperty", "GetBeamMaterialName", "GetElementMaterialName", "GetIsotropicMaterialCount", "GetIsotropicMaterialProperties", "GetMaterialPropertyEx", "GetIsotropicMaterialPropertiesEx", "GetIsotropicMaterialPropertiesAssigned", "GetOrthotropic2DMaterialCount", "GetOrthotropic2DMaterialProperties" };
        private static MethodInfo Method(string name) => typeof(IOSProperty).GetMethod(name);
        private static OpenStaadWrapper Wrap(Native native) => new OpenStaadWrapper(new Root { Property = native });
        private static object[] Args(string name) => Method(name).GetParameters().Select(p => p.ParameterType == typeof(string) ? (object)" Steel Case " :
            p.ParameterType == typeof(MemberEnd) ? MemberEnd.End : p.Name == "maximumResults" ? 100000 : p.Name == "materialIndex" ? 0 : p.Name == "cornerIndex" ? 2 : 17).ToArray();
        private static object Invoke(IOSProperty property, string name, object[] args = null)
        {
            try { return Method(name).Invoke(property, args ?? Args(name)); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
        }
        private static object Prop(object result, string name) => result.GetType().GetProperty(name).GetValue(result);
        private static void Status(object result, PropertyQueryStatus expected)
        {
            Assert.That(Prop(result,"Status"),Is.EqualTo(expected));Assert.That(Prop(result,"IsSuccessful"),Is.EqualTo(expected==PropertyQueryStatus.Success));
            if(expected!=PropertyQueryStatus.Success)Assert.That(()=>Prop(result,"Data"),Throws.TypeOf<TargetInvocationException>().With.InnerException.TypeOf<InvalidOperationException>());
        }
        [TestCaseSource(nameof(Names))]
        public void EveryQueryDispatchesTheIntendedNativeMemberOnTheCallingThread(string name)
        {
            var native=new Native();using(var wrapper=Wrap(native))
            {
                var result=Invoke(wrapper.Property,name);Status(result,PropertyQueryStatus.Success);
                string expected=NativeNames[Array.IndexOf(Names,name)];Assert.That(native.Calls,Does.Contain(expected));
                Assert.That(Prop(result,"NativeFunction"),Is.EqualTo(expected));
                Assert.That(native.Threads.All(t=>t==Thread.CurrentThread.ManagedThreadId),Is.True);
                Assert.That(native.Calls.Any(c=>c.StartsWith("Set")||c.StartsWith("Create")||c.StartsWith("Assign")||c.StartsWith("Analyze")||c.StartsWith("Save")),Is.False);
            }
        }
        [TestCaseSource(nameof(Names))]
        public void EveryQueryRejectsDisposedOrReboundReceiversBeforeDispatch(string name)
        {
            var native=new Native();var root=new Root{Property=native};var wrapper=new OpenStaadWrapper(root);var property=wrapper.Property;wrapper.Dispose();native.Calls.Clear();
            Assert.Throws<InvalidOperationException>(()=>Invoke(property,name));Assert.That(native.Calls,Is.Empty);
            native=new Native();root=new Root{Property=native};using(wrapper=new OpenStaadWrapper(root))
            {property=wrapper.Property;root.Property=new Native();wrapper.RefreshComObjects();native.Calls.Clear();Assert.Throws<InvalidOperationException>(()=>Invoke(property,name));Assert.That(native.Calls,Is.Empty);Status(Invoke(wrapper.Property,name),PropertyQueryStatus.Success);}
        }
        [TestCaseSource(nameof(Names))]
        public void EveryQueryPropagatesOriginalTransportExceptions(string name)
        {
            var native=new Native();var error=new COMException("native transport",unchecked((int)0x80004005));native.OnCall=op=>{throw error;};
            using(var wrapper=Wrap(native))Assert.That(Assert.Throws<COMException>(()=>Invoke(wrapper.Property,name)),Is.SameAs(error));
        }
        [TestCaseSource(nameof(Names))]
        public void EveryQueryHasMatchingDescriptiveManagedSignaturesAndCompleteComparativeIntelliSense(string name)
        {
            var method=Method(name);var concrete=typeof(OSPropertyAdapter).GetMethod(name,method.GetParameters().Select(p=>p.ParameterType).ToArray());
            Assert.That(concrete,Is.Not.Null);Assert.That(concrete.ReturnType,Is.EqualTo(method.ReturnType));Assert.That(concrete.GetParameters().Select(p=>p.Name),Is.EqualTo(method.GetParameters().Select(p=>p.Name)));
            Assert.That(method.GetParameters().Any(p=>p.ParameterType.IsByRef||p.ParameterType.IsArray),Is.False);
            var xml=XDocument.Load(Path.ChangeExtension(typeof(IOSProperty).Assembly.Location,".xml"));
            var entries=xml.Descendants("member").Where(m=>((string)m.Attribute("name")).StartsWith("M:StaadPro.Interop.Adapters.")&&(((string)m.Attribute("name")).EndsWith("."+name)||((string)m.Attribute("name")).Contains("."+name+"("))).ToArray();
            Assert.That(entries.Length,Is.EqualTo(2));Assert.That(entries[0].Elements().Select(e=>e.ToString()),Is.EqualTo(entries[1].Elements().Select(e=>e.ToString())));
            foreach(string tag in new[]{"summary","returns","remarks","example"})Assert.That(entries[0].Element(tag)?.Value.Trim(),Is.Not.Null.And.Not.Empty,name+" "+tag);
            Assert.That(entries[0].Element("remarks").Value,Does.Contain("When to use:").And.Contain("No model mutation").And.Contain("COM apartment"));
            Assert.That(entries[0].Elements("param").Select(p=>(string)p.Attribute("name")),Is.EqualTo(method.GetParameters().Select(p=>p.Name)));
            Assert.That(entries[0].Element("example").Value,Does.Contain("property."+name+"(").And.Contain("if (result.IsSuccessful)"));
        }
        public static IEnumerable<TestCaseData> InvalidInputs
        {
            get
            {
                foreach(string name in Names)
                    for(int i=0;i<Method(name).GetParameters().Length;i++)
                        yield return new TestCaseData(name,i);
            }
        }
        [TestCaseSource(nameof(InvalidInputs))]
        public void InvalidArgumentsNeverReachNativeCode(string name,int parameter)
        {
            var native=new Native();using(var wrapper=Wrap(native))
            {var args=Args(name);var type=Method(name).GetParameters()[parameter].ParameterType;args[parameter]=type==typeof(string)?null:type==typeof(MemberEnd)?(object)(MemberEnd)99:-1;Assert.That(()=>Invoke(wrapper.Property,name,args),Throws.InstanceOf<ArgumentException>());Assert.That(native.Calls,Is.Empty);}
        }
        [Test]
        public void NewPayloadPropertiesAreNamedImmutableAndFullyDocumented()
        {
            var types=typeof(IOSProperty).Assembly.GetExportedTypes().Where(t=>t.Namespace=="StaadPro.Interop.Models.Property"||t.Namespace=="StaadPro.Interop.Enums.Property").ToArray();
            Assert.That(types.Length,Is.EqualTo(36));var docs=XDocument.Load(Path.ChangeExtension(typeof(IOSProperty).Assembly.Location,".xml")).Descendants("member").ToDictionary(m=>(string)m.Attribute("name"));
            foreach(var type in types)
            {
                Assert.That(docs["T:"+type.FullName].Element("summary")?.Value,Is.Not.Null.And.Not.Empty);
                foreach(var p in type.GetProperties(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly))
                {Assert.That(p.CanWrite,Is.False);Assert.That(p.PropertyType.IsArray,Is.False);Assert.That(p.PropertyType,Is.Not.EqualTo(typeof(object)));Assert.That(docs["P:"+type.FullName+"."+p.Name].Element("summary")?.Value,Is.Not.Null.And.Not.Empty);}
                if(type.IsEnum)foreach(string field in Enum.GetNames(type))Assert.That(docs["F:"+type.FullName+"."+field].Element("summary")?.Value,Is.Not.Null.And.Not.Empty);
            }
        }

        [TestCase(-3001,PropertyQueryStatus.NotFound)]
        public void BetaErrorDoesNotBecomeAnAngle(int code,PropertyQueryStatus status)
        {
            var native=new Native();native.Responses["GetBetaAngle"]=new Queue<object>(new object[]{code});using(var wrapper=Wrap(native)){var result=wrapper.Property.GetBetaAngle(17);Status(result,status);Assert.That(result.NativeReturnCode,Is.EqualTo(code));}
        }
        [TestCase(-45.0)] [TestCase(0.0)] [TestCase(360.0)]
        public void FiniteSignedBetaAnglesArePreserved(double angle)
        {
            var native=new Native();native.Responses["GetBetaAngle"]=new Queue<object>(new object[]{angle});using(var wrapper=Wrap(native))Assert.That(wrapper.Property.GetBetaAngle(17).Data,Is.EqualTo(angle));
        }
        [Test]
        public void IntegerBetaZeroIsValidButBooleanAngleIsRejected()
        {
            var native=new Native();native.Responses["GetBetaAngle"]=new Queue<object>(new object[]{0,true});using(var wrapper=Wrap(native)){Assert.That(wrapper.Property.GetBetaAngle(17).Data,Is.Zero);Assert.Throws<InvalidOperationException>(()=>wrapper.Property.GetBetaAngle(17));}
        }
        public static IEnumerable<TestCaseData> OffsetFailures
        {
            get
            {
                foreach(string name in new[]{"GetMemberLocalOffset","GetMemberGlobalOffset"})
                {yield return new TestCaseData(name,-3001,PropertyQueryStatus.NotFound);yield return new TestCaseData(name,-6026,PropertyQueryStatus.InvalidReference);yield return new TestCaseData(name,-6027,PropertyQueryStatus.NoSpecification);}
                foreach(string name in new[]{"GetPlateLocalOffset","GetPlateGlobalOffset","GetPlateOffsetSpecification"})
                {yield return new TestCaseData(name,-4001,PropertyQueryStatus.NotFound);if(name!="GetPlateOffsetSpecification")yield return new TestCaseData(name,-6027,PropertyQueryStatus.NoSpecification);}
            }
        }
        public static IEnumerable<TestCaseData> ForeignOffsetStatuses
        {
            get
            {
                foreach (string name in new[] { "GetMemberLocalOffset", "GetMemberGlobalOffset" })
                    yield return new TestCaseData(name, -4001);
                foreach (string name in new[] { "GetPlateLocalOffset", "GetPlateGlobalOffset", "GetPlateOffsetSpecification" })
                {
                    yield return new TestCaseData(name, -3001);
                    yield return new TestCaseData(name, -6026);
                }
                yield return new TestCaseData("GetPlateOffsetSpecification", -6027);
            }
        }
        [TestCaseSource(nameof(ForeignOffsetStatuses))]
        public void OffsetStatusesAreNotBorrowedFromSimilarNativeQueries(string name, int code)
        {
            var native = new Native();
            native.Responses[NativeNames[Array.IndexOf(Names, name)]] = new Queue<object>(new object[] { code });
            using (var wrapper = Wrap(native))
                Assert.Throws<InvalidOperationException>(() => Invoke(wrapper.Property, name),
                    "Only status codes documented for this specific native query may be interpreted.");
        }
        [TestCaseSource(nameof(OffsetFailures))]
        public void MissingOffsetAndInvalidReferencesKeepExactNativeOutcomes(string name,int code,PropertyQueryStatus expected)
        {
            var native=new Native();native.Responses[NativeNames[Array.IndexOf(Names,name)]]=new Queue<object>(new object[]{code});native.ReplaceScalar=(role,value)=>double.NaN;
            using(var wrapper=Wrap(native)){var result=Invoke(wrapper.Property,name);Status(result,expected);Assert.That(Prop(result,"NativeReturnCode"),Is.EqualTo(code));}
        }
        [Test]
        public void ZeroOffsetAndNoSpecificationAreDifferentAndVectorCopiesAreIndependent()
        {
            var native=new Native();native.ReplaceScalar=(role,value)=>role=="axis"?value:0.0;using(var wrapper=Wrap(native))
            {
                var zero=wrapper.Property.GetMemberGlobalOffset(17,MemberEnd.Start);Status(zero,PropertyQueryStatus.Success);Assert.That(zero.Data.Length,Is.Zero);
                var vector=zero.Data;vector.X=123;Assert.That(zero.Data.X,Is.Zero);
                var spec=wrapper.Property.GetPlateOffsetSpecification(17,1);Assert.That(spec.Data.Axis,Is.EqualTo(ElementNodeOffsetAxis.Z));Assert.That(spec.Data.Offset.Length,Is.Zero);
                native.Responses["GetElementOffsetSpec"]=new Queue<object>(new object[]{0});native.ReplaceScalar=(role,value)=>null;
                Status(wrapper.Property.GetPlateOffsetSpecification(17,1),PropertyQueryStatus.NoSpecification);
            }
        }
        [TestCase(-1)] [TestCase(3)] [TestCase(int.MinValue)]
        public void UnknownPlateOffsetSelectorsAreRejected(int selector)
        {
            var native=new Native{OffsetAxis=selector};using(var wrapper=Wrap(native))Assert.Throws<InvalidOperationException>(()=>wrapper.Property.GetPlateOffsetSpecification(17,1));
        }
        [TestCase(0)] [TestCase(5)]
        public void InvalidPlateCornerIsRejectedBeforeNativeAccess(int corner)
        {
            var native=new Native();using(var wrapper=Wrap(native)){Assert.Throws<ArgumentOutOfRangeException>(()=>wrapper.Property.GetPlateLocalOffset(17,corner));Assert.Throws<ArgumentOutOfRangeException>(()=>wrapper.Property.GetPlateGlobalOffset(17,corner));Assert.Throws<ArgumentOutOfRangeException>(()=>wrapper.Property.GetPlateOffsetSpecification(17,corner));Assert.That(native.Calls,Is.Empty);}
        }
        [TestCase(-4001,PropertyQueryStatus.NotFound)] [TestCase(-6022,PropertyQueryStatus.NotAssigned)]
        public void PlatePropertyReferenceReportsAbsenceSeparately(int code,PropertyQueryStatus status)
        {
            var native=new Native();native.Responses["GetPlateSectionPropertyRefNo"]=new Queue<object>(new object[]{code});using(var wrapper=Wrap(native))Status(wrapper.Property.GetPlateSectionPropertyId(17),status);
        }
        [TestCase(0)] [TestCase(-1)]
        public void UndocumentedPlatePropertyReferencesAreRejected(int value)
        {
            var native=new Native();native.Responses["GetPlateSectionPropertyRefNo"]=new Queue<object>(new object[]{value});using(var wrapper=Wrap(native))Assert.Throws<InvalidOperationException>(()=>wrapper.Property.GetPlateSectionPropertyId(17));
        }
        [Test]
        public void MissingDocumentedPlatePropertyMemberReturnsUnsupportedWithoutFallback()
        {
            using(var wrapper=new OpenStaadWrapper(new Root{Property=new object()}))Status(wrapper.Property.GetPlateSectionPropertyId(17),PropertyQueryStatus.Unsupported);
            var native=new Native();native.Errors["GetPlateSectionPropertyRefNo"]=new COMException("missing",unchecked((int)0x80020003));using(var wrapper=Wrap(native)){Status(wrapper.Property.GetPlateSectionPropertyId(17),PropertyQueryStatus.Unsupported);Assert.That(native.Calls,Is.EqualTo(new[]{"GetPlateSectionPropertyRefNo"}));}
        }
        [Test]
        public void BasicAndExtendedReleasesPreserveEveryModeAndOnlyApplicableNumericValues()
        {
            var native=new Native();using(var wrapper=Wrap(native))
            {
                var basic=wrapper.Property.GetMemberReleaseSpecification(17,MemberEnd.Start).Data;
                Assert.That(basic.TranslationX.Mode,Is.EqualTo(MemberDofReleaseMode.Restrained));Assert.That(basic.TranslationX.NativeParameter,Is.Null);
                Assert.That(basic.TranslationY.Mode,Is.EqualTo(MemberDofReleaseMode.Released));Assert.That(basic.TranslationY.NativeParameter,Is.Null);
                Assert.That(basic.TranslationZ.Mode,Is.EqualTo(MemberDofReleaseMode.Spring));Assert.That(basic.TranslationZ.NativeParameter,Is.EqualTo(12.5));
                Assert.That(basic.RotationX.Mode,Is.EqualTo(MemberDofReleaseMode.CommonPartialMoment));Assert.That(basic.RotationY.Mode,Is.EqualTo(MemberDofReleaseMode.DirectionalPartialMoment));
                Assert.That(basic.RotationY.NativeParameter,Is.EqualTo(0.25));Assert.That(basic.CommonPartialMomentFactor,Is.Null);Assert.That(basic.DirectionalPartialMomentFactors,Is.Null);Assert.That(basic.IncludesExtendedOutputs,Is.False);
                var extended=wrapper.Property.GetMemberReleaseSpecificationExtended(17,MemberEnd.End).Data;
                Assert.That(extended.CommonPartialMomentFactor,Is.EqualTo(0.75));Assert.That(extended.DirectionalPartialMomentFactors.MomentX,Is.Null);Assert.That(extended.DirectionalPartialMomentFactors.MomentY,Is.EqualTo(0.8));Assert.That(extended.DirectionalPartialMomentFactors.MomentZ,Is.EqualTo(0.9));Assert.That(extended.IncludesExtendedOutputs,Is.True);
                native.ReleaseModes[2]=0;native.ReleaseValues[2]=99;Assert.That(extended.TranslationZ.Mode,Is.EqualTo(MemberDofReleaseMode.Spring));Assert.That(extended.TranslationZ.NativeParameter,Is.EqualTo(12.5));
                Assert.That(native.Arguments.First(),Is.EqualTo(new object[]{17,0}));Assert.That(native.Arguments.Last(),Is.EqualTo(new object[]{17,1}));
            }
        }
        [Test]
        public void UnusedExtendedReleaseOutputsAreNotInventedAsZeroOrRequiredToBeWritten()
        {
            var native=new Native{ReleaseModes=new int[6],ReleaseValues=Enumerable.Repeat(double.NaN,6).ToArray(),CommonFactor=double.NaN,DirectionalFactors=Enumerable.Repeat(double.NaN,3).ToArray()};
            using(var wrapper=Wrap(native)){var data=wrapper.Property.GetMemberReleaseSpecificationExtended(17,MemberEnd.Start).Data;Assert.That(data.CommonPartialMomentFactor,Is.Null);Assert.That(data.DirectionalPartialMomentFactors,Is.Null);Assert.That(data.TranslationX.NativeParameter,Is.Null);}
        }
        [Test]
        public void ExtendedDirectionalFactorsCanBeReturnedWithoutUnusedBasicParameterSlots()
        {
            var native=new Native();native.ReleaseValues[4]=double.NaN;native.ReleaseValues[5]=double.NaN;
            using(var wrapper=Wrap(native)){var data=wrapper.Property.GetMemberReleaseSpecificationExtended(17,MemberEnd.Start).Data;Assert.That(data.RotationY.NativeParameter,Is.Null);Assert.That(data.DirectionalPartialMomentFactors.MomentY,Is.EqualTo(0.8));Assert.Throws<InvalidOperationException>(()=>wrapper.Property.GetMemberReleaseSpecification(17,MemberEnd.Start));}
        }
        [TestCase(2)] [TestCase(-4)] [TestCase(int.MinValue)]
        public void UnknownOrUnwrittenReleaseModesAreRejected(int mode)
        {
            var native=new Native();native.ReleaseModes[0]=mode;using(var wrapper=Wrap(native))Assert.Throws<InvalidOperationException>(()=>wrapper.Property.GetMemberReleaseSpecification(17,MemberEnd.Start));
        }
        [TestCase("common")] [TestCase("springs")] [TestCase("directional")]
        public void ApplicableReleaseParametersMustBeWrittenAndFinite(string role)
        {
            var native=new Native();if(role=="common")native.CommonFactor=double.NaN;else if(role=="springs")native.ReleaseValues[2]=double.NaN;else native.DirectionalFactors[1]=double.NaN;
            using(var wrapper=Wrap(native))Assert.Throws<InvalidOperationException>(()=>wrapper.Property.GetMemberReleaseSpecificationExtended(17,MemberEnd.Start));
        }
        [Test]
        public void ShortSectionPacketDoesNotFabricateThicknessOrRelabelInertiaSlots()
        {
            var native=new Native();using(var wrapper=Wrap(native))
            {
                var basic=wrapper.Property.GetMemberSectionProperties(17).Data;Assert.That(basic.Width,Is.EqualTo(1));Assert.That(basic.Depth,Is.EqualTo(2));Assert.That(basic.AxialArea,Is.EqualTo(3));Assert.That(basic.NativeIx,Is.EqualTo(6));Assert.That(basic.NativeIy,Is.EqualTo(7));Assert.That(basic.NativeIz,Is.EqualTo(8));Assert.That(basic.FlangeThickness,Is.Null);Assert.That(basic.WebThickness,Is.Null);
                var full=wrapper.Property.GetMemberSectionPropertiesExtended(17).Data;Assert.That(full.FlangeThickness,Is.EqualTo(9));Assert.That(full.WebThickness,Is.EqualTo(10));
            }
        }
        [Test]
        public void NamedMaterialAndIndexedMaterialHaveDistinctSelectorsAndActualShearModulus()
        {
            var native=new Native();using(var wrapper=Wrap(native))
            {
                var named=wrapper.Property.GetMaterialProperty(" Steel Case ").Data;Assert.That(named.Name,Is.EqualTo(" Steel Case "));Assert.That(named.Constants.ElasticModulus,Is.EqualTo(1));Assert.That(named.Constants.PoissonRatio,Is.EqualTo(2));Assert.That(named.Constants.WeightDensity,Is.EqualTo(3));Assert.That(named.Constants.ThermalExpansionCoefficient,Is.EqualTo(4));Assert.That(named.Constants.DampingRatio,Is.EqualTo(5));Assert.That(named.Constants.ShearModulus,Is.Null);Assert.That(named.Strengths,Is.Null);Assert.That(named.AssignmentState,Is.Null);
                var indexed=wrapper.Property.GetIsotropicMaterialProperty(0).Data;Assert.That(indexed.Name,Is.EqualTo("NATIVE STEEL"));Assert.That(indexed.Constants.ShearModulus,Is.EqualTo(6));Assert.That(indexed.Constants.ShearModulus,Is.Not.EqualTo(indexed.Constants.ElasticModulus/(2*(1+indexed.Constants.PoissonRatio))));
                Assert.That(native.Arguments[native.Calls.IndexOf("GetMaterialProperty")],Is.EqualTo(new object[]{" Steel Case "}));Assert.That(native.Arguments[native.Calls.IndexOf("GetIsotropicMaterialProperties")],Is.EqualTo(new object[]{0}));
                var extended=wrapper.Property.GetExtendedIsotropicMaterialProperty(0).Data;Assert.That(extended.Strengths.YieldStress,Is.EqualTo(7));Assert.That(extended.Strengths.UltimateTensileStrength,Is.EqualTo(8));Assert.That(extended.Strengths.YieldStrengthRatio,Is.EqualTo(9));Assert.That(extended.Strengths.TensileStrengthRatio,Is.EqualTo(10));Assert.That(extended.Strengths.CompressiveStrength,Is.EqualTo(11));
                Assert.That(wrapper.Property.GetExtendedMaterialProperty("STEEL").Data.Constants.ShearModulus,Is.Null);Assert.That(wrapper.Property.GetMemberMaterialConstants(17).Data.ShearModulus,Is.Null);
            }
        }
        [TestCase(1.0,MaterialAssignmentState.Unassigned)] [TestCase(2.0,MaterialAssignmentState.Assigned)]
        [TestCase(1,MaterialAssignmentState.Unassigned)] [TestCase(2,MaterialAssignmentState.Assigned)]
        public void AssignmentOutputIsDecodedAsNativeOneTwoRatherThanBoolean(object state,MaterialAssignmentState expected)
        {
            var native=new Native{Assignment=state};using(var wrapper=Wrap(native)){var result=wrapper.Property.GetIsotropicMaterialPropertiesAssigned(0);Assert.That(result.Data.AssignmentState,Is.EqualTo(expected));Assert.That(native.Arguments.Single(),Is.EqualTo(new object[]{0}));Assert.That(native.Calls,Does.Not.Contain("GetIsotropicMaterialCount"));}
        }
        [TestCase(0)] [TestCase(3)] [TestCase(1.5)] [TestCase(true)] [TestCase(null)] [TestCase(double.NaN)]
        public void InvalidAssignmentOutputIsNotExposed(object flag)
        {
            var native=new Native{Assignment=flag};using(var wrapper=Wrap(native))Assert.Throws<InvalidOperationException>(()=>wrapper.Property.GetIsotropicMaterialPropertiesAssigned(2));
        }
        [Test]
        public void OrthotropicArraysUseThirteenNamedValuesWithoutGuessedPhysicalAxes()
        {
            var native=new Native();using(var wrapper=Wrap(native))
            {
                var data=wrapper.Property.GetOrthotropic2DMaterialProperties(7).Data;
                Assert.That(data.Name,Is.EqualTo("NATIVE ORTHO"));Assert.That(data.ElasticModulusFirstDirection,Is.EqualTo(1));Assert.That(data.ElasticModulusSecondDirection,Is.EqualTo(2));Assert.That(data.PoissonRatioFirstDirection,Is.EqualTo(3));Assert.That(data.PoissonRatioSecondDirection,Is.EqualTo(4));Assert.That(data.ShearModulusFirstPlane,Is.EqualTo(5));Assert.That(data.ShearModulusSecondPlane,Is.EqualTo(6));Assert.That(data.ShearModulusThirdPlane,Is.EqualTo(7));Assert.That(data.WeightDensityFirstDirection,Is.EqualTo(8));Assert.That(data.WeightDensitySecondDirection,Is.EqualTo(9));Assert.That(data.ThermalExpansionFirstDirection,Is.EqualTo(10));Assert.That(data.ThermalExpansionSecondDirection,Is.EqualTo(11));Assert.That(data.DampingRatioFirstDirection,Is.EqualTo(12));Assert.That(data.DampingRatioSecondDirection,Is.EqualTo(13));
                Assert.That(native.Arguments.Single(),Is.EqualTo(new object[]{7}));Assert.That(native.Calls,Does.Not.Contain("GetOrthotropic2DMaterialCount"));
                ((double[])native.Buffers["e"])[0]=99;Assert.That(data.ElasticModulusFirstDirection,Is.EqualTo(1));
            }
        }
        [TestCase("GetBeamMaterialName")] [TestCase("GetElementMaterialName")]
        public void MaterialNamesRetainWhitespaceAndDoNotTreatNullOrEmptyAsKnownUnassigned(string name)
        {
            var native=new Native();string op=NativeNames[Array.IndexOf(Names,name)];native.Responses[op]=new Queue<object>(new object[]{" Steel Mixed ",null,"",false});using(var wrapper=Wrap(native)){Assert.That(Prop(Invoke(wrapper.Property,name),"Data"),Is.EqualTo(" Steel Mixed "));Status(Invoke(wrapper.Property,name),PropertyQueryStatus.EmptyOrUnavailable);Status(Invoke(wrapper.Property,name),PropertyQueryStatus.EmptyOrUnavailable);Assert.Throws<InvalidOperationException>(()=>Invoke(wrapper.Property,name));}
        }
        [TestCase("GetIsotropicMaterialProperty")] [TestCase("GetExtendedIsotropicMaterialProperty")]
        [TestCase("GetIsotropicMaterialPropertiesAssigned")] [TestCase("GetOrthotropic2DMaterialProperties")]
        public void NativeNullNamesExposeNoMaterialDefaults(string name)
        {
            var native=new Native();string op=NativeNames[Array.IndexOf(Names,name)];native.Responses[op]=new Queue<object>(new object[]{null});native.ReplaceScalar=(role,value)=>double.NaN;
            using(var wrapper=Wrap(native))Status(Invoke(wrapper.Property,name),PropertyQueryStatus.NotFound);
        }
        [TestCase("GetIsotropicMaterialProperty")] [TestCase("GetExtendedIsotropicMaterialProperty")]
        public void IndexedMaterialsAreBoundedAndDiscardObservedCountChanges(string name)
        {
            var native=new Native{Count=0};using(var wrapper=Wrap(native)){Status(Invoke(wrapper.Property,name),PropertyQueryStatus.NotFound);Assert.That(native.Calls,Is.EqualTo(new[]{"GetIsotropicMaterialCount"}));}
            native=new Native();native.Responses["GetIsotropicMaterialCount"]=new Queue<object>(new object[]{2,3});using(var wrapper=Wrap(native))Status(Invoke(wrapper.Property,name),PropertyQueryStatus.StateChanged);
        }
        [Test]
        public void AssignedIdsAreBoundedReadOnlyOrderedSnapshotsWithNoDeduplication()
        {
            var native=new Native{Count=3};using(var wrapper=Wrap(native))
            {
                var result=wrapper.Property.GetIsotropicMaterialAssignedMemberIds(" Case ");Assert.That(result.Data,Is.EqualTo(new[]{41,17,17}));var list=(IList<int>)result.Data;Assert.That(list.IsReadOnly,Is.True);((int[])native.Buffers["ids"])[0]=99;Assert.That(result.Data.First(),Is.EqualTo(41));
                native.Calls.Clear();Status(wrapper.Property.GetIsotropicMaterialAssignedMemberIds("Case",2),PropertyQueryStatus.LimitExceeded);Assert.That(native.Calls,Is.EqualTo(new[]{"GetIsotropicMaterialAssignedBeamCount"}));
            }
        }
        [Test]
        public void AssignedIdsRecheckSizingAndSkipEmptyFetch()
        {
            var native=new Native{Count=0};using(var wrapper=Wrap(native)){var result=wrapper.Property.GetIsotropicMaterialAssignedMemberIds("STEEL");Assert.That(result.Data,Is.Empty);Assert.That(native.Calls,Does.Not.Contain("GetIsotropicMaterialAssignedBeamList"));}
            native=new Native();native.Responses["GetIsotropicMaterialAssignedBeamCount"]=new Queue<object>(new object[]{2,3});using(var wrapper=Wrap(native))Status(wrapper.Property.GetIsotropicMaterialAssignedMemberIds("STEEL"),PropertyQueryStatus.StateChanged);
        }
        [TestCase("GetIsotropicMaterialAssignedMemberCount")] [TestCase("GetIsotropicMaterialAssignedMemberIds")] [TestCase("GetMaterialProperty")]
        public void MissingNamedMaterialPreservesTheNativeSentinel(string name)
        {
            var native=new Native();string op=name=="GetIsotropicMaterialAssignedMemberIds"?"GetIsotropicMaterialAssignedBeamCount":NativeNames[Array.IndexOf(Names,name)];native.Responses[op]=new Queue<object>(new object[]{-6023});using(var wrapper=Wrap(native)){var result=Invoke(wrapper.Property,name);Status(result,PropertyQueryStatus.NotFound);Assert.That(Prop(result,"NativeReturnCode"),Is.EqualTo(-6023));}
        }
        [TestCase("GetMemberReleaseSpecification",0)] [TestCase("GetMemberReleaseSpecificationExtended",0)]
        [TestCase("GetMemberSectionProperties",0)] [TestCase("GetMemberSectionPropertiesExtended",0)]
        [TestCase("GetMemberMaterialConstants",false)] [TestCase("GetExtendedMaterialProperty",false)]
        public void FailureFlagsNeverPublishInitializedOrDefaultData(string name,object failure)
        {
            var native=new Native();native.Responses[NativeNames[Array.IndexOf(Names,name)]]=new Queue<object>(new[]{failure});native.ReplaceScalar=(role,value)=>double.NaN;
            using(var wrapper=Wrap(native)){var result=Invoke(wrapper.Property,name);Status(result,PropertyQueryStatus.NativeFailure);Assert.That(Prop(result,"NativeBooleanReturn"),Is.EqualTo(failure is bool b?(bool?)b:null));Assert.That(Prop(result,"NativeReturnCode"),Is.EqualTo(failure is int i?(int?)i:null));}
        }
        [TestCase("GetElementOffsetSpecificationCount")] [TestCase("GetIsotropicMaterialCount")] [TestCase("GetOrthotropic2DMaterialCount")]
        public void CountsAcceptZeroButRejectNegativeValues(string name)
        {
            var native=new Native();native.Responses[NativeNames[Array.IndexOf(Names,name)]]=new Queue<object>(new object[]{0,-1});using(var wrapper=Wrap(native)){Assert.That(Prop(Invoke(wrapper.Property,name),"Data"),Is.Zero);Assert.Throws<InvalidOperationException>(()=>Invoke(wrapper.Property,name));}
        }
        [TestCase("")] [TestCase(" ")] [TestCase("STEEL\0OTHER")]
        public void BlankOrNulMaterialNamesNeverReachNativeCode(string materialName)
        {
            var native=new Native();using(var wrapper=Wrap(native)){Assert.That(()=>wrapper.Property.GetMaterialProperty(materialName),Throws.InstanceOf<ArgumentException>());Assert.That(native.Calls,Is.Empty);}
        }

        [TestCaseSource(nameof(Names))]
        public void EveryQueryRejectsAnUnsupportedNativeResponseType(string name)
        {
            var native=new Native();native.Responses[NativeNames[Array.IndexOf(Names,name)]]=new Queue<object>(new[]{new object()});using(var wrapper=Wrap(native))Assert.Throws<InvalidOperationException>(()=>Invoke(wrapper.Property,name));
        }
        private static readonly string[] ScalarReaders={"GetMemberLocalOffset","GetMemberGlobalOffset","GetPlateLocalOffset","GetPlateGlobalOffset","GetPlateOffsetSpecification","GetMemberSectionProperties","GetMemberSectionPropertiesExtended","GetMemberMaterialConstants","GetMaterialProperty","GetIsotropicMaterialProperty","GetExtendedMaterialProperty","GetExtendedIsotropicMaterialProperty","GetIsotropicMaterialPropertiesAssigned","GetMemberReleaseSpecificationExtended"};
        public static IEnumerable<TestCaseData> MalformedScalars
        {
            get
            {
                foreach(string name in ScalarReaders)
                    foreach(string bad in new[]{"null","nan","infinity","text","boolean","decimal"})
                        yield return new TestCaseData(name,bad);
            }
        }
        [TestCaseSource(nameof(MalformedScalars))]
        public void ApplicableNativeScalarsMustHaveTheReviewedTypeAndFiniteValues(string name,string bad)
        {
            var native=new Native();object value=bad=="null"?null:bad=="nan"?(object)double.NaN:bad=="infinity"?double.PositiveInfinity:bad=="text"?(object)"1":bad=="boolean"?true:(object)1.0m;
            native.ReplaceScalar=(role,original)=>value;
            using(var wrapper=Wrap(native))Assert.Throws<InvalidOperationException>(()=>Invoke(wrapper.Property,name));
        }
        public static IEnumerable<TestCaseData> MalformedArrays
        {
            get
            {
                foreach(string name in new[]{"GetMemberReleaseSpecification","GetMemberReleaseSpecificationExtended","GetOrthotropic2DMaterialProperties","GetIsotropicMaterialAssignedMemberIds"})
                {
                    string[] roles=name=="GetMemberReleaseSpecification"?new[]{"modes","springs"}:name=="GetMemberReleaseSpecificationExtended"?new[]{"modes","springs","directional"}:name=="GetOrthotropic2DMaterialProperties"?new[]{"e","poisson","g","density","alpha","damping"}:new[]{"ids"};
                    foreach(string role in roles)
                        foreach(string shape in new[]{"null","wrong-type","short","long","rank","unwritten","bad-value"})
                            yield return new TestCaseData(name,role,shape);
                }
            }
        }
        private static Array ValidArray(Native native,string role,object input,int lower=0,bool single=false)
        {
            var a=(Array)input;Type element=a.GetType().GetElementType();if(single&&element==typeof(double))element=typeof(float);
            var copy=Array.CreateInstance(element,new[]{a.Length},new[]{lower});
            for(int i=0;i<a.Length;i++)
            {
                object value=role=="modes"?(object)native.ReleaseModes[i]:role=="springs"?native.ReleaseValues[i]:role=="directional"?native.DirectionalFactors[i]:role=="ids"?(object)(i==0?41:17):(i+0.5);
                if(element==typeof(float))value=(float)(double)value;copy.SetValue(value,i+lower);
            }
            return copy;
        }
        [TestCaseSource(nameof(MalformedArrays))]
        public void EveryOutputBufferRejectsMalformedShapeTypeOrApplicableData(string name,string role,string shape)
        {
            var native=new Native();native.ReplaceBuffer=(actual,input)=>
            {
                if(actual!=role)return ValidArray(native,actual,input);var a=(Array)input;var element=a.GetType().GetElementType();
                if(shape=="null")return null;
                if(shape=="wrong-type")return new object[a.Length];
                if(shape=="short")return Array.CreateInstance(element,a.Length-1);
                if(shape=="long")return Array.CreateInstance(element,a.Length+1);
                if(shape=="rank")return Array.CreateInstance(element,a.Length,1);
                if(shape=="unwritten")return input;
                var valid=ValidArray(native,actual,input);int bad=role=="springs"?2:role=="directional"?1:0;valid.SetValue(element==typeof(double)?(object)double.PositiveInfinity:role=="modes"?99:0,bad);return valid;
            };
            using(var wrapper=Wrap(native))Assert.Throws<InvalidOperationException>(()=>Invoke(wrapper.Property,name));
        }
        [TestCase("GetMemberReleaseSpecification")] [TestCase("GetMemberReleaseSpecificationExtended")]
        [TestCase("GetOrthotropic2DMaterialProperties")] [TestCase("GetIsotropicMaterialAssignedMemberIds")]
        public void ReviewedArrayTypesSupportNonzeroLowerBoundsAndOwnTheirData(string name)
        {
            var native=new Native();native.ReplaceBuffer=(role,input)=>ValidArray(native,role,input,5);
            using(var wrapper=Wrap(native))
            {
                var result=Invoke(wrapper.Property,name);Status(result,PropertyQueryStatus.Success);
                if(name=="GetIsotropicMaterialAssignedMemberIds"){Assert.That((IEnumerable<int>)Prop(result,"Data"),Is.EqualTo(new[]{41,17}));((Array)native.Buffers["ids"]).SetValue(99,5);Assert.That(((IEnumerable<int>)Prop(result,"Data")).First(),Is.EqualTo(41));}
            }
        }
        [TestCase("GetMemberReleaseSpecification")] [TestCase("GetMemberReleaseSpecificationExtended")]
        [TestCase("GetOrthotropic2DMaterialProperties")]
        public void NativeSinglePrecisionFloatingBuffersAreAcceptedWithoutAxisOrUnitConversion(string name)
        {
            var native=new Native();native.ReplaceBuffer=(role,input)=>ValidArray(native,role,input,5,true);using(var wrapper=Wrap(native))Status(Invoke(wrapper.Property,name),PropertyQueryStatus.Success);
        }
        [TestCase(24)] [TestCase(25)] [TestCase(26)] [TestCase(99)]
        public void SharedContractsRemainPerWrapperAndDoNotRequireArtificialYearServices(int major)
        {
            var native=new Native();using(var wrapper=new OpenStaadWrapper(new Root{Property=native,Major=major}))
                foreach(string name in Names)Status(Invoke(wrapper.Property,name),PropertyQueryStatus.Success);
        }
        [TestCase("GetMemberMaterialConstants")] [TestCase("GetExtendedMaterialProperty")]
        public void OnlyDocumentedBooleanContractsAcceptIntegralVariantTrue(string name)
        {
            var native=new Native();native.Responses[NativeNames[Array.IndexOf(Names,name)]]=new Queue<object>(new object[]{-1});using(var wrapper=Wrap(native))Status(Invoke(wrapper.Property,name),PropertyQueryStatus.Success);
        }
        [TestCase("GetMemberReleaseSpecification")] [TestCase("GetMemberSectionProperties")]
        public void NumericOneSuccessContractsRejectUndocumentedNegativeFlags(string name)
        {
            var native=new Native();native.Responses[NativeNames[Array.IndexOf(Names,name)]]=new Queue<object>(new object[]{-1});using(var wrapper=Wrap(native))Assert.Throws<InvalidOperationException>(()=>Invoke(wrapper.Property,name));
        }
    }
}
