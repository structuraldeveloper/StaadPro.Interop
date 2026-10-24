using System;
using System.Linq;
using StaadPro.Interop.Common;
using static StaadPro.Interop.Services.Property.PropertyValueReader;

namespace StaadPro.Interop.Entities
{
    /// <summary>Carries dimensionless directional partial-moment factors, separate from Boolean release flags.</summary>
    public sealed class MpFactors : ValidatablePropertyStore
    {
        /// <summary>Initializes zero factors without selecting a native partial-release mode.</summary>
        public MpFactors() { }
        /// <summary>Initializes three directional partial-moment factors.</summary>
        /// <param name="mpX">Dimensionless MPX factor.</param>
        /// <param name="mpY">Dimensionless MPY factor.</param>
        /// <param name="mpZ">Dimensionless MPZ factor.</param>
        public MpFactors(double mpX, double mpY, double mpZ) { MPx = mpX; MPy = mpY; MPz = mpZ; }
        /// <summary>Gets or sets the dimensionless MPx factor; a value alone is not evidence of a native selected mode.</summary>
        public double MPx { get => Get<double>(); set => Set(value); }
        /// <summary>Gets or sets the dimensionless MPy factor; a value alone is not evidence of a native selected mode.</summary>
        public double MPy { get => Get<double>(); set => Set(value); }
        /// <summary>Gets or sets the dimensionless MPz factor; a value alone is not evidence of a native selected mode.</summary>
        public double MPz { get => Get<double>(); set => Set(value); }
        /// <summary>Copies three finite directional factors, retaining source null no-op behavior.</summary>
        /// <param name="mpFactorsArrayObj">Null, or exactly three floating-point values in MPX/MPY/MPZ order.</param>
        public void Parse(object mpFactorsArrayObj) { if (mpFactorsArrayObj == null) return; var a = FloatingArray(mpFactorsArrayObj, 3, "MpFactors.Parse"); MPx = a[0]; MPy = a[1]; MPz = a[2]; }
    }
}
