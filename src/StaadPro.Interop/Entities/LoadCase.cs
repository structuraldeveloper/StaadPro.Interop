using Newtonsoft.Json;
using StaadPro.Interop.Common;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Entities
{
    /// <summary>
    /// Represents a primary or reference load case in STAAD.Pro.
    /// </summary>
    public class LoadCase : ValidatablePropertyStore, ILoadCase, IEntity
    {
        public LoadCase()
        {
            Type = LoadType.None;
            CaseType = LoadCaseType.PrimaryLoad;
        }

        public LoadCase(int id) : this()
        {
            Id = id;
        }

        public LoadCase(int id, string title, LoadCaseType caseType = LoadCaseType.PrimaryLoad, LoadType loadType = LoadType.None) : this(id)
        {
            Title = title;
            CaseType = caseType;
            Type = loadType;
        }

        public LoadCase(string title, LoadType loadType = LoadType.Dead) : this()
        {
            Title = title;
            Type = loadType;
            CaseType = LoadCaseType.PrimaryLoad;
        }

        [JsonProperty(Order = 1)]
        public int Id { get => Get<int>(); set => Set(value); }

        [JsonProperty(Order = 2)]
        public string Title { get => Get<string>(); set => Set(value); }

        [JsonProperty(Order = 3)]
        public LoadType Type { get => Get<LoadType>(); set => Set(value); }

        [JsonProperty(Order = 4)]
        public LoadCaseType CaseType { get => Get<LoadCaseType>(); set => Set(value); }

        public override bool Equals(object obj)
        {
            return obj is LoadCase other && Id == other.Id && CaseType == other.CaseType && Type == other.Type;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ CaseType.GetHashCode() ^ Type.GetHashCode();
        }

        public static bool operator ==(LoadCase left, LoadCase right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }

        public static bool operator !=(LoadCase left, LoadCase right) => !(left == right);
    }
}
