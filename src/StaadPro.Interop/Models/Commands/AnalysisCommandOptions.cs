using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.AnalysisCommands;
using System;
using StaadPro.Interop.Services.Commands;

namespace StaadPro.Interop.Models.Commands
{
    /// <summary>Typed options for OpenSTAAD PerformPDeltaAnalysisEx.</summary>
    /// <remarks>Configures model instructions; it does not execute analysis. Values are validated and copied into fresh native packets before dispatch. Do not mutate this object or its nested settings concurrently with a call. Native units and signs are retained.</remarks>
    public sealed class PDeltaAnalysisOptions
    {
        /// <summary>Gets or sets the positive number of P-Delta iterations; default 15.</summary>
        public int NumberOfIterations { get; set; } = 15;
        /// <summary>Gets or sets the native print selector; ModeShapes (4) is rejected by cable and buckling commands.</summary>
        public PrintOption PrintOption { get; set; } = PrintOption.NoPrint;
        /// <summary>Gets or sets the explicit P-Large-Delta (0) or P-Small-Delta (1) formulation.</summary>
        public PDeltaDisplacementEffect DisplacementEffect { get; set; } = PDeltaDisplacementEffect.SmallDelta;
        /// <summary>Gets or sets whether to include the geometric stiffness matrix; marshaled as a 0/1 short flag.</summary>
        public bool IncludeGeometricStiffness { get; set; }

        internal void Validate()
        {
            AnalysisCommandOptionValidation.Positive(NumberOfIterations, nameof(NumberOfIterations));
            AnalysisCommandOptionValidation.EnumValue(PrintOption, nameof(PrintOption));
            AnalysisCommandOptionValidation.EnumValue(DisplacementEffect, nameof(DisplacementEffect));
        }
    }

    /// <summary>Additional switches used by advanced cable analysis.</summary>
    /// <remarks>Configures model instructions; it does not execute analysis. Values are validated and copied into fresh native packets before dispatch. Do not mutate this object or its nested settings concurrently with a call. Native units and signs are retained.</remarks>
    public sealed class AdvancedCableAnalysisOptions
    {
        /// <summary>Gets or sets whether advanced cable analysis uses full Newton-Raphson; default true.</summary>
        public bool UseFullNewtonRaphson { get; set; } = true;
        /// <summary>Gets or sets whether to include the geometric stiffness matrix; marshaled as a 0/1 short flag.</summary>
        public bool IncludeGeometricStiffness { get; set; }

        internal short[] ToOpenStaadArray()
        {
            return new[]
            {
                Convert.ToInt16(UseFullNewtonRaphson),
                Convert.ToInt16(IncludeGeometricStiffness)
            };
        }
    }

    /// <summary>Basic cable-analysis parameters in documented OpenSTAAD array order.</summary>
    /// <remarks>Configures model instructions; it does not execute analysis. Values are validated and copied into fresh native packets before dispatch. Do not mutate this object or its nested settings concurrently with a call. Native units and signs are retained.</remarks>
    public sealed class BasicCableAnalysisParameters
    {
        /// <summary>Gets or sets the number of load increments; basic cable requires 5..145, advanced cable and nonlinear analysis require a positive integer.</summary>
        public int LoadSteps { get; set; } = 145;
        /// <summary>Gets or sets the maximum equilibrium iterations; basic cable requires 10..500 and advanced cable a positive integer.</summary>
        public int EquilibriumIterations { get; set; } = 300;
        /// <summary>Gets or sets the finite positive equilibrium tolerance; default 1e-4 basic or 1e-6 advanced.</summary>
        public double EquilibriumTolerance { get; set; } = 1.0E-4;
        /// <summary>Gets or sets the finite native minimum sag value; no length conversion or sign substitution is performed.</summary>
        public double MinimumSag { get; set; }
        /// <summary>Gets or sets the basic-cable stabilization value from 0 through 1000 inclusive; default 1.</summary>
        public double StabilityStiffness { get; set; } = 1.0;
        /// <summary>Gets or sets the positive number of initial load steps using stabilization; default 1.</summary>
        public int StabilityLoadSteps { get; set; } = 1;
        /// <summary>Gets or sets the basic-cable small-stiffness value from 0 through 1 inclusive.</summary>
        public double SmallStiffness { get; set; }

        internal double[] ToOpenStaadArray()
        {
            if (LoadSteps < 5 || LoadSteps > 145)
                throw new ArgumentOutOfRangeException(nameof(LoadSteps), "Basic cable load steps must be from 5 through 145.");
            if (EquilibriumIterations < 10 || EquilibriumIterations > 500)
                throw new ArgumentOutOfRangeException(nameof(EquilibriumIterations), "Basic cable equilibrium iterations must be from 10 through 500.");
            AnalysisCommandOptionValidation.Positive(EquilibriumTolerance, nameof(EquilibriumTolerance));
            AnalysisCommandOptionValidation.Finite(MinimumSag, nameof(MinimumSag));
            AnalysisCommandOptionValidation.Range(StabilityStiffness, 0.0, 1000.0, nameof(StabilityStiffness));
            AnalysisCommandOptionValidation.Positive(StabilityLoadSteps, nameof(StabilityLoadSteps));
            AnalysisCommandOptionValidation.Range(SmallStiffness, 0.0, 1.0, nameof(SmallStiffness));
            return new[]
            {
                (double)LoadSteps,
                EquilibriumIterations,
                EquilibriumTolerance,
                MinimumSag,
                StabilityStiffness,
                StabilityLoadSteps,
                SmallStiffness
            };
        }
    }

    /// <summary>Advanced cable-analysis parameters in documented OpenSTAAD array order.</summary>
    /// <remarks>Configures model instructions; it does not execute analysis. Values are validated and copied into fresh native packets before dispatch. Do not mutate this object or its nested settings concurrently with a call. Native units and signs are retained.</remarks>
    public sealed class AdvancedCableAnalysisParameters
    {
        /// <summary>Gets or sets the number of load increments; basic cable requires 5..145, advanced cable and nonlinear analysis require a positive integer.</summary>
        public int LoadSteps { get; set; } = 1;
        /// <summary>Gets or sets the maximum equilibrium iterations; basic cable requires 10..500 and advanced cable a positive integer.</summary>
        public int EquilibriumIterations { get; set; } = 300;
        /// <summary>Gets or sets the finite positive equilibrium tolerance; default 1e-4 basic or 1e-6 advanced.</summary>
        public double EquilibriumTolerance { get; set; } = 1.0E-6;

        internal double[] ToOpenStaadArray()
        {
            AnalysisCommandOptionValidation.Positive(LoadSteps, nameof(LoadSteps));
            AnalysisCommandOptionValidation.Positive(EquilibriumIterations, nameof(EquilibriumIterations));
            AnalysisCommandOptionValidation.Positive(EquilibriumTolerance, nameof(EquilibriumTolerance));
            return new[]
            {
                (double)LoadSteps,
                EquilibriumIterations,
                EquilibriumTolerance
            };
        }
    }

    /// <summary>Complete typed input for OpenSTAAD PerformCableAnalysisEx.</summary>
    /// <remarks>Configures model instructions; it does not execute analysis. Values are validated and copied into fresh native packets before dispatch. Do not mutate this object or its nested settings concurrently with a call. Native units and signs are retained.</remarks>
    public sealed class CableAnalysisOptions
    {
        /// <summary>Gets or sets the Basic or Advanced cable algorithm; only its selected parameter packet is marshaled.</summary>
        public CableAnalysisMethod Method { get; set; } = CableAnalysisMethod.Basic;
        /// <summary>Gets or sets the native print selector; ModeShapes (4) is rejected by cable and buckling commands.</summary>
        public PrintOption PrintOption { get; set; } = PrintOption.NoPrint;
        /// <summary>Gets or sets the owned-by-caller basic cable settings; required even when Advanced is selected, matching the source contract.</summary>
        public BasicCableAnalysisParameters BasicParameters { get; set; } = new BasicCableAnalysisParameters();
        /// <summary>Gets or sets the owned-by-caller advanced cable settings; required even when Basic is selected, matching the source contract.</summary>
        public AdvancedCableAnalysisParameters AdvancedParameters { get; set; } = new AdvancedCableAnalysisParameters();
        /// <summary>Gets or sets the owned-by-caller advanced cable flags; required; Basic dispatch sends a native null options packet.</summary>
        public AdvancedCableAnalysisOptions AdvancedOptions { get; set; } = new AdvancedCableAnalysisOptions();

        internal object CreateAdditionalOptionsArray()
        {
            Validate();
            return Method == CableAnalysisMethod.Advanced ? (object)AdvancedOptions.ToOpenStaadArray() : null;
        }

        internal object CreateParameterArray()
        {
            Validate();
            return Method == CableAnalysisMethod.Advanced ? (object)AdvancedParameters.ToOpenStaadArray() : BasicParameters.ToOpenStaadArray();
        }

        internal void Validate()
        {
            AnalysisCommandOptionValidation.EnumValue(Method, nameof(Method));
            AnalysisCommandOptionValidation.EnumValue(PrintOption, nameof(PrintOption));
            if (BasicParameters == null)
                throw new ArgumentNullException(nameof(BasicParameters));
            if (AdvancedParameters == null)
                throw new ArgumentNullException(nameof(AdvancedParameters));
            if (AdvancedOptions == null)
                throw new ArgumentNullException(nameof(AdvancedOptions));
        }
    }

    /// <summary>Complete typed input for AISC Direct Analysis.</summary>
    /// <remarks>Configures model instructions; it does not execute analysis. Values are validated and copied into fresh native packets before dispatch. Do not mutate this object or its nested settings concurrently with a call. Native units and signs are retained.</remarks>
    public sealed class DirectAnalysisOptions
    {
        /// <summary>Gets or sets the AISC Direct Analysis format, LRFD (1) or ASD (2).</summary>
        public DirectAnalysisOption DesignFormat { get; set; } = DirectAnalysisOption.Lrfd;
        /// <summary>Gets or sets the finite Tau-b tolerance from 0.001 through 1 inclusive; default 0.01.</summary>
        public double TauBTolerance { get; set; } = 0.01;
        /// <summary>Gets or sets the finite positive native displacement tolerance; default 0.01; translation and rotation keep their native conventions.</summary>
        public double DisplacementTolerance { get; set; } = 0.01;
        /// <summary>Gets or sets the positive Direct Analysis iteration limit; default 1.</summary>
        public int DirectIterations { get; set; } = 1;
        /// <summary>Gets or sets the Direct Analysis P-Delta iteration count from 1 through 15; larger values are rejected rather than silently clamped.</summary>
        public int PDeltaIterations { get; set; } = 15;
        /// <summary>Gets or sets whether to use reduced EI; marshaled independently of Tau-b iteration.</summary>
        public bool UseReducedFlexuralStiffness { get; set; }
        /// <summary>Gets or sets whether to iterate Tau-b; default false.</summary>
        public bool IterateTauB { get; set; }
        /// <summary>Gets or sets the native print selector; ModeShapes (4) is rejected by cable and buckling commands.</summary>
        public PrintOption PrintOption { get; set; } = PrintOption.NoPrint;

        internal double[] CreateParameterArray()
        {
            Validate();
            return new[]
            {
                TauBTolerance,
                DisplacementTolerance,
                (double)DirectIterations,
                PDeltaIterations
            };
        }

        internal short[] CreateAdditionalOptionsArray()
        {
            Validate();
            return new[]
            {
                Convert.ToInt16(UseReducedFlexuralStiffness),
                Convert.ToInt16(IterateTauB)
            };
        }

        internal void Validate()
        {
            AnalysisCommandOptionValidation.EnumValue(DesignFormat, nameof(DesignFormat));
            AnalysisCommandOptionValidation.EnumValue(PrintOption, nameof(PrintOption));
            AnalysisCommandOptionValidation.Range(TauBTolerance, 0.001, 1.0, nameof(TauBTolerance));
            AnalysisCommandOptionValidation.Positive(DisplacementTolerance, nameof(DisplacementTolerance));
            AnalysisCommandOptionValidation.Positive(DirectIterations, nameof(DirectIterations));
            AnalysisCommandOptionValidation.Positive(PDeltaIterations, nameof(PDeltaIterations));
            if (PDeltaIterations > 15)
                throw new ArgumentOutOfRangeException(nameof(PDeltaIterations), "OpenSTAAD resets Direct Analysis P-Delta iterations above 15.");
        }
    }

    /// <summary>Optional target used by nonlinear displacement-control analysis.</summary>
    /// <remarks>Configures model instructions; it does not execute analysis. Values are validated and copied into fresh native packets before dispatch. Do not mutate this object or its nested settings concurrently with a call. Native units and signs are retained.</remarks>
    public sealed class NonlinearDisplacementTarget
    {
        /// <summary>Gets or sets the positive analytical joint ID monitored for displacement control, not an index.</summary>
        public int JointId { get; set; }
        /// <summary>Gets or sets the global monitored DOF: translations X/Y/Z (1..3) or rotations X/Y/Z (4..6).</summary>
        public GlobalDegreeOfFreedom DegreeOfFreedom { get; set; } = GlobalDegreeOfFreedom.TranslationX;
        /// <summary>Gets or sets the finite native displacement target; preserve sign and the native length/rotation convention without conversion.</summary>
        public double TargetValue { get; set; }

        internal double[] ToOpenStaadArray()
        {
            AnalysisCommandOptionValidation.Positive(JointId, nameof(JointId));
            AnalysisCommandOptionValidation.EnumValue(DegreeOfFreedom, nameof(DegreeOfFreedom));
            AnalysisCommandOptionValidation.Finite(TargetValue, nameof(TargetValue));
            return new[]
            {
                (double)JointId,
                (double)DegreeOfFreedom,
                TargetValue
            };
        }
    }

    /// <summary>Complete typed input for OpenSTAAD PerformNonlinearAnalysisEx.</summary>
    /// <remarks>Configures model instructions; it does not execute analysis. Values are validated and copied into fresh native packets before dispatch. Do not mutate this object or its nested settings concurrently with a call. Native units and signs are retained.</remarks>
    public sealed class NonlinearAnalysisOptions
    {
        /// <summary>Gets or sets the native print selector; ModeShapes (4) is rejected by cable and buckling commands.</summary>
        public PrintOption PrintOption { get; set; } = PrintOption.NoPrint;
        /// <summary>Gets or sets the finite nonnegative first-step absolute displacement limit; zero disables displacement control.</summary>
        public double ArcLength { get; set; }
        /// <summary>Gets or sets the positive nonlinear equilibrium iteration limit; default 5.</summary>
        public int MaximumIterations { get; set; } = 5;
        /// <summary>Gets or sets the finite positive nonlinear convergence tolerance; default 1e-9.</summary>
        public double ConvergenceTolerance { get; set; } = 1.0E-9;
        /// <summary>Gets or sets the number of load increments; basic cable requires 5..145, advanced cable and nonlinear analysis require a positive integer.</summary>
        public int LoadSteps { get; set; } = 1;
        /// <summary>Gets or sets whether to rebuild once per load step (0) or at every step and iteration (1).</summary>
        public StiffnessMatrixRebuildFrequency RebuildFrequency { get; set; } = StiffnessMatrixRebuildFrequency.OncePerLoadStep;
        /// <summary>Gets or sets whether to include the geometric stiffness matrix; marshaled as a 0/1 short flag.</summary>
        public bool IncludeGeometricStiffness { get; set; }
        /// <summary>Gets or sets the optional displacement-control target; null sends a native null packet, not a fabricated default target.</summary>
        public NonlinearDisplacementTarget DisplacementTarget { get; set; }

        internal object CreateDisplacementTargetArray()
        {
            Validate();
            return DisplacementTarget?.ToOpenStaadArray();
        }

        internal void Validate()
        {
            AnalysisCommandOptionValidation.EnumValue(PrintOption, nameof(PrintOption));
            AnalysisCommandOptionValidation.EnumValue(RebuildFrequency, nameof(RebuildFrequency));
            AnalysisCommandOptionValidation.Finite(ArcLength, nameof(ArcLength));
            if (ArcLength < 0.0)
                throw new ArgumentOutOfRangeException(nameof(ArcLength));
            AnalysisCommandOptionValidation.Positive(MaximumIterations, nameof(MaximumIterations));
            AnalysisCommandOptionValidation.Positive(ConvergenceTolerance, nameof(ConvergenceTolerance));
            AnalysisCommandOptionValidation.Positive(LoadSteps, nameof(LoadSteps));
        }
    }
}
