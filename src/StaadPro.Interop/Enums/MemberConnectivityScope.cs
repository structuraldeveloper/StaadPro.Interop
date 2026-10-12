namespace StaadPro.Interop.Enums
{
    /// <summary>Specifies which member nodes participate in connectivity grouping.</summary>
    public enum MemberConnectivityScope
    {
        /// <summary>Only StartNode and EndNode participate; interior nodes do not connect members.</summary>
        EndpointsOnly = 0,
        /// <summary>Uses the Nodes collection of both members, including constituent-beam interior nodes.</summary>
        AnyNode = 1
    }
}
