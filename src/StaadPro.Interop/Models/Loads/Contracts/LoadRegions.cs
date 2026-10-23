using System;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Services.Load;

namespace StaadPro.Interop.Models.Loads.Contracts
{
    /// <summary>Contains finite ordered global-coordinate bounds in current native length units.</summary>
    /// <remarks>Equal bounds permit a plane. Does not assume Y-up, convert units or enumerate geometry.</remarks>
    public sealed class GlobalLoadRegion
    {
        /// <summary>Creates explicit global X/Y/Z intervals.</summary>
        /// <param name="minimumX">Minimum global X.</param><param name="maximumX">Maximum global X.</param>
        /// <param name="minimumY">Minimum global Y.</param><param name="maximumY">Maximum global Y.</param>
        /// <param name="minimumZ">Minimum global Z.</param><param name="maximumZ">Maximum global Z.</param>
        /// <exception cref="ArgumentOutOfRangeException">A coordinate is nonfinite.</exception>
        /// <exception cref="ArgumentException">A minimum exceeds its maximum.</exception>
        public GlobalLoadRegion(double minimumX, double maximumX, double minimumY, double maximumY, double minimumZ, double maximumZ)
        {
            MinimumX = LoadContractArguments.Finite(minimumX, nameof(minimumX)); MaximumX = LoadContractArguments.Finite(maximumX, nameof(maximumX));
            MinimumY = LoadContractArguments.Finite(minimumY, nameof(minimumY)); MaximumY = LoadContractArguments.Finite(maximumY, nameof(maximumY));
            MinimumZ = LoadContractArguments.Finite(minimumZ, nameof(minimumZ)); MaximumZ = LoadContractArguments.Finite(maximumZ, nameof(maximumZ));
            if (minimumX > maximumX || minimumY > maximumY || minimumZ > maximumZ) throw new ArgumentException("Region minima cannot exceed maxima.");
        }
        /// <summary>Gets minimum global X.</summary>
        public double MinimumX { get; }
        /// <summary>Gets maximum global X.</summary>
        public double MaximumX { get; }
        /// <summary>Gets minimum global Y.</summary>
        public double MinimumY { get; }
        /// <summary>Gets maximum global Y.</summary>
        public double MaximumY { get; }
        /// <summary>Gets minimum global Z.</summary>
        public double MinimumZ { get; }
        /// <summary>Gets maximum global Z.</summary>
        public double MaximumZ { get; }
    }

    /// <summary>Combines a bounded global region with its floor range axis.</summary>
    /// <remarks>Range axis is distinct from the force direction; internal floor-count mapping uses axis+1.</remarks>
    public sealed class FloorRegion
    {
        /// <summary>Creates a region for floor beam-count and influence-area observations.</summary>
        /// <param name="bounds">Explicit global bounds.</param><param name="rangeAxis">Defined global range axis.</param>
        /// <exception cref="ArgumentNullException">Bounds are null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The axis is undefined.</exception>
        public FloorRegion(GlobalLoadRegion bounds, GlobalAxis rangeAxis)
        { Bounds = bounds ?? throw new ArgumentNullException(nameof(bounds)); RangeAxis = LoadContractArguments.Defined(rangeAxis, nameof(rangeAxis)); }
        /// <summary>Gets the immutable coordinate bounds.</summary>
        public GlobalLoadRegion Bounds { get; }
        /// <summary>Gets the axis of the range selection, not the loading axis.</summary>
        public GlobalAxis RangeAxis { get; }
    }
}
