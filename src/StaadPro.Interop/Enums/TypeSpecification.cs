namespace StaadPro.Interop.Enums
{
    /// <summary>Preserves the main-project TypeSpecification names and integer table codes.</summary>
    /// <remarks>These codes identify native table modifiers. Creation functions accept different subsets; cover-plate creation accepts TC, BC and TB only.</remarks>
    public enum TypeSpecification
    {
        /// <summary>Native ST selector, code 0.</summary>
        ST = 0,
        /// <summary>Native RA selector, code 1.</summary>
        RA = 1,
        /// <summary>Native D selector, code 2.</summary>
        D = 2,
        /// <summary>Native LD selector, code 3.</summary>
        LD = 3,
        /// <summary>Native SD selector, code 4.</summary>
        SD = 4,
        /// <summary>Native T selector, code 5.</summary>
        T = 5,
        /// <summary>Native CM selector, code 6.</summary>
        CM = 6,
        /// <summary>Native TC selector, code 7.</summary>
        TC = 7,
        /// <summary>Native BC selector, code 8.</summary>
        BC = 8,
        /// <summary>Native TB selector, code 9.</summary>
        TB = 9,
        /// <summary>Native BA selector, code 10.</summary>
        BA = 10,
        /// <summary>Native FR selector, code 11.</summary>
        FR = 11,
        /// <summary>Native SA selector, code 12.</summary>
        SA = 12,
        /// <summary>Native UserDefined selector, code -1.</summary>
        UserDefined = -1,
    }
}
