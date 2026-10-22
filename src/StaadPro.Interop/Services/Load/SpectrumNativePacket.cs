using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Models.Loads.Seismic;

namespace StaadPro.Interop.Services.Load
{
    internal sealed class SpectrumNativePacket
    {
        internal int Code, Combination;
        internal string[] PrimaryNames, GenerationNames;
        internal double[] PrimaryValues, GenerationValues, CustomPairs;
        internal float[] DirectionFactors, Periods, Ordinates;
        internal float Scale, Damping, MissingMassFactor, ZpaFactor, Ff1Factor, Ff2Factor;
        internal int Quantity, DampingType, Interpolation, MissingMass, Zpa, Ff1, Ff2, Save;

        internal static SpectrumNativePacket Extended(int code, int combination, string[] primaryNames, double[] primaryValues,
            string[] generationNames, double[] generationValues, double[] customPairs)
        {
            LoadArguments.Defined((ResponseSpectrumCode)code, nameof(code));
            LoadArguments.Defined((ModalCombinationMethod)combination, nameof(combination));
            if (primaryNames == null) throw new ArgumentNullException(nameof(primaryNames));
            if (primaryValues == null) throw new ArgumentNullException(nameof(primaryValues));
            if (primaryNames.Length == 0) throw new ArgumentException("At least one primary parameter is required.", nameof(primaryNames));
            var packet = new SpectrumNativePacket
            {
                Code = code, Combination = combination,
                PrimaryNames = (string[])primaryNames.Clone(), PrimaryValues = (double[])primaryValues.Clone(),
                GenerationNames = generationNames == null ? null : (string[])generationNames.Clone(),
                GenerationValues = generationValues == null ? null : (double[])generationValues.Clone(),
                CustomPairs = customPairs == null ? null : (double[])customPairs.Clone()
            };
            ValidatePairs(packet.PrimaryNames, packet.PrimaryValues, nameof(primaryNames));
            if ((packet.GenerationNames == null) != (packet.GenerationValues == null))
                throw new ArgumentException("Generation names and values must both be supplied or both be null.");
            if (packet.GenerationNames != null) ValidatePairs(packet.GenerationNames, packet.GenerationValues, nameof(generationNames));
            if (packet.CustomPairs != null)
            {
                if (packet.CustomPairs.Length % 2 != 0) throw new ArgumentException("Custom spectrum data requires period/value pairs.", nameof(customPairs));
                for (int i = 0; i < packet.CustomPairs.Length; i++)
                {
                    LoadArguments.Finite(packet.CustomPairs[i], nameof(customPairs));
                    if (i % 2 == 0 && packet.CustomPairs[i] < 0) throw new ArgumentOutOfRangeException(nameof(customPairs), "Periods cannot be negative.");
                }
            }
            if (packet.GenerationNames?.Length > 0 && packet.CustomPairs?.Length > 0)
                throw new ArgumentException("Generated spectrum settings and custom points are mutually exclusive.");
            if (packet.GenerationNames?.Length == 0) { packet.GenerationNames = null; packet.GenerationValues = null; }
            if (packet.CustomPairs?.Length == 0) packet.CustomPairs = null;
            return packet;
        }

        internal static SpectrumNativePacket Extended(ResponseSpectrumLoadExParameters request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.PrimaryParameters == null) throw new ArgumentNullException(nameof(request.PrimaryParameters));
            var primary = request.PrimaryParameters.ToArray();
            var generation = request.SpectrumGenerationParameters?.ToArray();
            var points = request.CustomSpectrumData?.ToArray();
            return Extended((int)request.Code, (int)request.CombinationMethod, primary.Select(p => p.Key).ToArray(),
                primary.Select(p => p.Value).ToArray(), generation?.Select(p => p.Key).ToArray(),
                generation?.Select(p => p.Value).ToArray(), Flatten(points));
        }

        private static double[] Flatten(SpectrumCurvePoint[] points)
        {
            if (points == null) return null;
            var result = new double[checked(points.Length * 2)];
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i] == null) throw new ArgumentException("Spectrum points cannot contain null.", nameof(points));
                result[2 * i] = points[i].Period; result[2 * i + 1] = points[i].Value;
            }
            return result;
        }

        private static void ValidatePairs(string[] names, double[] values, string argument)
        {
            if (names.Length != values.Length) throw new ArgumentException("Parameter names and values must have equal lengths.", argument);
            var seen = new HashSet<string>(StringComparer.Ordinal);
            for (int i = 0; i < names.Length; i++)
            {
                LoadArguments.Keyword(names[i], argument);
                if (!seen.Add(names[i])) throw new ArgumentException("Duplicate parameter keyword.", argument);
                LoadArguments.Finite(values[i], argument);
            }
        }

        internal static SpectrumNativePacket Legacy(ResponseSpectrumLoadParameters request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            LoadArguments.Defined(request.LoadType, nameof(request.LoadType));
            LoadArguments.Defined(request.QuantityType, nameof(request.QuantityType));
            LoadArguments.Defined(request.DampingType, nameof(request.DampingType));
            LoadArguments.Defined(request.InterpolationType, nameof(request.InterpolationType));
            if (request.DirectionFactors == null) throw new ArgumentNullException(nameof(request.DirectionFactors));
            double[] factors = (double[])request.DirectionFactors.Clone();
            if (factors.Length != 3) throw new ArgumentException("Exactly three directional factors are required.", nameof(request.DirectionFactors));
            if (request.SpectrumPoints == null) throw new ArgumentNullException(nameof(request.SpectrumPoints));
            var points = request.SpectrumPoints.ToArray();
            if (points.Length == 0) throw new ArgumentException("At least one spectrum point is required.", nameof(request.SpectrumPoints));
            var packet = new SpectrumNativePacket
            {
                Code = (int)request.LoadType, DirectionFactors = factors.Select(v => Single(v, nameof(request.DirectionFactors))).ToArray(),
                Quantity = (int)request.QuantityType, Scale = Single(request.Scale, nameof(request.Scale)),
                DampingType = (int)request.DampingType, Damping = Single(request.DampingFactor, nameof(request.DampingFactor)),
                Interpolation = (int)request.InterpolationType, MissingMass = request.ConsiderMissingMass ? 1 : 0,
                MissingMassFactor = Single(request.MissingMassFactor, nameof(request.MissingMassFactor)),
                Zpa = request.ConsiderZPA ? 1 : 0, ZpaFactor = Single(request.ZPAFactor, nameof(request.ZPAFactor)),
                Ff1 = request.ConsiderFf1 ? 1 : 0, Ff1Factor = Single(request.Ff1Factor, nameof(request.Ff1Factor)),
                Ff2 = request.ConsiderFf2 ? 1 : 0, Ff2Factor = Single(request.Ff2Factor, nameof(request.Ff2Factor)),
                Save = request.SaveFlag ? 1 : 0, Periods = new float[points.Length], Ordinates = new float[points.Length]
            };
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i] == null) throw new ArgumentException("Spectrum points cannot contain null.", nameof(request.SpectrumPoints));
                if (points[i].Period < 0) throw new ArgumentOutOfRangeException(nameof(request.SpectrumPoints), "Periods cannot be negative.");
                packet.Periods[i] = Single(points[i].Period, nameof(request.SpectrumPoints));
                packet.Ordinates[i] = Single(points[i].Value, nameof(request.SpectrumPoints));
            }
            return packet;
        }

        private static float Single(double value, string argument)
        {
            LoadArguments.Finite(value, argument);
            float result = (float)value;
            if (float.IsInfinity(result) || (value != 0 && result == 0))
                throw new ArgumentOutOfRangeException(argument, "A finite nonzero value cannot overflow or underflow Single precision.");
            return result;
        }
    }
}
