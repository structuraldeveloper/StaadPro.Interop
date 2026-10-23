namespace StaadPro.Interop.Enums.Loads
{
    /// <summary>Identifies documented native load-item codes returned by item-index inspection.</summary>
    /// <remarks>Unknown positive codes are retained by casting to this enum; use Enum.IsDefined when testing recognition. Code 3312 has two native labels and is intentionally ambiguous. This table is distinct from LoadCommandKind and the coarse managed LoadItemType.</remarks>
    public enum LoadItemKind
    {
        /// <summary>Nodal (native code 3110).</summary>
        Nodal = 3110,
        /// <summary>Inclined Nodal (native code 3120).</summary>
        InclinedNodal = 3120,
        /// <summary>Support Displacement (native code 3910).</summary>
        SupportDisplacement = 3910,
        /// <summary>Member Uniform Force (native code 3210).</summary>
        MemberUniformForce = 3210,
        /// <summary>Member Uniform Moment (native code 3220).</summary>
        MemberUniformMoment = 3220,
        /// <summary>Member Concentrated Force (native code 3230).</summary>
        MemberConcentratedForce = 3230,
        /// <summary>Member Concentrated Moment (native code 3240).</summary>
        MemberConcentratedMoment = 3240,
        /// <summary>Member Linear (native code 3250).</summary>
        MemberLinear = 3250,
        /// <summary>Physical Uniform Force (native code 3275).</summary>
        PhysicalUniformForce = 3275,
        /// <summary>Physical Uniform Moment (native code 3280).</summary>
        PhysicalUniformMoment = 3280,
        /// <summary>Physical Concentrated Force (native code 3285).</summary>
        PhysicalConcentratedForce = 3285,
        /// <summary>Physical Concentrated Moment (native code 3290).</summary>
        PhysicalConcentratedMoment = 3290,
        /// <summary>Physical Trapezoidal (native code 3295).</summary>
        PhysicalTrapezoidal = 3295,
        /// <summary>Plate Trapezoidal (native code 3320).</summary>
        PlateTrapezoidal = 3320,
        /// <summary>Solid (native code 3322).</summary>
        Solid = 3322,
        /// <summary>Temperature (native code 3710).</summary>
        Temperature = 3710,
        /// <summary>Strain (native code 3720).</summary>
        Strain = 3720,
        /// <summary>Strain Rate (native code 3721).</summary>
        StrainRate = 3721,
        /// <summary>Prestress (native code 3620).</summary>
        Prestress = 3620,
        /// <summary>Fixed End (native code 3810).</summary>
        FixedEnd = 3810,
        /// <summary>Floor Group (native code 3530).</summary>
        FloorGroup = 3530,
        /// <summary>Area (native code 3410).</summary>
        Area = 3410,
        /// <summary>One Way Floor Group (native code 3554).</summary>
        OneWayFloorGroup = 3554,
        /// <summary>Self Weight (native code 4000).</summary>
        SelfWeight = 4000,
        /// <summary>Member Trapezoidal (native code 3260).</summary>
        MemberTrapezoidal = 3260,
        /// <summary>Hydrostatic (native code 3261).</summary>
        Hydrostatic = 3261,
        /// <summary>Plate Full Pressure (native code 3310).</summary>
        PlateFullPressure = 3310,
        /// <summary>Plate Concentrated (native code 3311).</summary>
        PlateConcentrated = 3311,
        /// <summary>Ambiguous Region Nodal Or Partial Plate Pressure (native code 3312).</summary>
        AmbiguousRegionNodalOrPartialPlatePressure = 3312,
        /// <summary>Floor YRange (native code 3510).</summary>
        FloorYRange = 3510,
        /// <summary>Floor XRange (native code 3511).</summary>
        FloorXRange = 3511,
        /// <summary>Floor ZRange (native code 3520).</summary>
        FloorZRange = 3520,
        /// <summary>One Way Floor XRange (native code 3551).</summary>
        OneWayFloorXRange = 3551,
        /// <summary>One Way Floor YRange (native code 3552).</summary>
        OneWayFloorYRange = 3552,
        /// <summary>One Way Floor ZRange (native code 3553).</summary>
        OneWayFloorZRange = 3553,
        /// <summary>Seismic Ubc (native code 4400).</summary>
        SeismicUbc = 4400,
        /// <summary>Seismic Ibc (native code 4405).</summary>
        SeismicIbc = 4405,
        /// <summary>Seismic Is1893 (native code 4410).</summary>
        SeismicIs1893 = 4410,
        /// <summary>Seismic Aij (native code 4500).</summary>
        SeismicAij = 4500,
        /// <summary>Seismic Colombian (native code 4510).</summary>
        SeismicColombian = 4510,
        /// <summary>Seismic Cfe (native code 4520).</summary>
        SeismicCfe = 4520,
        /// <summary>Seismic Rpa (native code 4530).</summary>
        SeismicRpa = 4530,
        /// <summary>Seismic Ntc (native code 4540).</summary>
        SeismicNtc = 4540,
        /// <summary>Seismic Nrc (native code 4550).</summary>
        SeismicNrc = 4550,
        /// <summary>Seismic Nrc2005 (native code 4560).</summary>
        SeismicNrc2005 = 4560,
        /// <summary>Seismic Nrc2010 (native code 4561).</summary>
        SeismicNrc2010 = 4561,
        /// <summary>Seismic Turkish (native code 4570).</summary>
        SeismicTurkish = 4570,
        /// <summary>Seismic Gb50011 (native code 4575).</summary>
        SeismicGb50011 = 4575,
        /// <summary>Seismic Colombian2010 (native code 4576).</summary>
        SeismicColombian2010 = 4576,
        /// <summary>Wind (native code 4600).</summary>
        Wind = 4600,
        /// <summary>Dynamic Wind (native code 4610).</summary>
        DynamicWind = 4610,
        /// <summary>Time History (native code 4820).</summary>
        TimeHistory = 4820,
        /// <summary>Snow (native code 4650).</summary>
        Snow = 4650,
        /// <summary>Snow Data (native code 4651).</summary>
        SnowData = 4651,
        /// <summary>Repeat (native code 4200).</summary>
        Repeat = 4200,
        /// <summary>Repeat Data (native code 4201).</summary>
        RepeatData = 4201,
        /// <summary>Spectrum (native code 4100).</summary>
        Spectrum = 4100,
        /// <summary>Spectrum Data (native code 4101).</summary>
        SpectrumData = 4101,
        /// <summary>Natural Frequency (native code 4700).</summary>
        NaturalFrequency = 4700,
        /// <summary>Rayleigh Frequency (native code 4701).</summary>
        RayleighFrequency = 4701,
        /// <summary>Modal Calculation (native code 4710).</summary>
        ModalCalculation = 4710,
        /// <summary>Reference (native code 4220).</summary>
        Reference = 4220,
        /// <summary>Notional (native code 4222).</summary>
        Notional = 4222,
        /// <summary>Notional Data (native code 4223).</summary>
        NotionalData = 4223,
    }
}
