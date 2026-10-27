using System.ComponentModel;

namespace StaadPro.Interop.Enums.AnalysisCommands
{
    /// <summary>OpenSTAAD P-Delta displacement formulation.</summary>
    public enum PDeltaDisplacementEffect
    {
        /// <summary>P-Large-Delta effect.</summary>
        [Description("P-Large-Delta effect")]
        LargeDelta = 0,
        /// <summary>P-Small-Delta effect.</summary>
        [Description("P-Small-Delta effect")]
        SmallDelta = 1
    }

    /// <summary>AISC Direct Analysis design format.</summary>
    public enum DirectAnalysisOption
    {
        /// <summary>Load and Resistance Factor Design (LRFD).</summary>
        [Description("Load and Resistance Factor Design (LRFD)")]
        Lrfd = 1,
        /// <summary>Allowable Strength Design (ASD).</summary>
        [Description("Allowable Strength Design (ASD)")]
        Asd = 2
    }

    /// <summary>Cable-analysis solution algorithm.</summary>
    public enum CableAnalysisMethod
    {
        /// <summary>Basic cable analysis.</summary>
        [Description("Basic cable analysis")]
        Basic = 0,
        /// <summary>Advanced cable analysis.</summary>
        [Description("Advanced cable analysis")]
        Advanced = 1
    }

    /// <summary>Frequency used to rebuild the tangent or stiffness matrix.</summary>
    public enum StiffnessMatrixRebuildFrequency
    {
        /// <summary>Once per load step.</summary>
        [Description("Once per load step")]
        OncePerLoadStep = 0,
        /// <summary>Every load step and iteration.</summary>
        [Description("Every load step and iteration")]
        EveryLoadStepAndIteration = 1
    }

    /// <summary>Global degree of freedom monitored by nonlinear displacement control.</summary>
    public enum GlobalDegreeOfFreedom
    {
        /// <summary>Translation along global X.</summary>
        [Description("Translation along global X")]
        TranslationX = 1,
        /// <summary>Translation along global Y.</summary>
        [Description("Translation along global Y")]
        TranslationY = 2,
        /// <summary>Translation along global Z.</summary>
        [Description("Translation along global Z")]
        TranslationZ = 3,
        /// <summary>Rotation about global X.</summary>
        [Description("Rotation about global X")]
        RotationX = 4,
        /// <summary>Rotation about global Y.</summary>
        [Description("Rotation about global Y")]
        RotationY = 5,
        /// <summary>Rotation about global Z.</summary>
        [Description("Rotation about global Z")]
        RotationZ = 6
    }

    /// <summary>Design-code option accepted by CHECK SOFT STORY.</summary>
    public enum SoftStoryDesignCode
    {
        /// <summary>IS 1893:2002.</summary>
        [Description("IS 1893:2002")]
        Is1893_2002 = 1,
        /// <summary>ASCE 7-05/10/16.</summary>
        [Description("ASCE 7-05/10/16")]
        Asce7_2005_2010_2016 = 2,
        /// <summary>IS 1893:2016.</summary>
        [Description("IS 1893:2016")]
        Is1893_2016 = 3
    }

    /// <summary>Design-code option accepted by CHECK IRREGULARITIES.</summary>
    public enum IrregularitiesDesignCode
    {
        /// <summary>ASCE 7-16.</summary>
        [Description("ASCE 7-16")]
        Asce7_2016 = 2,
        /// <summary>IS 1893:2016.</summary>
        [Description("IS 1893:2016")]
        Is1893_2016 = 3
    }
}
