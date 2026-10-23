using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Loads;

namespace StaadPro.Interop.Models.Loads.Contracts
{
    /// <summary>Contains the native member command parameter packet with its original index and kind.</summary>
    /// <remarks>Force parameter dimensions and distance meanings depend on Kind. Slots are named rather than returned as arrays; no unsupported specialization or axis conversion is inferred.</remarks>
    public sealed class MemberLoadCommandInfo
    {
        internal MemberLoadCommandInfo(int loadItemIndex, LoadItemKind kind, LoadDirection direction, double firstForceParameter, double secondForceParameter, double thirdForceParameter, double firstDistance, double secondDistance, double thirdDistance)
        { LoadItemIndex = loadItemIndex; Kind = kind; Direction = direction; FirstForceParameter = firstForceParameter; SecondForceParameter = secondForceParameter; ThirdForceParameter = thirdForceParameter; FirstDistance = firstDistance; SecondDistance = secondDistance; ThirdDistance = thirdDistance; }
        /// <summary>Gets zero-based original item index.</summary>
        public int LoadItemIndex { get; }
        /// <summary>Gets original item code, including unknown positive codes.</summary>
        public LoadItemKind Kind { get; }
        /// <summary>Gets translated local/global/projected direction.</summary>
        public LoadDirection Direction { get; }
        /// <summary>Gets first native force parameter; dimensions depend on Kind.</summary>
        public double FirstForceParameter { get; }
        /// <summary>Gets second native force parameter.</summary>
        public double SecondForceParameter { get; }
        /// <summary>Gets third native force parameter.</summary>
        public double ThirdForceParameter { get; }
        /// <summary>Gets first native distance parameter in model length units.</summary>
        public double FirstDistance { get; }
        /// <summary>Gets second native distance parameter.</summary>
        public double SecondDistance { get; }
        /// <summary>Gets third native distance parameter.</summary>
        public double ThirdDistance { get; }
    }

    /// <summary>Contains one applied plate pressure definition with its local rectangular extent.</summary>
    /// <remarks>Applied loads are distinct from Output stresses. The native top-left/bottom-right coordinate order is preserved without imposing min/max ordering.</remarks>
    public sealed class PlatePressureLoad
    {
        internal PlatePressureLoad(LoadDirection direction, double pressure, double topLeftX, double topLeftY, double bottomRightX, double bottomRightY)
        { Direction = direction; Pressure = pressure; TopLeftX = topLeftX; TopLeftY = topLeftY; BottomRightX = bottomRightX; BottomRightY = bottomRightY; }
        /// <summary>Gets translated native load direction.</summary>
        public LoadDirection Direction { get; }
        /// <summary>Gets signed applied pressure in native pressure units.</summary>
        public double Pressure { get; }
        /// <summary>Gets first local X coordinate.</summary>
        public double TopLeftX { get; }
        /// <summary>Gets first local Y coordinate.</summary>
        public double TopLeftY { get; }
        /// <summary>Gets second local X coordinate.</summary>
        public double BottomRightX { get; }
        /// <summary>Gets second local Y coordinate.</summary>
        public double BottomRightY { get; }
    }

    /// <summary>Contains one applied concentrated plate load at a local position.</summary>
    /// <remarks>Reuses the field semantics of main ElementConcentratedLoad, whose native ApplyTo is unimplemented. No stub or proprietary base class is ported. Magnitude preserves native force/quantity interpretation pending qualification.</remarks>
    public sealed class PlateConcentratedLoad
    {
        internal PlateConcentratedLoad(LoadDirection direction, double magnitude, double localX, double localY)
        { Direction = direction; Magnitude = magnitude; LocalX = localX; LocalY = localY; }
        /// <summary>Gets translated native load direction.</summary>
        public LoadDirection Direction { get; }
        /// <summary>Gets signed native concentrated-load magnitude.</summary>
        public double Magnitude { get; }
        /// <summary>Gets application point local X coordinate.</summary>
        public double LocalX { get; }
        /// <summary>Gets application point local Y coordinate.</summary>
        public double LocalY { get; }
    }

    /// <summary>Contains the original pressure-command packet and four local coordinate parameters.</summary>
    /// <remarks>Native GetElementLoadInfo supports only ElementLoadPressure and documents direction codes 0-8. The future reader must translate them to LoadDirection 1-9; unrelated element types remain unsupported.</remarks>
    public sealed class PlateLoadCommandInfo
    {
        internal PlateLoadCommandInfo(int loadItemIndex, LoadDirection direction, double firstPressure, double secondPressure, double thirdPressure, double fourthPressure, double firstLocalX, double firstLocalY, double secondLocalX, double secondLocalY)
        { LoadItemIndex = loadItemIndex; Direction = direction; FirstPressure = firstPressure; SecondPressure = secondPressure; ThirdPressure = thirdPressure; FourthPressure = fourthPressure; FirstLocalX = firstLocalX; FirstLocalY = firstLocalY; SecondLocalX = secondLocalX; SecondLocalY = secondLocalY; }
        /// <summary>Gets zero-based original native item index.</summary>
        public int LoadItemIndex { get; }
        /// <summary>Gets semantic direction translated from this API’s zero-based native code.</summary>
        public LoadDirection Direction { get; }
        /// <summary>Gets first native pressure parameter.</summary>
        public double FirstPressure { get; }
        /// <summary>Gets second native pressure parameter.</summary>
        public double SecondPressure { get; }
        /// <summary>Gets third native pressure parameter.</summary>
        public double ThirdPressure { get; }
        /// <summary>Gets fourth native pressure parameter.</summary>
        public double FourthPressure { get; }
        /// <summary>Gets native first local X parameter.</summary>
        public double FirstLocalX { get; }
        /// <summary>Gets native first local Y parameter.</summary>
        public double FirstLocalY { get; }
        /// <summary>Gets native second local X parameter.</summary>
        public double SecondLocalX { get; }
        /// <summary>Gets native second local Y parameter.</summary>
        public double SecondLocalY { get; }
    }

    /// <summary>Associates one native beam number with its floor influence area.</summary>
    /// <remarks>Preserves pairing and native area units. Does not calculate tributary areas in managed code.</remarks>
    public sealed class BeamInfluenceArea
    {
        internal BeamInfluenceArea(int beamId, double area)
        { BeamId = beamId; Area = area; }
        /// <summary>Gets positive native beam number.</summary>
        public int BeamId { get; }
        /// <summary>Gets native influence area in squared native length units.</summary>
        public double Area { get; }
    }

    /// <summary>Contains one ordered reference-case and factor pair.</summary>
    /// <remarks>Separate from LoadFactorContribution, whose existing contract explicitly identifies primary repeat cases. Retains duplicate entries and finite signed/zero factors.</remarks>
    public sealed class ReferenceLoadContribution
    {
        internal ReferenceLoadContribution(int loadCaseId, double factor)
        { LoadCaseId = loadCaseId; Factor = factor; }
        /// <summary>Gets positive reference-case number.</summary>
        public int LoadCaseId { get; }
        /// <summary>Gets dimensionless contribution factor.</summary>
        public double Factor { get; }
    }

    /// <summary>Preserves an undecoded native GetAttribute response without inventing an attribute schema.</summary>
    /// <remarks>The reviewed help lists only 0 success/-1 error and no payload. This is a diagnostic contract only; GetLoadAttribute remains withheld from IOSLoad until meaning is verified. NativeValue must not be interpreted as a LoadStrengthType.</remarks>
    public sealed class LoadAttributeObservation
    {
        internal LoadAttributeObservation(int nativeValue)
        { NativeValue = nativeValue; }
        /// <summary>Gets original undecoded native value; no enum or attribute meaning is asserted.</summary>
        public int NativeValue { get; }
    }
}
