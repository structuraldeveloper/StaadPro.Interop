using System;
using System.Linq;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models.Sections;

namespace StaadPro.Interop.Services.Property
{
    // Immutable request snapshot; keep COM dispatch in the internal adapter service.
    internal sealed class SectionCreationInput
    {
        internal SectionProperty Packet;
        internal string Route, Name;
        internal int Country, Modifier;
        internal double[] Values;
        internal static SectionCreationInput From(SectionProperty packet)
        {
            if (packet == null) throw new InvalidOperationException("Section definition must not be null.");
            var definition = packet.SectionDefinition; var selected = definition?.SectionProfile?.SelectedSection;
            if (selected == null) throw new ArgumentException("Select a native H/C/L/O/Box table section before creation.", nameof(packet));
            if (!Enum.IsDefined(typeof(Country), selected.Country) || !Enum.IsDefined(typeof(TypeSpecification), definition.TypeSpecification)) throw new ArgumentOutOfRangeException(nameof(packet));
            string name = selected.StaadName;
            if (string.IsNullOrWhiteSpace(name) || name.IndexOf('\0') >= 0) throw new ArgumentException("Provide an exact nonblank native table name without NUL characters.", nameof(packet));
            var modifier = definition.TypeSpecification;
            var input = new SectionCreationInput { Packet = packet, Name = name, Country = (int)selected.Country, Modifier = (int)modifier, Values = new double[0] };
            if (selected is RolledSectionHShape)
            {
                switch (modifier)
                {
                    case TypeSpecification.ST: input.Route = "beam"; input.Values = new[] { 0.0, 0.0 }; break;
                    case TypeSpecification.T: input.Route = "tee"; break;
                    case TypeSpecification.D: input.Route = "beam"; input.Values = new[] { Spacing(definition), 0.0 }; break;
                    case TypeSpecification.TC: case TypeSpecification.BC: case TypeSpecification.TB:
                        var plates = definition.AdditionalSpecifications as TopAndBottomCoverPlateSpecifications ?? throw new ArgumentException("Provide top/bottom cover-plate specifications.", nameof(packet));
                        input.Route = "cover";
                        input.Values = modifier == TypeSpecification.TC ? plates.Top.ToArray() : modifier == TypeSpecification.BC ? plates.Bottom.ToArray() : plates.ToArray();
                        Positive(input.Values); break;
                    case TypeSpecification.CM:
                        var concrete = definition.AdditionalSpecifications as ConcreteCompositeSpecifications ?? throw new ArgumentException("Provide concrete-composite specifications.", nameof(packet));
                        input.Route = "composite"; input.Values = concrete.ToArray(); Positive(input.Values); break;
                    default: throw new NotSupportedException("This modifier is not supported by the main H-section creation route.");
                }
            }
            else if (selected is RolledSectionCShape)
            {
                if (modifier != TypeSpecification.ST && modifier != TypeSpecification.D && modifier != TypeSpecification.FR && modifier != TypeSpecification.BA) throw new NotSupportedException("Channel creation supports ST, D, FR and BA.");
                input.Route = "channel"; input.Values = new[] { modifier == TypeSpecification.ST ? 0.0 : Spacing(definition) };
            }
            else if (selected is RolledSectionLShape)
            {
                if (modifier != TypeSpecification.ST && modifier != TypeSpecification.RA && modifier != TypeSpecification.SA && modifier != TypeSpecification.LD && modifier != TypeSpecification.SD) throw new NotSupportedException("Angle creation supports ST, RA, SA, LD and SD.");
                input.Route = "angle"; input.Values = new[] { modifier == TypeSpecification.LD || modifier == TypeSpecification.SD ? Spacing(definition) : 0.0 };
            }
            else if (selected is RolledSectionOShape)
            {
                input.Route = "pipe";
                if (modifier == TypeSpecification.ST) input.Values = new[] { 0.0, 0.0 };
                else if (modifier == TypeSpecification.UserDefined)
                {
                    input.Values = (definition.AdditionalSpecifications as PipeDefineSpecifications ?? throw new ArgumentException("Provide user-defined pipe dimensions.", nameof(packet))).ToArray();
                    if (input.Values[0] <= input.Values[1] || input.Values[1] < 0) throw new ArgumentException("Pipe OD must exceed nonnegative ID.", nameof(packet));
                }
                else throw new NotSupportedException("Pipe creation supports ST or UserDefined.");
            }
            else if (selected is RolledSectionBoxShape)
            {
                input.Route = "tube";
                if (modifier == TypeSpecification.ST) input.Values = new[] { 0.0, 0.0, 0.0 };
                else if (modifier == TypeSpecification.UserDefined)
                {
                    input.Values = (definition.AdditionalSpecifications as TubeDefineSpecifications ?? throw new ArgumentException("Provide user-defined tube dimensions.", nameof(packet))).ToArray(); Positive(input.Values);
                    if (2 * input.Values[0] >= Math.Min(input.Values[1], input.Values[2])) throw new ArgumentException("Tube wall thickness must leave a positive inner width/depth.", nameof(packet));
                }
                else throw new NotSupportedException("Tube creation supports ST or UserDefined.");
            }
            else throw new NotSupportedException("The selected profile is not a supported H/C/L/O/Box carrier.");
            if (input.Values.Any(v => double.IsNaN(v) || double.IsInfinity(v))) throw new ArgumentException("Creation specifications must be finite.", nameof(packet));
            return input;
        }
        private static double Spacing(SectionDefinition definition)
        {
            double value = (definition.AdditionalSpecifications as SpacedDoubleSectionsSpecifications ?? throw new ArgumentException("Provide double-profile spacing.", nameof(definition))).Spacing;
            if (double.IsNaN(value) || double.IsInfinity(value) || value < 0) throw new ArgumentException("Spacing must be finite and nonnegative.", nameof(definition)); return value;
        }
        private static void Positive(double[] values)
        { if (values.Any(v => double.IsNaN(v) || double.IsInfinity(v) || v <= 0)) throw new ArgumentException("Provide finite positive creation values.", nameof(values)); }
        internal object Invoke(dynamic native)
        {
            switch (Route)
            {
                case "beam": return native.CreateBeamPropertyFromTable(Country, Name, Modifier, Values[0], Values[1]);
                case "tee": return native.CreateTeePropertyFromTable(Country, Name, Modifier);
                case "cover": return native.CreateBeamPropertyFromTableWithCoverPlates(Country, Name, Modifier, Values);
                case "composite": return native.CreateBeamPropertyFromTableComposite(Country, Name, Modifier, Values);
                case "channel": return native.CreateChannelPropertyFromTable(Country, Name, Modifier, Values[0]);
                case "angle": return native.CreateAnglePropertyFromTable(Country, Name, Modifier, Values[0]);
                case "pipe": return native.CreatePipePropertyFromTable(Country, Name, Modifier, Values[0], Values[1]);
                case "tube": return native.CreateTubePropertyFromTable(Country, Name, Modifier, Values[0], Values[1], Values[2]);
                default: throw new NotSupportedException("Unknown section creation route.");
            }
        }
    }
}
