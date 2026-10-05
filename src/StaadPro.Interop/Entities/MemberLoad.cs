using System.Collections.Generic;
using Newtonsoft.Json;
using StaadPro.Interop.Attributes;
using StaadPro.Interop.Common.Units;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Entities
{
    /// <summary>
    /// Abstract base class for all member (beam) load items in STAAD.Pro.
    /// </summary>
    public abstract class MemberLoad : LoadItem
    {
        #region Constructors

        protected MemberLoad() : base(LoadItemType.MemberLoad)
        {
            Direction = LoadDirection.GlobalY;
            Members = new HashSet<Beam>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the direction in which the load is applied (local or global).
        /// </summary>
        [JsonProperty]
        [LoadDefiningProperty]
        public LoadDirection Direction { get; set; }

        /// <summary>
        /// Gets the collection of beams to which this load is applied.
        /// </summary>
        [JsonIgnore]
        public HashSet<Beam> Members { get; private set; }

        #endregion

        #region Abstract Methods

        public abstract MemberLoad DeepCopy();

        public abstract void ConvertUsing(UnitsConverter uc);

        public abstract void ApplyTo(dynamic load, int[] beamIds);

        #endregion
    }
}
