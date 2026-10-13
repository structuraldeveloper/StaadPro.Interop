namespace StaadPro.Interop.Enums
{
    /// <summary>Classifies an OpenSTAAD load envelope using its native codes, zero through six.</summary>
    /// <remarks>The spelling Envelop is retained from the reference API. These codes describe envelope metadata, not calculated extrema.</remarks>
    public enum LoadEnvelopType
    {
        /// <summary>No envelope classification (native code 0).</summary>
        None = 0,
        /// <summary>Stress envelope (native code 1).</summary>
        Stress = 1,
        /// <summary>Serviceability envelope (native code 2).</summary>
        Serviceability = 2,
        /// <summary>Column envelope (native code 3).</summary>
        Column = 3,
        /// <summary>Connection envelope (native code 4).</summary>
        Connection = 4,
        /// <summary>Strength envelope (native code 5).</summary>
        Strength = 5,
        /// <summary>Temporary envelope (native code 6).</summary>
        Temporary = 6
    }
}
