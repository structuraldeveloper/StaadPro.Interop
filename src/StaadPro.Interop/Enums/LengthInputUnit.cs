namespace StaadPro.Interop.Enums
{
    /// <summary>Identifies a documented OpenSTAAD model input length unit.</summary>
    /// <remarks>Uses native numeric codes. Code 1 is an additional documented feet alias without a named enum member;
    /// the independent SetInputUnitForLength method accepts it, while existing SetInputUnitsEx named-value validation does not.
    /// Wrapper unit reads normalize supported feet aliases to Feet. Changing a managed enum value alone does not update STAAD.</remarks>
    public enum LengthInputUnit
    {
        /// <summary>Native code 0: inches.</summary>
        Inch = 0,
        /// <summary>Native code 2: feet; native code 1 is a separate unnamed alias.</summary>
        Feet = 2,
        /// <summary>Native code 3: centimetres.</summary>
        CentiMeter = 3,
        /// <summary>Native code 4: metres.</summary>
        Meter = 4,
        /// <summary>Native code 5: millimetres.</summary>
        MilliMeter = 5,
        /// <summary>Native code 6: decimetres.</summary>
        DeciMeter = 6,
        /// <summary>Native code 7: kilometres.</summary>
        KiloMeter = 7
    }
}
