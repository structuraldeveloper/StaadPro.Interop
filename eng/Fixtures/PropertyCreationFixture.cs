using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Models.Property;

namespace PackageVerification
{
    // Managed native stand-in shared by tests and the PackageReference-only verifier. Never activates STAAD.
    public static class PropertyCreationFixture
    {
        public sealed class Root
        {
            public object Property { get; set; }
            public object Geometry => null;
            public object GetApplicationVersion(ref object major, ref object update, ref object revision, ref object build)
            { major = 26; update = 0; revision = 0; build = 100; return "26.00.00.100"; }
        }
        public sealed class Native
        {
            public readonly List<string> Calls = new List<string>();
            public readonly List<object[]> Arguments = new List<object[]>();
            public readonly List<int> Threads = new List<int>();
            public readonly Dictionary<string, Queue<object>> Responses = new Dictionary<string, Queue<object>>();
            public Action<string> OnCall;
            public string UniqueId = " exact-native-id ";
            public object R(string op, object fallback, params object[] args)
            {
                Calls.Add(op); Arguments.Add(args); Threads.Add(Thread.CurrentThread.ManagedThreadId); OnCall?.Invoke(op);
                return Responses.TryGetValue(op, out var q) && q.Count > 0 ? q.Dequeue() : fallback;
            }
            public object GetIsotropicMaterialCount() => R(nameof(GetIsotropicMaterialCount), 2);
            public object CreateWideFlangePropertyFromTable(int country, string name, int modifier, double[] values) => R(nameof(CreateWideFlangePropertyFromTable), 701, country, name, modifier, values);
            public object CreateBeamPropertyFromTableEx(int country, string name, int shape) => R(nameof(CreateBeamPropertyFromTableEx), 702, country, name, shape);
            public object CreatePrismaticRectangleProperty(double depth, double width) => R(nameof(CreatePrismaticRectangleProperty), 703, depth, width);
            public object CreatePrismaticCircleProperty(double diameter) => R(nameof(CreatePrismaticCircleProperty), 704, diameter);
            public object CreatePrismaticTeeProperty(double total, double flange, double stem, double width) => R(nameof(CreatePrismaticTeeProperty), 705, total, flange, stem, width);
            public object CreatePrismaticTrapezoidalProperty(double depth, double top, double bottom) => R(nameof(CreatePrismaticTrapezoidalProperty), 706, depth, top, bottom);
            public object CreatePrismaticGeneralProperty(double[] values) => R(nameof(CreatePrismaticGeneralProperty), 707, values);
            public object CreateTaperedTubeProperty(int shape, double start, double end, double thickness) => R(nameof(CreateTaperedTubeProperty), 708, shape, start, end, thickness);
            public object CreateMemberPartialReleaseSpec(int end, int[] flags, double[] factors) => R(nameof(CreateMemberPartialReleaseSpec), 0, end, flags, factors);
            public object CreateMemberTrussSpec() => R(nameof(CreateMemberTrussSpec), 801);
            public object CreateMemberInactiveSpec() => R(nameof(CreateMemberInactiveSpec), 802);
            public object CreateMemberTensionSpec() => R(nameof(CreateMemberTensionSpec), 803);
            public object CreateMemberCompressionSpec() => R(nameof(CreateMemberCompressionSpec), 804);
            public object CreateMemberIgnoreStiffSpec() => R(nameof(CreateMemberIgnoreStiffSpec), 805);
            public object CreateMemberCableSpec(int kind, double value) => R(nameof(CreateMemberCableSpec), 806, kind, value);
            public object CreateMemberCableSpecEx(int kind, double value, int end, double x, double y, double z) => R(nameof(CreateMemberCableSpecEx), 807, kind, value, end, x, y, z);
            public object CreateElementNodeReleaseSpec(int node, int[] flags) => R(nameof(CreateElementNodeReleaseSpec), 0, node, flags);
            public object CreateElementPlaneStressSpec() => R(nameof(CreateElementPlaneStressSpec), 808);
            public object CreateElementIgnoreInplaneRotnSpec() => R(nameof(CreateElementIgnoreInplaneRotnSpec), 809);
            public object AddControlDependentRelation(int control, int mode, int fx, int fy, int fz, int mx, int my, int mz, int[] ids) => R(nameof(AddControlDependentRelation), 0, control, mode, fx, fy, fz, mx, my, mz, ids);
            public object CreateElementOffsetSpec(int axes, int corner, double x, double y, double z) => R(nameof(CreateElementOffsetSpec), 810, axes, corner, x, y, z);
            public object CreateElementLocalZOffsetSpec(double a, double b, double c, double d) => R(nameof(CreateElementLocalZOffsetSpec), 811, a, b, c, d);
            public object CreateMemberFireProofingSpec(int type, double thickness, double density) => R(nameof(CreateMemberFireProofingSpec), 0, type, thickness, density);
            public object GetPropertyUniqueID(int id) => R(nameof(GetPropertyUniqueID), UniqueId, id);
            public void SetPropertyUniqueID(int id, string value) { R(nameof(SetPropertyUniqueID), null, id, value); UniqueId = value; }
        }
        // Runs every new mutation against the managed stand-in, even though shipped examples are guarded.
        public static void VerifyCalls(IOSProperty property, Native native)
        {
            var flags = new DegreeOfFreedomSelection(true, false, true, false, true, false);
            var factors = new PartialReleaseFactors(.1, .2, .3, .4, .5, .6);
            var results = new[] {
                property.CreateWideFlangePropertyFromTable(WideFlangeTableDefinition.WithBottomPlate(Country.Indian, "ISMB300", .2, .01)),
                property.CreateBeamPropertyFromTableEx(Country.Indian, "TABLE_SHAPE", TableSolidShape.SolidRectangle),
                property.CreatePrismaticRectangleProperty(.4, .2), property.CreatePrismaticCircleProperty(.3),
                property.CreatePrismaticTeeProperty(.5, .3, .45, .02), property.CreatePrismaticTrapezoidalProperty(.5, .2, .4),
                property.CreatePrismaticGeneralProperty(new GeneralPrismaticSectionDefinition(.01, .002, .003, .000001, .00001, .0001, .3, .2, .25, .02)),
                property.CreateTaperedTubeProperty(TaperedTubeShape.Hexadecagonal, .4, .2, .01),
                property.CreateMemberPartialReleaseSpec(MemberEnd.End, flags, factors), property.CreateMemberTrussSpec(), property.CreateMemberInactiveSpec(),
                property.CreateMemberTensionSpec(), property.CreateMemberCompressionSpec(), property.CreateMemberIgnoreStiffSpec(),
                property.CreateMemberCableSpec(CableDefinitionKind.UnstressedLength, 5),
                property.CreateMemberCableSpecEx(CableDefinitionKind.InitialTension, 25, CableTensionEnd.Start, 0, -1, .5),
                property.CreateElementNodeReleaseSpec(11, flags), property.CreateElementPlaneStressSpec(), property.CreateElementIgnoreInplaneRotnSpec(),
                property.AddControlDependentRelation(11, ControlDependencyMode.SpecifiedDegreesOfFreedom, flags, new[] { 13, 12, 13 }),
                property.CreateElementOffsetSpec(ElementNodeOffsetAxis.Local, 2, .1, -.2, .3), property.CreateElementLocalZOffsetSpec(.1, .2, -.3, .4),
                property.CreateMemberFireProofingSpec(FireProofingType.Block, .03, 12)
            };
            if (results.Length != 23 || results.Any(r => !r.IsSuccessful)) throw new InvalidOperationException("Installed creation/specification acknowledgement failed.");
            if (results[8].CreatedSpecificationId != null || results[16].CreatedSpecificationId != null || results[22].CreatedSpecificationIndex != 0 || results[22].CreatedSpecificationId != null)
                throw new InvalidOperationException("Installed status-only/zero-based identity failed.");
            var setter = property.SetPropertyUniqueID(37, " exact-installed-id ");
            if (setter.Status != PropertyMutationStatus.InvokedWithoutAcknowledgement || setter.IsSuccessful || setter.NativeReturnCode.HasValue || setter.NativeBooleanReturn.HasValue || setter.PropertyId != 37)
                throw new InvalidOperationException("Installed void setter fabricated acknowledgement.");
            var read = property.GetPropertyUniqueID(37);
            if (!read.IsSuccessful || read.Data.PropertyId != 37 || read.Data.Value != " exact-installed-id ") throw new InvalidOperationException("Installed exact metadata read failed.");
            var wide = native.Arguments[native.Calls.IndexOf("CreateWideFlangePropertyFromTable")];
            if ((int)wide[2] != 8 || !((double[])wide[3]).SequenceEqual(new[] { .2, .01, 0, 0 })) throw new InvalidOperationException("Installed bottom-cover selector/layout failed.");
            var cable = native.Arguments[native.Calls.IndexOf("CreateMemberCableSpecEx")];
            if ((int)cable[0] != 0 || (int)cable[2] != 1 || !cable.Skip(3).Cast<double>().SequenceEqual(new[] { 0, -1, .5 })) throw new InvalidOperationException("Installed cable selectors/order failed.");
        }
    }
}
