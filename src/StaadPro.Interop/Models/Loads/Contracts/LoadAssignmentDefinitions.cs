using System;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Loads;
using StaadPro.Interop.Services.Load;

namespace StaadPro.Interop.Models.Loads.Contracts
{
    /// <summary>Contains fixed-end force/moment components at both member ends.</summary>
    /// <remarks>Reuses the existing Forces model with defensive copies. Values preserve caller signs,
    /// native force and moment units; no support-reaction sign reversal is implied.</remarks>
    public sealed class FixedEndLoadDefinition
    {
        private readonly Forces start, end;
        /// <summary>Snapshots both six-component end definitions.</summary>
        /// <param name="startForces">Finite Fx/Fy/Fz/Mx/My/Mz at the member start.</param>
        /// <param name="endForces">Finite Fx/Fy/Fz/Mx/My/Mz at the member end.</param>
        /// <exception cref="ArgumentNullException">An end definition is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">A component is nonfinite.</exception>
        public FixedEndLoadDefinition(Forces startForces, Forces endForces)
        { start = LoadContractArguments.Copy(startForces, nameof(startForces)); end = LoadContractArguments.Copy(endForces, nameof(endForces)); }
        /// <summary>Gets an independent editable copy of the start components.</summary>
        public Forces StartForces => LoadContractArguments.Copy(start, nameof(start));
        /// <summary>Gets an independent editable copy of the end components.</summary>
        public Forces EndForces => LoadContractArguments.Copy(end, nameof(end));
    }

    /// <summary>Contains hydrostatic pressure limits and separate load/interpolation axes.</summary>
    public sealed class HydrostaticPressureDefinition
    {
        /// <summary>Creates a finite pressure-gradient request in current native pressure units.</summary>
        /// <param name="direction">LocalZ or GlobalX/Y/Z pressure direction.</param>
        /// <param name="interpolationAxis">Global interpolation axis, internally encoded as 1/2/3.</param>
        /// <param name="minimumPressure">Native minimum load parameter; signed value preserved.</param>
        /// <param name="maximumPressure">Native maximum load parameter; signed value preserved without reordering.</param>
        /// <exception cref="ArgumentOutOfRangeException">A direction is unsupported or a value nonfinite.</exception>
        public HydrostaticPressureDefinition(LoadDirection direction, GlobalAxis interpolationAxis, double minimumPressure, double maximumPressure)
        {
            Direction = PressureDirection(direction); InterpolationAxis = LoadContractArguments.Defined(interpolationAxis, nameof(interpolationAxis));
            MinimumPressure = LoadContractArguments.Finite(minimumPressure, nameof(minimumPressure)); MaximumPressure = LoadContractArguments.Finite(maximumPressure, nameof(maximumPressure));
        }
        internal static LoadDirection PressureDirection(LoadDirection direction)
        { if ((int)direction < 3 || (int)direction > 6) throw new ArgumentOutOfRangeException(nameof(direction)); return direction; }
        /// <summary>Gets the pressure vector direction.</summary>
        public LoadDirection Direction { get; }
        /// <summary>Gets the independent global interpolation axis.</summary>
        public GlobalAxis InterpolationAxis { get; }
        /// <summary>Gets the native minimum-pressure parameter.</summary>
        public double MinimumPressure { get; }
        /// <summary>Gets the native maximum-pressure parameter.</summary>
        public double MaximumPressure { get; }
    }

    /// <summary>Contains either a two-value local-axis variation or four pressures in plate joint order.</summary>
    /// <remarks>Reuses the main PlatePressure node-pressure convention without importing text generation,
    /// direction fallback or a Y-up fluid calculation. Factories prevent mixed two/four-pressure states.</remarks>
    public sealed class TrapezoidalPressureDefinition
    {
        private TrapezoidalPressureDefinition(LoadDirection direction, PlatePressureVariation variation, double first, double second, double? third, double? fourth)
        {
            Direction = HydrostaticPressureDefinition.PressureDirection(direction); Variation = variation;
            FirstPressure = LoadContractArguments.Finite(first, nameof(first)); SecondPressure = LoadContractArguments.Finite(second, nameof(second));
            ThirdPressure = third.HasValue ? LoadContractArguments.Finite(third.Value, nameof(third)) : (double?)null;
            FourthPressure = fourth.HasValue ? LoadContractArguments.Finite(fourth.Value, nameof(fourth)) : (double?)null;
        }
        /// <summary>Creates a two-value pressure variation.</summary>
        /// <param name="direction">LocalZ or GlobalX/Y/Z load direction.</param><param name="variation">LocalX or LocalY only.</param>
        /// <param name="startPressure">Signed starting pressure.</param><param name="endPressure">Signed ending pressure.</param>
        /// <returns>A two-value definition whose third/fourth pressures are absent.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The axis/direction is invalid or a pressure nonfinite.</exception>
        public static TrapezoidalPressureDefinition AlongLocalAxis(LoadDirection direction, PlatePressureVariation variation, double startPressure, double endPressure)
        {
            if (variation != PlatePressureVariation.LocalX && variation != PlatePressureVariation.LocalY) throw new ArgumentOutOfRangeException(nameof(variation));
            return new TrapezoidalPressureDefinition(direction, variation, startPressure, endPressure, null, null);
        }
        /// <summary>Creates four joint-ordered pressures.</summary>
        /// <param name="direction">LocalZ or GlobalX/Y/Z load direction.</param>
        /// <param name="pressureAtNodeA">Pressure at first plate joint.</param><param name="pressureAtNodeB">Pressure at second joint.</param>
        /// <param name="pressureAtNodeC">Pressure at third joint.</param><param name="pressureAtNodeD">Pressure at fourth joint; triangular conventions require native qualification.</param>
        /// <returns>A joint-variation definition preserving all four values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">A direction is invalid or a pressure nonfinite.</exception>
        public static TrapezoidalPressureDefinition AtCorners(LoadDirection direction, double pressureAtNodeA, double pressureAtNodeB, double pressureAtNodeC, double pressureAtNodeD) =>
            new TrapezoidalPressureDefinition(direction, PlatePressureVariation.Joints, pressureAtNodeA, pressureAtNodeB, pressureAtNodeC, pressureAtNodeD);
        /// <summary>Gets the pressure direction.</summary>
        public LoadDirection Direction { get; }
        /// <summary>Gets the explicit two-value or joint variation mode.</summary>
        public PlatePressureVariation Variation { get; }
        /// <summary>Gets start pressure or pressure at joint A.</summary>
        public double FirstPressure { get; }
        /// <summary>Gets end pressure or pressure at joint B.</summary>
        public double SecondPressure { get; }
        /// <summary>Gets pressure at joint C, or null for local-axis variation.</summary>
        public double? ThirdPressure { get; }
        /// <summary>Gets pressure at joint D, or null for local-axis variation.</summary>
        public double? FourthPressure { get; }
    }

    /// <summary>Contains a range-based or floor-group pressure request with explicit mode.</summary>
    /// <remarks>AddFloorLoad uses the extended native call for all supported modes; it does not silently fall back
    /// to the legacy fixed-direction function.</remarks>
    public sealed class FloorLoadDefinition
    {
        private FloorLoadDefinition(double pressure, GlobalAxis direction, FloorRegion region, string group, bool oneWay)
        { Pressure = LoadContractArguments.Finite(pressure, nameof(pressure)); Direction = LoadContractArguments.Defined(direction, nameof(direction)); Region = region; FloorGroupName = group; IsOneWay = oneWay; }
        /// <summary>Creates a coordinate-range floor pressure definition.</summary>
        /// <param name="pressure">Finite signed pressure in native units.</param><param name="direction">Global force axis.</param>
        /// <param name="region">Bounded floor range and its independent range axis.</param><param name="oneWay">Whether one-way distribution is requested.</param>
        /// <returns>A range definition with no group name.</returns>
        /// <exception cref="ArgumentNullException">Region is null.</exception><exception cref="ArgumentOutOfRangeException">Pressure or axis is invalid.</exception>
        public static FloorLoadDefinition ForRange(double pressure, GlobalAxis direction, FloorRegion region, bool oneWay = false) =>
            new FloorLoadDefinition(pressure, direction, region ?? throw new ArgumentNullException(nameof(region)), null, oneWay);
        /// <summary>Creates a named floor-group pressure definition.</summary>
        /// <param name="pressure">Finite signed pressure.</param><param name="direction">Global force axis.</param><param name="floorGroupName">Nonblank FLOOR group name; native group existence is checked at submission.</param>
        /// <returns>A group definition with no range or one-way mode.</returns>
        /// <exception cref="ArgumentException">Group name is blank or the reserved native value 0.</exception><exception cref="ArgumentOutOfRangeException">Pressure or axis is invalid.</exception>
        public static FloorLoadDefinition ForGroup(double pressure, GlobalAxis direction, string floorGroupName)
        {
            LoadContractArguments.Text(floorGroupName, nameof(floorGroupName));
            if (floorGroupName.Trim() == "0") throw new ArgumentException("0 is a native one-way marker, not a group name.", nameof(floorGroupName));
            return new FloorLoadDefinition(pressure, direction, null, floorGroupName, false);
        }
        /// <summary>Gets finite signed floor pressure.</summary>
        public double Pressure { get; }
        /// <summary>Gets the global loading axis, not the range axis.</summary>
        public GlobalAxis Direction { get; }
        /// <summary>Gets the range, or null for group mode.</summary>
        public FloorRegion Region { get; }
        /// <summary>Gets the group name, or null for range mode.</summary>
        public string FloorGroupName { get; }
        /// <summary>Gets whether this is a group-mode definition.</summary>
        public bool UsesFloorGroup => FloorGroupName != null;
        /// <summary>Gets whether one-way distribution is selected in range mode.</summary>
        public bool IsOneWay { get; }
    }

    /// <summary>Contains a wind-load application request, separate from wind-definition creation.</summary>
    /// <remarks>Native range conventions assume Y vertical; the service rejects Z-up or unavailable geometry before assignment.</remarks>
    public sealed class WindLoadDefinition
    {
        /// <summary>Creates an explicit wind application request.</summary>
        /// <param name="windDefinitionId">Positive existing wind-definition number.</param><param name="direction">Defined X/Z signed wind direction.</param>
        /// <param name="factor">Finite signed dimensionless multiplier.</param><param name="isOpenStructure">Explicit open/closed structure choice.</param><param name="region">Explicit global range; no all-model default.</param>
        /// <exception cref="ArgumentOutOfRangeException">ID, factor or direction is invalid.</exception><exception cref="ArgumentNullException">Region is null.</exception>
        public WindLoadDefinition(int windDefinitionId, WindLoadDirection direction, double factor, bool isOpenStructure, GlobalLoadRegion region)
        {
            WindDefinitionId = LoadContractArguments.Positive(windDefinitionId, nameof(windDefinitionId)); Direction = LoadContractArguments.Defined(direction, nameof(direction));
            Factor = LoadContractArguments.Finite(factor, nameof(factor)); IsOpenStructure = isOpenStructure; Region = region ?? throw new ArgumentNullException(nameof(region));
        }
        /// <summary>Gets the existing wind definition number.</summary>
        public int WindDefinitionId { get; }
        /// <summary>Gets the signed global X/Z direction.</summary>
        public WindLoadDirection Direction { get; }
        /// <summary>Gets the dimensionless signed factor.</summary>
        public double Factor { get; }
        /// <summary>Gets the explicit native open-structure flag.</summary>
        public bool IsOpenStructure { get; }
        /// <summary>Gets the bounded global application region.</summary>
        public GlobalLoadRegion Region { get; }
    }
}
