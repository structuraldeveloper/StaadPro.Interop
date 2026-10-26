namespace StaadPro.Interop.Enums
{
    /// <summary>Preserves the reviewed native SupportType codes and compatible main-project vocabulary.</summary>
    public enum SupportType
    {
        /// <summary>Native general-error code; not a usable support.</summary>
        GeneralError = -1,
        /// <summary>No support assigned.</summary>
        NoSupport = 0,
        /// <summary>Native pinned support; actual torsional restraint must be read back.</summary>
        Pinned = 1,
        /// <summary>Native fixed support.</summary>
        Fixed = 2,
        /// <summary>Native fixed support with releases or springs.</summary>
        FixedBut = 3,
        /// <summary>Native enforced support.</summary>
        Enforced = 4,
        /// <summary>Native enforced support with releases.</summary>
        EnforcedBut = 5,
        /// <summary>Native inclined support.</summary>
        Inclined = 6,
        /// <summary>Native elastic footing.</summary>
        ElasticFooting = 7,
        /// <summary>Native elastic mat.</summary>
        ElasticMat = 8,
        /// <summary>Native plate mat.</summary>
        PlateMat = 9,
        /// <summary>Native multilinear spring.</summary>
        MultilinearSpring = 10,
        /// <summary>Native generated pinned support.</summary>
        GeneratedPinned = 11,
        /// <summary>Native generated fixed support.</summary>
        GeneratedFixed = 12,
        /// <summary>Native generated fixed support with releases.</summary>
        GeneratedFixedBut = 13
    }
}
