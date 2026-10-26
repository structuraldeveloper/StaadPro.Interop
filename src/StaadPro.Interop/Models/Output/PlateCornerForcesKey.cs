using System;
using StaadPro.Interop.Enums;
namespace StaadPro.Interop.Models.Output
{
    /// <summary>Identifies a plate-corner observation independently of mutable entity references.</summary>
    /// <remarks>Adapted from the main-project key. Case family is included to prevent a reference case and primary case with the same number from colliding. The default struct value is invalid and is rejected by query methods.</remarks>
    public readonly struct PlateCornerForcesKey : IEquatable<PlateCornerForcesKey>
    {
        /// <summary>Creates an automatic-family key from positive native identifiers.</summary>
        /// <param name="loadCaseId">Positive case number.</param><param name="plateId">Positive plate ID.</param><param name="cornerNodeId">Positive incidence-node ID.</param>
        /// <exception cref="ArgumentOutOfRangeException">An identifier is not positive.</exception>
        public PlateCornerForcesKey(int loadCaseId, int plateId, int cornerNodeId) : this(loadCaseId, plateId, cornerNodeId, null) { }
        /// <summary>Creates a key with an explicit metadata case family.</summary>
        /// <param name="loadCaseId">Positive case number.</param><param name="plateId">Positive plate ID.</param><param name="cornerNodeId">Positive incidence-node ID.</param><param name="loadCaseType">Primary, reference or combination metadata family, or null for automatic unambiguous resolution; repeat metadata is not independently resolved here.</param>
        /// <exception cref="ArgumentOutOfRangeException">An identifier or family is invalid.</exception>
        public PlateCornerForcesKey(int loadCaseId, int plateId, int cornerNodeId, LoadCaseType? loadCaseType)
        {
            if (loadCaseId <= 0)
                throw new ArgumentOutOfRangeException(nameof(loadCaseId), "A positive case number is required.");
            if (plateId <= 0)
                throw new ArgumentOutOfRangeException(nameof(plateId), "A positive plate ID is required.");
            if (cornerNodeId <= 0)
                throw new ArgumentOutOfRangeException(nameof(cornerNodeId), "A positive incidence-node ID is required.");
            if (loadCaseType.HasValue && loadCaseType != StaadPro.Interop.Enums.LoadCaseType.PrimaryLoad && loadCaseType != StaadPro.Interop.Enums.LoadCaseType.ReferenceLoad && loadCaseType != StaadPro.Interop.Enums.LoadCaseType.LoadCombination)
                throw new ArgumentOutOfRangeException(nameof(loadCaseType));
            LoadCaseId = loadCaseId;
            PlateId = plateId;
            CornerNodeId = cornerNodeId;
            LoadCaseType = loadCaseType;
        }
        /// <summary>Gets the positive result case number.</summary>
        public int LoadCaseId
        {
            get;
        }
        /// <summary>Gets the positive plate ID.</summary>
        public int PlateId
        {
            get;
        }
        /// <summary>Gets the positive incidence-node ID.</summary>
        public int CornerNodeId
        {
            get;
        }
        /// <summary>Gets the metadata family, or null for an automatic/unresolved key; native Output still accepts only the numeric case selector.</summary>
        public LoadCaseType? LoadCaseType
        {
            get;
        }
        /// <summary>Compares all four captured identity fields.</summary><param name="other">Key to compare.</param><returns>True for equal IDs and family.</returns>
        public bool Equals(PlateCornerForcesKey other) => LoadCaseId == other.LoadCaseId && PlateId == other.PlateId && CornerNodeId == other.CornerNodeId && LoadCaseType == other.LoadCaseType;
        /// <summary>Compares another boxed key.</summary><param name="obj">Candidate key.</param><returns>True for an equal key.</returns>
        public override bool Equals(object obj) => obj is PlateCornerForcesKey key && Equals(key);
        /// <summary>Computes a hash from the captured identity.</summary><returns>A stable hash within this implementation.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (((LoadCaseId * 397) ^ PlateId) * 397 ^ CornerNodeId) * 397 ^ (int)(LoadCaseType ?? 0);
            }
        }
        /// <summary>Compares two key values.</summary><param name="left">First key.</param><param name="right">Second key.</param><returns>True when identities match.</returns>
        public static bool operator ==(PlateCornerForcesKey left, PlateCornerForcesKey right) => left.Equals(right);
        /// <summary>Compares two key values for inequality.</summary><param name="left">First key.</param><param name="right">Second key.</param><returns>True when identities differ.</returns>
        public static bool operator !=(PlateCornerForcesKey left, PlateCornerForcesKey right) => !left.Equals(right);
        /// <summary>Formats the captured case/plate/node/family identity.</summary><returns>Diagnostic text without native lookup.</returns>
        public override string ToString() => (LoadCaseType?.ToString() ?? "Automatic") + ":" + LoadCaseId + "/" + PlateId + "/" + CornerNodeId;
    }
}
