using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Services.Load;

namespace StaadPro.Interop.Models.Loads.Seismic
{
    /// <summary>
    /// Encapsulates parameters for STAAD Direct Analysis Definition stiffness reduction commands (AISC 360 / Chapter C).
    /// </summary>
    public class DirectAnalysisParameters
    {
        /// <summary>
        /// Direct Analysis parameter type: <see cref="DirectAnalysisParameterType.Flex"/> or <see cref="DirectAnalysisParameterType.Axial"/>.
        /// </summary>
        public DirectAnalysisParameterType ParameterType { get; set; } = DirectAnalysisParameterType.Flex;

        /// <summary>
        /// Flexural reduction factor value (e.g. 0.6 to 1.0; 0 for Axial as it is not applicable).
        /// </summary>
        public double ParameterValue { get; set; } = 0.8;

        /// <summary>
        /// Collection of member IDs to assign the stiffness reduction parameter to.
        /// </summary>
        public List<int> MemberIds { get; set; } = new List<int>();

        /// <summary>Creates an editable FLEX request with factor 0.8 and an empty member list.</summary>
        public DirectAnalysisParameters()
        {
        }

        /// <summary>Copies the supplied selection into an editable direct-analysis request.</summary>
        /// <param name="type">Documented Flex or Axial classification, checked on validation/submission.</param>
        /// <param name="value">FLEX scalar. The AXIAL constructor preserves source behavior by storing zero instead of this unused value.</param>
        /// <param name="memberIds">Optional IDs to copy once; null leaves an empty request that cannot be submitted until populated.</param>
        public DirectAnalysisParameters(DirectAnalysisParameterType type, double value, IEnumerable<int> memberIds)
        {
            ParameterType = type;
            ParameterValue = (type == DirectAnalysisParameterType.Axial) ? 0.0 : value;
            if (memberIds != null)
            {
                MemberIds.AddRange(memberIds);
            }
        }

        /// <summary>
        /// Creates a Direct Analysis FLEX parameter with specified flexural reduction factor.
        /// </summary>
        /// <param name="flexFactor">Finite FLEX parameter required on submission.</param>
        /// <param name="memberIds">Selection copied once; IDs must be positive and the typed request nonempty on submission.</param>
        /// <returns>A new editable request whose validity is checked by Validate or the adapter.</returns>
        public static DirectAnalysisParameters CreateFlex(double flexFactor, IEnumerable<int> memberIds)
        {
            return new DirectAnalysisParameters(DirectAnalysisParameterType.Flex, flexFactor, memberIds);
        }

        /// <summary>
        /// Creates a Direct Analysis AXIAL parameter for specified members.
        /// </summary>
        /// <param name="memberIds">Selection copied once; IDs must be positive and the typed request nonempty on submission.</param>
        /// <returns>A new AXIAL request with an exact zero unused scalar.</returns>
        public static DirectAnalysisParameters CreateAxial(IEnumerable<int> memberIds)
        {
            return new DirectAnalysisParameters(DirectAnalysisParameterType.Axial, 0.0, memberIds);
        }

        /// <summary>
        /// Validates parameters.
        /// </summary>
        /// <remarks>Validates a request without issuing native commands. Keep mutable inputs stable during validation and dispatch.</remarks>
        /// <exception cref="ArgumentException">A required field, classification, numeric value or input packet is invalid.</exception>
        public void Validate()
        {
            LoadArguments.Defined(ParameterType, nameof(ParameterType));
            LoadArguments.Finite(ParameterValue, nameof(ParameterValue));
            if (ParameterType == DirectAnalysisParameterType.Axial && ParameterValue != 0) throw new ArgumentException("AXIAL requires a zero unused scalar.", nameof(ParameterValue));
            if (MemberIds == null) throw new ArgumentNullException(nameof(MemberIds));
            if (MemberIds.Count == 0) throw new ArgumentException("At least one member ID is required.", nameof(MemberIds));
            if (MemberIds.Any(id => id <= 0)) throw new ArgumentOutOfRangeException(nameof(MemberIds));
        }
    }
}
