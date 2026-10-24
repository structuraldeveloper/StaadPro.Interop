namespace StaadPro.Interop.Enums
{
    /// <summary>Identifies the coordinate frame of a member-end offset.</summary>
    /// <remarks>Matches CreateMemberOffsetSpec. ElementNodeOffsetAxis uses different numbering.</remarks>
    public enum OffsetAxis
    {
        /// <summary>Components along global model axes; native code zero.</summary>
        Global = 0,
        /// <summary>Components along member-local axes; native code one.</summary>
        Local = 1
    }
}
