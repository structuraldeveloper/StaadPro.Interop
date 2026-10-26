using System;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Support;
namespace StaadPro.Interop.Models.Support
{
    /// <summary>Contains a named elastic-footing definition read-back without computing foundation response.</summary>
    public sealed class ElasticFootingObservation
    {
        internal ElasticFootingObservation(int value0, double value1, double value2, FoundationSupportDirection value3, double value4, int value5)
        { SupportId=value0; Length=value1; Width=value2; Direction=value3; SubgradeModulus=value4; AssignedNodeCount=value5; }
        /// <summary>Gets the requested positive support definition ID.</summary>
        public int SupportId { get; }
        /// <summary>Gets the finite positive native footing length.</summary>
        public double Length { get; }
        /// <summary>Gets the finite positive native footing width.</summary>
        public double Width { get; }
        /// <summary>Gets the native direction 0..5 without axis synthesis.</summary>
        public FoundationSupportDirection Direction { get; }
        /// <summary>Gets the finite nonnegative native subgrade modulus in force/length cubed.</summary>
        public double SubgradeModulus { get; }
        /// <summary>Gets the nonnegative observed assigned-node count.</summary>
        public int AssignedNodeCount { get; }
    }
}
