using System;
using System.Windows.Media.Media3D;
using Newtonsoft.Json;
using StaadPro.Interop.Common;
using StaadPro.Interop.Common.Units;
using StaadPro.Interop.Interfaces;

namespace StaadPro.Interop.Models
{
    /// <summary>
    /// Represents a 6-DOF force and moment vector with vector algebra and unit conversion support.
    /// </summary>
    public class Forces : ValidatablePropertyStore, IForces, IEquatable<Forces>
    {
        #region Constructors

        public Forces()
        {
        }

        public Forces(double fx, double fy, double fz, double mx, double my, double mz)
        {
            Fx = fx;
            Fy = fy;
            Fz = fz;
            Mx = mx;
            My = my;
            Mz = mz;
        }

        #endregion

        #region Public Properties

        [JsonProperty(Order = 1)]
        public double Fx { get => Get<double>(); set => Set(value); }

        [JsonProperty(Order = 2)]
        public double Fy { get => Get<double>(); set => Set(value); }

        [JsonProperty(Order = 3)]
        public double Fz { get => Get<double>(); set => Set(value); }

        [JsonProperty(Order = 4)]
        public double Mx { get => Get<double>(); set => Set(value); }

        [JsonProperty(Order = 5)]
        public double My { get => Get<double>(); set => Set(value); }

        [JsonProperty(Order = 6)]
        public double Mz { get => Get<double>(); set => Set(value); }

        #endregion

        #region Public Vector Properties

        [JsonIgnore]
        public Vector3D ForceVector => new Vector3D(Fx, Fy, Fz);

        [JsonIgnore]
        public Vector3D MomentVector => new Vector3D(Mx, My, Mz);

        #endregion

        #region Public Methods

        public void Add(IForces other)
        {
            if (other == null) return;
            Fx += other.Fx;
            Fy += other.Fy;
            Fz += other.Fz;
            Mx += other.Mx;
            My += other.My;
            Mz += other.Mz;
        }

        public void ConvertUsing(UnitsConverter uc)
        {
            if (uc == null) return;
            Fx *= uc.ForceConversionFactor;
            Fy *= uc.ForceConversionFactor;
            Fz *= uc.ForceConversionFactor;
            Mx *= uc.MomentConversionFactor;
            My *= uc.MomentConversionFactor;
            Mz *= uc.MomentConversionFactor;
        }

        public Forces ScaledBy(double factor)
        {
            return new Forces(Fx * factor, Fy * factor, Fz * factor, Mx * factor, My * factor, Mz * factor);
        }

        #endregion

        #region Equality

        public bool Equals(Forces other)
        {
            if (other is null) return false;
            return Math.Abs(Fx - other.Fx) < 1e-6 &&
                   Math.Abs(Fy - other.Fy) < 1e-6 &&
                   Math.Abs(Fz - other.Fz) < 1e-6 &&
                   Math.Abs(Mx - other.Mx) < 1e-6 &&
                   Math.Abs(My - other.My) < 1e-6 &&
                   Math.Abs(Mz - other.Mz) < 1e-6;
        }

        public override bool Equals(object obj) => Equals(obj as Forces);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Fx.GetHashCode();
                hash = (hash * 397) ^ Fy.GetHashCode();
                hash = (hash * 397) ^ Fz.GetHashCode();
                hash = (hash * 397) ^ Mx.GetHashCode();
                hash = (hash * 397) ^ My.GetHashCode();
                hash = (hash * 397) ^ Mz.GetHashCode();
                return hash;
            }
        }

        #endregion
    }
}
