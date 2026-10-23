namespace StaadPro.Interop.Enums.Loads
{
    /// <summary>Identifies command-kind codes accepted by native command counts and assignment counts.</summary>
    /// <remarks>Distinct from LoadType (case classification) and LoadItemKind (item inspection). Codes 3260 and 3310 intentionally cover multiple documented command forms. Undefined request codes are rejected.</remarks>
    public enum LoadCommandKind
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
        /// <summary>Member Trapezoidal Or Hydrostatic (native code 3260).</summary>
        MemberTrapezoidalOrHydrostatic = 3260,
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
        /// <summary>Plate Pressure Family (native code 3310).</summary>
        PlatePressureFamily = 3310,
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
    }
}
