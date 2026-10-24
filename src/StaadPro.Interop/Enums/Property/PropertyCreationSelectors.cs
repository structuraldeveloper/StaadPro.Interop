namespace StaadPro.Interop.Enums.Property
{
    /// <summary>Identifies the native solid-shape selector accepted by CreateBeamPropertyFromTableEx.</summary>
    public enum TableSolidShape
    {
        /// <summary>Plate strip (native one).</summary>
        PlateStrip = 1,
        /// <summary>Solid rectangle (native two).</summary>
        SolidRectangle = 2,
        /// <summary>Solid round (native three).</summary>
        SolidRound = 3,
        /// <summary>Round (native four), kept distinct from SolidRound.</summary>
        Round = 4,
        /// <summary>Cable (native five).</summary>
        Cable = 5
    }
    /// <summary>Identifies the native tapered-tube cross-section selector.</summary>
    public enum TaperedTubeShape
    {
        /// <summary>Round tube (native zero).</summary>
        Round = 0,
        /// <summary>Sixteen-sided tube; the native table spells this HexDecagonal (native one).</summary>
        Hexadecagonal = 1,
        /// <summary>Twelve-sided tube (native two).</summary>
        Dodecagonal = 2,
        /// <summary>Eight-sided tube (native three).</summary>
        Octagonal = 3,
        /// <summary>Six-sided tube (native four).</summary>
        Hexagonal = 4,
        /// <summary>Square tube (native five).</summary>
        Square = 5
    }
    /// <summary>Identifies the meaning and dimension of a cable definition value.</summary>
    public enum CableDefinitionKind
    {
        /// <summary>Initial tension in native force units (native zero).</summary>
        InitialTension = 0,
        /// <summary>Unstressed cable length in native length units (native one).</summary>
        UnstressedLength = 1
    }
    /// <summary>Identifies the tension-end selector for advanced cable analysis.</summary>
    public enum CableTensionEnd
    {
        /// <summary>Neither start nor end node is considered (native zero).</summary>
        Unspecified = 0,
        /// <summary>The cable start node is considered (native one).</summary>
        Start = 1,
        /// <summary>The cable end node is considered (native two).</summary>
        End = 2
    }
    /// <summary>Identifies the native control/dependent rigidity mode.</summary>
    public enum ControlDependencyMode
    {
        /// <summary>All directions rigid (native zero).</summary>
        AllDirections = 0,
        /// <summary>Native XY rigid-plane selector (native one); no coordinate transform is inferred.</summary>
        RigidXY = 1,
        /// <summary>Native YZ rigid-plane selector (native two).</summary>
        RigidYZ = 2,
        /// <summary>Native ZX rigid-plane selector (native three).</summary>
        RigidZX = 3,
        /// <summary>Use explicit FX/FY/FZ/MX/MY/MZ flags; native four selects the documented other-values branch.</summary>
        SpecifiedDegreesOfFreedom = 4
    }
    /// <summary>Identifies the native member fireproofing definition type.</summary>
    public enum FireProofingType
    {
        /// <summary>Block fireproofing, BFP (native one).</summary>
        Block = 1,
        /// <summary>Contour fireproofing, CFP (native two).</summary>
        Contour = 2
    }
}
