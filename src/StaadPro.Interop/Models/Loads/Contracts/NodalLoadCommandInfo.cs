using StaadPro.Interop.Services.Load;

namespace StaadPro.Interop.Models.Loads.Contracts
{
    /// <summary>Contains a six-component nodal command observation with its original item index.</summary>
    /// <remarks>Reuses the existing Forces type without exposing its mutable state. This differs from
    /// an entity-scoped NodalLoad, which does not preserve the original native command index.</remarks>
    public sealed class NodalLoadCommandInfo
    {
        private readonly Forces forces;
        internal NodalLoadCommandInfo(int loadItemIndex, Forces forces)
        { LoadItemIndex = loadItemIndex; this.forces = LoadContractArguments.Copy(forces, nameof(forces)); }
        /// <summary>Gets the original zero-based item index, subject to native index qualification.</summary>
        public int LoadItemIndex { get; }
        /// <summary>Gets an independent six-component Forces copy in native load units and axes.</summary>
        public Forces Forces => LoadContractArguments.Copy(forces, nameof(forces));
    }
}
