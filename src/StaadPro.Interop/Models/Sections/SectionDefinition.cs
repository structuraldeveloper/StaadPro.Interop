using StaadPro.Interop.Common;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Interfaces;

namespace StaadPro.Interop.Models.Sections
{
    /// <summary>Carries a selected table profile and its additional creation specifications.</summary>
    /// <remarks>Preserves the main creation graph SectionProfile.SelectedSection and AdditionalSpecifications. Private material/catalog selection, validation UI and display services are omitted. Set the current existing material separately through IOSProperty.SetMaterialName.</remarks>
    public class SectionDefinition : ValidatablePropertyStore
    {
        /// <summary>Initializes an empty profile and standard single-section specification.</summary>
        public SectionDefinition() { SectionProfile = new SectionProfile(); TypeSpecification = TypeSpecification.ST; }
        /// <summary>Gets or sets the native modifier. Changing it resets additional values to the corresponding empty specification type.</summary>
        public TypeSpecification TypeSpecification
        {
            get => Get<TypeSpecification>();
            set
            {
                Set(value);
                switch (value)
                {
                    case TypeSpecification.D: case TypeSpecification.LD: case TypeSpecification.SD: case TypeSpecification.FR: case TypeSpecification.BA:
                        AdditionalSpecifications = new SpacedDoubleSectionsSpecifications(); break;
                    case TypeSpecification.TC: case TypeSpecification.BC: case TypeSpecification.TB:
                        AdditionalSpecifications = new TopAndBottomCoverPlateSpecifications(); break;
                    case TypeSpecification.CM: AdditionalSpecifications = new ConcreteCompositeSpecifications(); break;
                    default: AdditionalSpecifications = new NoAdditionalSpecifications(); break;
                }
            }
        }
        /// <summary>Gets or sets the selected profile holder; creation requires a recognized non-null selected shape.</summary>
        public SectionProfile SectionProfile { get => Get<SectionProfile>(); set => Set(value); }
        /// <summary>Gets or sets additional creation values. For UserDefined select PipeDefineSpecifications or TubeDefineSpecifications explicitly.</summary>
        /// <remarks>The setter is exposed to support SDK-free input construction without the private specifications factory.</remarks>
        public IAdditionalSpecifications AdditionalSpecifications { get => Get<IAdditionalSpecifications>(); set => Set(value); }
    }
    /// <summary>Holds an explicit native table selection without reading a database or changing STAAD.</summary>
    public class SectionProfile : ValidatablePropertyStore
    {
        /// <summary>Gets or sets one of the supported H/C/L/O/Box table profile carriers.</summary>
        public IRolledSection SelectedSection { get => Get<IRolledSection>(); set => Set(value); }
    }
    /// <summary>Carries a table identity; derive/use the specific H/C/L/O/Box class to select its native creation route.</summary>
    public abstract class RolledSection : ValidatablePropertyStore, IRolledSection
    {
        /// <summary>Gets or sets the preserved native country code.</summary>
        public Country Country { get => Get<Country>(); set => Set(value); }
        /// <summary>Gets or sets the exact native database designation.</summary>
        public string StaadName { get => Get<string>(); set => Set(value); }
        /// <summary>Gets or sets an optional identity/display designation; no native rename is performed.</summary>
        public string Designation { get => Get<string>(); set => Set(value); }
    }
    /// <summary>Selects the wide-flange/beam table routes, including tee cuts, double profiles, cover plates and concrete composites.</summary>
    public sealed class RolledSectionHShape : RolledSection { }
    /// <summary>Selects channel table creation for ST, D, FR or BA.</summary>
    public sealed class RolledSectionCShape : RolledSection { }
    /// <summary>Selects angle table creation for ST, RA, SA, LD or SD.</summary>
    public sealed class RolledSectionLShape : RolledSection { }
    /// <summary>Selects pipe table creation for ST or user-defined diameters.</summary>
    public sealed class RolledSectionOShape : RolledSection { }
    /// <summary>Selects tube table creation for ST or user-defined wall/width/depth.</summary>
    public sealed class RolledSectionBoxShape : RolledSection { }
}
