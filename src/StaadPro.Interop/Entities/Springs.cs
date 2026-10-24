using System;
using System.Linq;
using StaadPro.Interop.Common;
using static StaadPro.Interop.Services.Property.PropertyValueReader;

namespace StaadPro.Interop.Entities
{
    /// <summary>Carries nullable translational and rotational release spring stiffnesses.</summary>
    /// <remarks>Translational stiffness has force/length units; rotational stiffness has moment/angle units. No shared conversion factor is assumed. The private unit-converter helper is omitted. Null unused slots serialize to zero only for the basic native creation input.</remarks>
    public class Springs : ValidatablePropertyStore
    {
        /// <summary>Initializes all six spring slots to zero as in the source packet.</summary>
        public Springs() { KFx = 0.0; KFy = 0.0; KFz = 0.0; KMx = 0.0; KMy = 0.0; KMz = 0.0; }
        /// <summary>Gets or sets KFX in native force/length units; null denotes an unused/unavailable compatibility slot.</summary>
        public double? KFx { get => Get<double?>(); set => Set(value); }
        /// <summary>Gets or sets KFY in native force/length units; null denotes an unused/unavailable compatibility slot.</summary>
        public double? KFy { get => Get<double?>(); set => Set(value); }
        /// <summary>Gets or sets KFZ in native force/length units; null denotes an unused/unavailable compatibility slot.</summary>
        public double? KFz { get => Get<double?>(); set => Set(value); }
        /// <summary>Gets or sets KMX in native moment/angle units; null denotes an unused/unavailable compatibility slot.</summary>
        public double? KMx { get => Get<double?>(); set => Set(value); }
        /// <summary>Gets or sets KMY in native moment/angle units; null denotes an unused/unavailable compatibility slot.</summary>
        public double? KMy { get => Get<double?>(); set => Set(value); }
        /// <summary>Gets or sets KMZ in native moment/angle units; null denotes an unused/unavailable compatibility slot.</summary>
        public double? KMz { get => Get<double?>(); set => Set(value); }
        /// <summary>Copies spring values in native DOF order, mapping unused null inputs to zero.</summary>
        /// <returns>A new six-entry array in KFX/KFY/KFZ/KMX/KMY/KMZ order.</returns>
        public double[] ToArray() => new[] { KFx ?? 0.0, KFy ?? 0.0, KFz ?? 0.0, KMx ?? 0.0, KMy ?? 0.0, KMz ?? 0.0 };
        /// <summary>Tests whether any supplied stiffness is positive.</summary>
        /// <returns>True if any non-null value exceeds zero.</returns>
        public bool HasAnyPositive() => ToArray().Any(v => v > 0);
        /// <summary>Tests whether every slot is zero or null.</summary>
        /// <returns>True when no nonzero stiffness is present.</returns>
        public bool IsAllZeroOrNull() => ToArray().All(v => v == 0);
        /// <summary>Copies exactly six finite native spring values.</summary>
        /// <param name="springsArrayObj">Six-entry one-dimensional floating-point array; malformed or nonfinite data throws InvalidOperationException.</param>
        public void Parse(object springsArrayObj) { var values = FloatingArray(springsArrayObj, 6, "Springs.Parse"); KFx = values[0]; KFy = values[1]; KFz = values[2]; KMx = values[3]; KMy = values[4]; KMz = values[5]; }
        /// <summary>Copies nullable spring values from another managed packet.</summary>
        /// <param name="source">Source packet; null is a no-op.</param>
        public void Assign(Springs source) { if (source == null) return; KFx = source.KFx; KFy = source.KFy; KFz = source.KFz; KMx = source.KMx; KMy = source.KMy; KMz = source.KMz; }
        /// <summary>Copies values from another managed packet.</summary>
        /// <param name="source">Source packet; null is a no-op.</param>
        public void MapFrom(Springs source) => Assign(source);
    }
}
