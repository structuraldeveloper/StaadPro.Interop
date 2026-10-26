using System;
using System.Collections.Generic;
using StaadPro.Interop.Enums.Output;
namespace StaadPro.Interop.Models.Output
{
    /// <summary>Contains a positive native response-sample count and integration interval.</summary>
    public sealed class TimeHistoryIntegrationInfo
    {
        internal TimeHistoryIntegrationInfo(int count, double interval) { SampleCount=count; TimeStepSeconds=interval; LastSampleTimeSeconds=(count-1)*interval; }
        /// <summary>Gets the native number of response samples, including the first sample at time zero.</summary>
        public int SampleCount { get; }
        /// <summary>Gets the positive finite native integration interval in seconds.</summary>
        public double TimeStepSeconds { get; }
        /// <summary>Gets (SampleCount-1) times the interval; no extra endpoint sample is invented.</summary>
        public double LastSampleTimeSeconds { get; }
    }
    /// <summary>Contains one time-history sample without scaling or unit conversion.</summary>
    public sealed class TimeHistorySample
    {
        internal TimeHistorySample(double time, double value) { TimeSeconds=time; Value=value; }
        /// <summary>Gets the requested or integration-derived nonnegative sample time in seconds.</summary>
        public double TimeSeconds { get; }
        /// <summary>Gets the finite signed response; units depend on translational/rotational DOF and displacement/velocity/acceleration kind.</summary>
        public double Value { get; }
    }
    /// <summary>Contains a bounded, owned time-history response with explicit node/DOF/kind identity.</summary>
    /// <remarks>The first sample is at zero; subsequent times are derived from the checked native interval. No analysis or case activation occurs. Equal before/after integration metadata cannot prove unchanged result contents.</remarks>
    public sealed class TimeHistorySeries
    {
        internal TimeHistorySeries(int nodeId, ResponseDegreeOfFreedom dof, TimeHistoryResponseKind kind, TimeHistoryIntegrationInfo integration, TimeHistorySample[] samples)
        { NodeId=nodeId; DegreeOfFreedom=dof; ResponseKind=kind; Integration=integration; Samples=Array.AsReadOnly((TimeHistorySample[])samples.Clone()); }
        /// <summary>Gets the requested positive node ID.</summary>
        public int NodeId { get; }
        /// <summary>Gets the requested translational or rotational native degree of freedom.</summary>
        public ResponseDegreeOfFreedom DegreeOfFreedom { get; }
        /// <summary>Gets the requested displacement, velocity or acceleration kind.</summary>
        public TimeHistoryResponseKind ResponseKind { get; }
        /// <summary>Gets the checked sample-count/interval observation used for this series.</summary>
        public TimeHistoryIntegrationInfo Integration { get; }
        /// <summary>Gets an owned read-only sequence in native sample order; no interpolation or extra final sample is supplied.</summary>
        public IReadOnlyList<TimeHistorySample> Samples { get; }
    }
}
