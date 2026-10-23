namespace StaadPro.Interop.Enums
{
    /// <summary>Identifies the analytical member end for an offset specification.</summary>
    /// <remarks>These codes are the native CreateMemberOffsetSpec location codes; they do not select a coordinate system.</remarks>
    public enum OffsetLocation
    {
        /// <summary>The start incidence of the member, native code zero.</summary>
        Start = 0,
        /// <summary>The end incidence of the member, native code one.</summary>
        End = 1
    }
}
