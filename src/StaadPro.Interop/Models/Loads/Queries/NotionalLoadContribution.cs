using System;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Loads;
using StaadPro.Interop.Services.Load;

namespace StaadPro.Interop.Models.Loads.Queries
{
    /// <summary>Contains one ordered notional contribution with explicit case family and global axis.</summary>
    /// <remarks>Positive native IDs indicate primary cases and negative IDs reference cases. The family
    /// and positive number are separate; duplicate contributions and finite signed/zero factors are retained.</remarks>
    public sealed class NotionalLoadContribution
    {
        /// <summary>Creates a typed contribution for future notional assignment without native access.</summary>
        /// <param name="loadCaseId">Positive contributing case number.</param>
        /// <param name="caseType">PrimaryLoad or ReferenceLoad; supplies the family explicitly.</param>
        /// <param name="factor">Finite dimensionless signed factor, including zero.</param>
        /// <param name="direction">Defined global notional axis.</param>
        /// <exception cref="ArgumentOutOfRangeException">ID, family, factor or direction is invalid.</exception>
        public NotionalLoadContribution(int loadCaseId, LoadCaseType caseType, double factor, NotionalLoadDirection direction)
        {
            LoadCaseId = LoadContractArguments.Positive(loadCaseId, nameof(loadCaseId));
            if (caseType != LoadCaseType.PrimaryLoad && caseType != LoadCaseType.ReferenceLoad) throw new ArgumentOutOfRangeException(nameof(caseType));
            CaseType = caseType; Factor = LoadContractArguments.Finite(factor, nameof(factor));
            Direction = LoadContractArguments.Defined(direction, nameof(direction));
            NativeLoadCaseId = caseType == LoadCaseType.PrimaryLoad ? loadCaseId : -loadCaseId;
        }
        internal NotionalLoadContribution(int id, double factor, NotionalLoadDirection direction)
        { NativeLoadCaseId = id; LoadCaseId = System.Math.Abs(id); CaseType = id > 0 ? LoadCaseType.PrimaryLoad : LoadCaseType.ReferenceLoad;
            Factor = factor; Direction = direction; }
        /// <summary>Gets the positive case number without its native family sign.</summary>
        public int LoadCaseId { get; }
        /// <summary>Gets PrimaryLoad or ReferenceLoad as identified by the native sign.</summary>
        public LoadCaseType CaseType { get; }
        /// <summary>Gets the original signed native case number.</summary>
        public int NativeLoadCaseId { get; }
        /// <summary>Gets the finite dimensionless contribution factor.</summary>
        public double Factor { get; }
        /// <summary>Gets the global X/Y/Z axis, without borrowing local load-direction codes.</summary>
        public NotionalLoadDirection Direction { get; }
    }
}
