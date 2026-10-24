namespace StaadPro.Interop.Enums.Property
{
    /// <summary>Preserves the native CreateIsotropicMaterialProperties outcome independently of a material list index.</summary>
    public enum MaterialCreationStatus
    {
        /// <summary>A new named material was created; native code zero.</summary>
        Created = 0,
        /// <summary>The existing named material was updated; native code one.</summary>
        Updated = 1
    }
}
