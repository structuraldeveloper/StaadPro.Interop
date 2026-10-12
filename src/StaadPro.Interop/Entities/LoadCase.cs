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
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCase..ctor()']/*" />
        public LoadCase()
        {
            Type = LoadType.None;
            CaseType = LoadCaseType.PrimaryLoad;
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCase..ctor(int)']/*" />
        public LoadCase(int id) : this()
        {
            Id = id;
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCase..ctor(int,string,LoadCaseType,LoadType)']/*" />
        public LoadCase(int id, string title, LoadCaseType caseType = LoadCaseType.PrimaryLoad, LoadType loadType = LoadType.None) : this(id)
        {
            Title = title;
            CaseType = caseType;
            Type = loadType;
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCase..ctor(string,LoadType)']/*" />
        public LoadCase(string title, LoadType loadType = LoadType.Dead) : this()
        {
            Title = title;
            Type = loadType;
            CaseType = LoadCaseType.PrimaryLoad;
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCase.P:Id']/*" />
        [JsonProperty(Order = 1)]
        public int Id { get => Get<int>(); set => Set(value); }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCase.P:Title']/*" />
        [JsonProperty(Order = 2)]
        public string Title { get => Get<string>(); set => Set(value); }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCase.P:Type']/*" />
        [JsonProperty(Order = 3)]
        public LoadType Type { get => Get<LoadType>(); set => Set(value); }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCase.P:CaseType']/*" />
        [JsonProperty(Order = 4)]
        public LoadCaseType CaseType { get => Get<LoadCaseType>(); set => Set(value); }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCase.Equals`0(object)']/*" />
        public override bool Equals(object obj)
        {
            return obj is LoadCase other && Id == other.Id && CaseType == other.CaseType && Type == other.Type;
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCase.GetHashCode`0()']/*" />
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
