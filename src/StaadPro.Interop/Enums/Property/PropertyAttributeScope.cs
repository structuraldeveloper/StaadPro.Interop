namespace StaadPro.Interop.Enums.Property
{
    /// <summary>Identifies the native attribute domain without inferring geometry from an integer ID.</summary>
    public enum PropertyAttributeScope
    {
        /// <summary>The explicitly named native member-attribute domain.</summary>
        Member,
        /// <summary>The explicitly named native element-attribute domain; no solid/plate hydration is performed.</summary>
        Element,
        /// <summary>The native ambiguous member/plate domain; a colliding integer ID is not resolved by this wrapper.</summary>
        MemberOrPlate
    }
    /// <summary>Identifies the ten reviewed native user-provided table types.</summary>
    public enum UserProvidedTableType
    {
        /// <summary>Wide flange (native one).</summary>
        WideFlange = 1,
        /// <summary>Channel (native two).</summary>
        Channel = 2,
        /// <summary>Angle (native three).</summary>
        Angle = 3,
        /// <summary>Double angle (native four).</summary>
        DoubleAngle = 4,
        /// <summary>Tee (native five).</summary>
        Tee = 5,
        /// <summary>Pipe (native six).</summary>
        Pipe = 6,
        /// <summary>Tube (native seven).</summary>
        Tube = 7,
        /// <summary>General (native eight).</summary>
        General = 8,
        /// <summary>I-section (native nine).</summary>
        ISection = 9,
        /// <summary>Prismatic (native ten).</summary>
        Prismatic = 10
    }
}
