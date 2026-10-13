using Newtonsoft.Json;
using StaadPro.Interop.Interfaces;

namespace StaadPro.Interop.Base
{
    /// <summary>Immutable identity and title shared by combination definitions and native read-back.</summary>
    public abstract class LoadCombination : ILoadCombination
    {
        /// <summary>Initializes managed combination metadata without native access.</summary>
        /// <param name="id">Combination number, validated by the operation consuming this object.</param>
        /// <param name="title">Combination title, stored unchanged.</param>
        [JsonConstructor]
        public LoadCombination(int id, string title) { Id = id; Title = title; }

        /// <summary>Gets the combination number.</summary>
        [JsonProperty]
        public int Id { get; private set; }
        /// <summary>Gets the title supplied to the constructor.</summary>
        [JsonProperty]
        public string Title { get; private set; }
    }
}
