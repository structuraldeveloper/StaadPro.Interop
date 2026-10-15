namespace StaadPro.Interop.Enums
{
    /// <summary>Identifies a documented OpenSTAAD model input force unit.</summary>
    /// <remarks>Uses native numeric codes 0 through 7. Changing a managed enum value alone does not update STAAD;
    /// submit an explicit unit request and refresh cached wrapper units when needed.</remarks>
    public enum ForceInputUnit
    {
        /// <summary>Native code 0: kilopound (kip).</summary>
        KiloPound = 0,
        /// <summary>Native code 1: pound-force.</summary>
        Pound = 1,
        /// <summary>Native code 2: kilogram-force.</summary>
        KiloGram = 2,
        /// <summary>Native code 3: metric-ton force.</summary>
        MetricTon = 3,
        /// <summary>Native code 4: newton.</summary>
        Newton = 4,
        /// <summary>Native code 5: kilonewton.</summary>
        KiloNewton = 5,
        /// <summary>Native code 6: meganewton.</summary>
        MegaNewton = 6,
        /// <summary>Native code 7: decanewton.</summary>
        DecaNewton = 7
    }
}
