
using System;
using System.Globalization;

namespace StaadPro.Interop.Models.Loads.Wind
{
    /// <summary>An immutable pressure/height value pair for an OpenSTAAD wind definition.</summary>
    /// <remarks>This small value type preserves the reference model's value semantics. It uses the current model's
    /// pressure and length units without conversion. The default value is the valid zero-intensity, zero-height pair.</remarks>
    public readonly struct WindIntensityPair : IEquatable<WindIntensityPair>
    {
        /// <summary>Gets the finite signed pressure intensity in model force per squared length units.</summary>
        public double Intensity { get; }

        /// <summary>Gets the finite nonnegative height in current model length units.</summary>
        public double Height { get; }

        /// <summary>Creates an immutable wind intensity/height value pair.</summary>
        /// <param name="intensity">Finite signed pressure value; the caller is responsible for its engineering meaning.</param>
        /// <param name="height">Finite height greater than or equal to zero.</param>
        /// <exception cref="ArgumentOutOfRangeException">Either value is nonfinite, or height is negative.</exception>
        public WindIntensityPair(double intensity, double height)
        {
            if (double.IsNaN(intensity) || double.IsInfinity(intensity)) throw new ArgumentOutOfRangeException(nameof(intensity));
            if (double.IsNaN(height) || double.IsInfinity(height) || height < 0) throw new ArgumentOutOfRangeException(nameof(height));
            Intensity = intensity; Height = height;
        }

        /// <summary>Compares both scalar values using exact double value equality.</summary>
        /// <param name="other">Pair to compare.</param>
        /// <returns>True when both values are equal.</returns>
        public bool Equals(WindIntensityPair other) => Intensity.Equals(other.Intensity) && Height.Equals(other.Height);

        /// <summary>Compares this pair with another boxed value.</summary>
        /// <param name="obj">Value to compare; null or another type is unequal.</param>
        /// <returns>True when the value is an equal WindIntensityPair.</returns>
        public override bool Equals(object obj) => obj is WindIntensityPair other && Equals(other);

        /// <summary>Calculates a hash from both immutable values.</summary>
        /// <returns>A hash consistent with pair equality.</returns>
        public override int GetHashCode()
        {
            unchecked { return Intensity.GetHashCode() * 397 ^ Height.GetHashCode(); }
        }

        /// <summary>Formats the two values using invariant culture.</summary>
        /// <returns>A pressure/height description without a unit conversion.</returns>
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "[Intensity: {0}, Height: {1}]", Intensity, Height);

        /// <summary>Tests exact pair equality.</summary>
        /// <param name="left">First pair.</param><param name="right">Second pair.</param>
        /// <returns>True if both values match.</returns>
        public static bool operator ==(WindIntensityPair left, WindIntensityPair right) => left.Equals(right);

        /// <summary>Tests pair inequality.</summary>
        /// <param name="left">First pair.</param><param name="right">Second pair.</param>
        /// <returns>True if either value differs.</returns>
        public static bool operator !=(WindIntensityPair left, WindIntensityPair right) => !left.Equals(right);
    }
}
