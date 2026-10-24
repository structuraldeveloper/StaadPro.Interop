using System;
using System.Collections.Generic;

namespace PackageVerification
{
    // Shared by unit tests and an isolated installed-package consumer; never activates native STAAD.
    public static class PropertySectionFixture
    {
        public sealed class Root
        {
            public object Property { get; set; }
            public int NativeMajor { get; set; } = 26;
            public object Geometry { get; } = new PropertyPortFixture.GeometryNative();
            public object GetApplicationVersion(ref object major, ref object update, ref object revision, ref object build)
            { major = NativeMajor; update = 0; revision = 0; build = 100; return NativeMajor + ".00.00.100"; }
        }
        public sealed class Definition
        {
            public int Type, Country;
            public string Name;
            public double[] Values;
        }
        public sealed class Native : PropertyPortFixture.Native
        {
            public int Capacity = 24;
            public readonly Dictionary<int, Definition> Definitions = new Dictionary<int, Definition> {
                [37] = new Definition { Type = 610, Country = 10, Name = "ISMB300", Values = new[] { .01, .3, .14, .01, .006, .0001, .00001, .000001 } },
                [38] = new Definition { Type = 672, Country = 0, Name = "RECT", Values = new[] { .02, .0001, .0002, .00001, .2, .1 } }
            };
            public new object GetCountofSectionPropertyValuesEx() => R(nameof(GetCountofSectionPropertyValuesEx), Capacity);
            private object OutputValue(string op, object value) => Output == null ? value : Output(op, value);
            public new object GetSectionPropertyValuesEx(int id, ref object type, ref object values)
            {
                var raw = R(nameof(GetSectionPropertyValuesEx), 1, id); var d = Definitions[id]; type = OutputValue("sectionType", d.Type);
                var array = (Array)values;
                for (int i = 0; i < Math.Min(array.Length, d.Values.Length); i++) array.SetValue(d.Values[i], array.GetLowerBound(0) + i);
                values = OutputValue(nameof(GetSectionPropertyValuesEx), values); Buffers[nameof(GetSectionPropertyValuesEx)] = values; return raw;
            }
            public new object GetSectionPropertyName(int id, ref object name)
            { var raw = R(nameof(GetSectionPropertyName), 0, id); name = OutputValue(nameof(GetSectionPropertyName), Definitions[id].Name); return raw; }
            public object GetSectionPropertyType(int id) => R(nameof(GetSectionPropertyType), Definitions[id].Type, id);
            public object GetSectionPropertyCountry(int id) => R(nameof(GetSectionPropertyCountry), Definitions[id].Country, id);
            public object GetRecordForSection(int country, string name) => R(nameof(GetRecordForSection), 42, country, name);
            public object CreateTeePropertyFromTable(int country, string name, int modifier) => R(nameof(CreateTeePropertyFromTable), 101, country, name, modifier);
            public object CreateChannelPropertyFromTable(int country, string name, int modifier, double spacing) => R(nameof(CreateChannelPropertyFromTable), 102, country, name, modifier, spacing);
            public object CreateAnglePropertyFromTable(int country, string name, int modifier, double spacing) => R(nameof(CreateAnglePropertyFromTable), 103, country, name, modifier, spacing);
            public object CreatePipePropertyFromTable(int country, string name, int modifier, double outside, double inside) => R(nameof(CreatePipePropertyFromTable), 104, country, name, modifier, outside, inside);
            public object CreateTubePropertyFromTable(int country, string name, int modifier, double thickness, double width, double depth) => R(nameof(CreateTubePropertyFromTable), 105, country, name, modifier, thickness, width, depth);
            public object CreateBeamPropertyFromTableComposite(int country, string name, int modifier, double[] values) => R(nameof(CreateBeamPropertyFromTableComposite), 106, country, name, modifier, values);
        }
    }
}
