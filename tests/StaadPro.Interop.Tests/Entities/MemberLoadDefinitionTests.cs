using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework;
using StaadPro.Interop.Common.Units;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;
using StaadPro.Interop.Tests.Adapter;

namespace StaadPro.Interop.Tests.Entities
{
    [TestFixture]
    public class MemberLoadDefinitionTests
    {
        public class AssignmentLoad
        {
            public int ActiveId { get; private set; }
            public bool Reference { get; private set; }
            public string Method { get; private set; }
            public int[] Ids { get; private set; }
            public int Direction { get; private set; }
            public double[] Values { get; private set; }
            public List<string> Calls { get; } = new List<string>();
            public Exception Failure { get; set; }
            public bool SetLoadActive(int id) { ActiveId = id; Reference = false; Calls.Add("primary"); return true; }
            public int SetReferenceLoadActive(int id) { ActiveId = id; Reference = true; Calls.Add("reference"); return id; }
            private int Record(string name, int[] ids, int direction, params double[] values)
            {
                if (Failure != null) throw Failure;
                Calls.Add(name); Method = name; Ids = ids; Direction = direction; Values = values;
                return 0;
            }
            public int AddMemberConcMoment(int[] ids, int direction, double magnitude, double position, double offset)
                => Record("moment", ids, direction, magnitude, position, offset);
            public int AddMemberLinearVari(int[] ids, int direction, double start, double end, double middle)
                => Record("linear", ids, direction, start, end, middle);
            public int AddMemberTrapezoidal(int[] ids, int direction, double start, double end, double startPosition, double endPosition)
                => Record("trapezoidal", ids, direction, start, end, startPosition, endPosition);
        }

        private static MemberLoad Definition(string kind)
        {
            switch (kind)
            {
                case "moment": return new MemberConcentratedMoment(LoadDirection.GlobalZ, -2, 1.5, -.25);
                case "linear": return new MemberLinearVaryingLoad(LoadDirection.LocalZ, -2, 7, 11);
                case "trapezoidal": return new MemberTrapezoidalLoad(LoadDirection.ProjectedX, -2, 11, 1.5, 3.25);
                default: throw new ArgumentException(nameof(kind));
            }
        }

        private static double[] Values(MemberLoad item)
        {
            if (item is MemberConcentratedMoment moment) return new[] { moment.Magnitude, moment.Position, moment.Eccentricity };
            if (item is MemberLinearVaryingLoad linear) return new[] { linear.WStart, linear.WEnd, linear.WMiddle };
            var trapezoidal = (MemberTrapezoidalLoad)item;
            return new[] { trapezoidal.WStart, trapezoidal.WEnd, trapezoidal.SPosition, trapezoidal.EPosition };
        }

        private static double[] Expected(string kind) => kind == "moment" ? new[] { -2, 1.5, -.25 } : kind == "linear" ? new[] { -2.0, 11, 7 } : new[] { -2, 11, 1.5, 3.25 };

        [TestCase("moment")]
        [TestCase("linear")]
        [TestCase("trapezoidal")]
        public void DefinitionCopy_IsIndependentAndPreservesValuesDirectionAndCase(string kind)
        {
            var definition = Definition(kind);
            var lc = new LoadCase(17);
            definition.LoadCase = lc;
            definition.Members.Add(new Beam(42, new Node(0, 0, 0), new Node(1, 0, 0)));
            var copy = definition.DeepCopy();
            Assert.That(copy, Is.Not.SameAs(definition));
            Assert.That(copy.GetType(), Is.EqualTo(definition.GetType()));
            Assert.That(Values(definition), Is.EqualTo(Expected(kind)), "Constructor must preserve property order.");
            Assert.That(Values(copy), Is.EqualTo(Expected(kind)));
            Assert.That(copy.Direction, Is.EqualTo(definition.Direction));
            Assert.That(copy.LoadCase, Is.SameAs(lc));
            Assert.That(copy.Members, Is.Empty, "Copy is a load definition, not a beam association copy.");
            definition.ConvertUsing(new UnitsConverter(toForce: ForceInputUnit.Newton));
            Assert.That(Values(copy), Is.EqualTo(Expected(kind)), "Mutating the original must not change copied values.");
            Assert.That(definition.Members.Count, Is.EqualTo(1));
        }

        [TestCase("moment")]
        [TestCase("linear")]
        [TestCase("trapezoidal")]
        public void ConvertUsing_UsesCorrectMomentDistributedAndLengthDimensions(string kind)
        {
            var definition = Definition(kind);
            var lc = new LoadCase(17); definition.LoadCase = lc;
            var direction = definition.Direction;
            definition.ConvertUsing(null);
            Assert.That(Values(definition), Is.EqualTo(Expected(kind)));
            // kN/m -> N/cm: force factor 1000, length factor 100,
            // moment factor 100000, distributed force factor 10.
            definition.ConvertUsing(new UnitsConverter(toLength: LengthInputUnit.CentiMeter, toForce: ForceInputUnit.Newton));
            var expected = kind == "moment" ? new[] { -200000.0, 150, -25 } : kind == "linear" ? new[] { -20.0, 110, 70 } : new[] { -20.0, 110, 150, 325 };
            Assert.That(Values(definition), Is.EqualTo(expected));
            Assert.That(definition.Direction, Is.EqualTo(direction));
            Assert.That(definition.LoadCase, Is.SameAs(lc));
            definition.ConvertUsing(new UnitsConverter(LengthInputUnit.CentiMeter, ForceInputUnit.Newton));
            Assert.That(Values(definition), Is.EqualTo(Expected(kind)).Within(1e-12));
        }

        [TestCase("moment", LoadCaseType.PrimaryLoad)]
        [TestCase("moment", LoadCaseType.ReferenceLoad)]
        [TestCase("linear", LoadCaseType.PrimaryLoad)]
        [TestCase("linear", LoadCaseType.ReferenceLoad)]
        [TestCase("trapezoidal", LoadCaseType.PrimaryLoad)]
        [TestCase("trapezoidal", LoadCaseType.ReferenceLoad)]
        public void AddMemberLoad_ActivatesCaseAndDispatchesEveryNativeArgument(string kind, LoadCaseType caseType)
        {
            var raw = new AssignmentLoad();
            var definition = Definition(kind);
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var beam = new Beam(42, new Node(0, 0, 0), new Node(1, 0, 0));
                wrapper.Load.AddMemberLoad(new LoadCase(17, "case", caseType), beam, definition);
                Assert.That(raw.ActiveId, Is.EqualTo(17));
                Assert.That(raw.Reference, Is.EqualTo(caseType == LoadCaseType.ReferenceLoad));
                Assert.That(raw.Calls, Is.EqualTo(new[] { caseType == LoadCaseType.ReferenceLoad ? "reference" : "primary", kind }));
                Assert.That(raw.Ids, Is.EqualTo(new[] { 42 }));
                Assert.That(raw.Direction, Is.EqualTo((int)definition.Direction));
                Assert.That(raw.Values, Is.EqualTo(Expected(kind)), "Native linear order must be start/end/middle.");
            }
        }

        [TestCase("moment")]
        [TestCase("linear")]
        [TestCase("trapezoidal")]
        public void ApplyTo_SkipsMissingInputsAndPassesMultipleMemberIdsUnchanged(string kind)
        {
            var raw = new AssignmentLoad();
            var definition = Definition(kind);
            definition.ApplyTo(null, new[] { 42 });
            definition.ApplyTo(raw, null);
            definition.ApplyTo(raw, new int[0]);
            Assert.That(raw.Calls, Is.Empty);
            definition.ApplyTo(raw, new[] { 42, 91 });
            Assert.That(raw.Ids, Is.EqualTo(new[] { 42, 91 }));
            Assert.That(raw.Values, Is.EqualTo(Expected(kind)));
        }

        [TestCase("moment")]
        [TestCase("linear")]
        [TestCase("trapezoidal")]
        public void ApplyTo_PropagatesNativeFailureUnchanged(string kind)
        {
            var failure = new COMException("native assignment failure");
            var raw = new AssignmentLoad { Failure = failure };
            Assert.That(Assert.Throws<COMException>(() => Definition(kind).ApplyTo(raw, new[] { 42 })), Is.SameAs(failure));
        }
    }
}
