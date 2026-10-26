using System.Windows.Media.Media3D;

namespace StaadPro.Interop.Models.Output
{
    /// <summary>Contains signed minimum/maximum axial actions and their native stations.</summary>
    /// <remarks>No absolute-value conversion, sorting, sign change or unit scaling is applied. Stations retain native distance units from the analytical start.</remarks>
    public sealed class MemberForceExtrema
    {
        internal MemberForceExtrema(double minimumValue, double minimumPosition, double maximumValue, double maximumPosition) { MinimumValue = minimumValue; MinimumPosition = minimumPosition; MaximumValue = maximumValue; MaximumPosition = maximumPosition; }
        /// <summary>Gets the signed minimum axial force in native force units.</summary>
        public double MinimumValue { get; }
        /// <summary>Gets the native nonnegative station of the minimum, measured from the analytical start.</summary>
        public double MinimumPosition { get; }
        /// <summary>Gets the signed maximum axial force in native force units.</summary>
        public double MaximumValue { get; }
        /// <summary>Gets the native nonnegative station of the maximum, measured from the analytical start.</summary>
        public double MaximumPosition { get; }
    }
}
