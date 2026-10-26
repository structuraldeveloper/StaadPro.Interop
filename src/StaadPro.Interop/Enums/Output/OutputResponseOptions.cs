namespace StaadPro.Interop.Enums.Output
{
    /// <summary>Selects the native axes for physical-member end actions without transforming returned values.</summary>
    public enum ResultCoordinateSystem
    {
        /// <summary>Native local physical-member axes, code 0.</summary>
        Local = 0,
        /// <summary>Native global model axes, code 1.</summary>
        Global = 1
    }
    /// <summary>Selects one native time-history response degree of freedom.</summary>
    public enum ResponseDegreeOfFreedom
    {
        /// <summary>Translation along native X; native Fx selector 1.</summary>
        TranslationX = 1,
        /// <summary>Translation along native Y; native Fy selector 2.</summary>
        TranslationY = 2,
        /// <summary>Translation along native Z; native Fz selector 3.</summary>
        TranslationZ = 3,
        /// <summary>Rotation about native X; native Mx selector 4.</summary>
        RotationX = 4,
        /// <summary>Rotation about native Y; native My selector 5.</summary>
        RotationY = 5,
        /// <summary>Rotation about native Z; native Mz selector 6.</summary>
        RotationZ = 6
    }
    /// <summary>Selects displacement, velocity or acceleration for a time-history degree of freedom.</summary>
    public enum TimeHistoryResponseKind
    {
        /// <summary>Native displacement response, code 0; rotational DOFs retain angular units.</summary>
        Displacement = 0,
        /// <summary>Native velocity response, code 1; translation/rotation units per second.</summary>
        Velocity = 1,
        /// <summary>Native acceleration response, code 2; translation/rotation units per second squared.</summary>
        Acceleration = 2
    }
    /// <summary>Classifies only explicit native steel-design PASS/FAIL text.</summary>
    /// <remarks>Retains the main-project Passed=0 and Failed=1 values. Numeric or unrecognized native status values are Unknown, never guessed from a utilization ratio.</remarks>
    public enum SteelDesignStatus
    {
        /// <summary>The native status was numeric, Boolean or unrecognized text; inspect the preserved evidence.</summary>
        Unknown = -1,
        /// <summary>Native status text explicitly reported PASS; source-compatible value 0.</summary>
        Passed = 0,
        /// <summary>Native status text explicitly reported FAIL; source-compatible value 1.</summary>
        Failed = 1
    }
}
