using System;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Output;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Output;
using StaadPro.Interop.Services.Interop;

namespace StaadPro.Interop.Services.Output
{
    // Common 2024–2026 contracts only. Version-sensitive station/direction/optional
    // operations must receive their own qualified policy when they are introduced.
    internal sealed class OutputQueryReader
    {
        private readonly OpenStaadWrapper wrapper;
        private readonly object native;
        internal OutputQueryReader(OpenStaadWrapper wrapper, object native)
        { this.wrapper = wrapper; this.native = native; }

        private dynamic Binding()
        {
            if (wrapper.RawOpenStaad == null || !ReferenceEquals((object)wrapper.RawOutput, native))
                throw new InvalidOperationException("The captured Output binding is disposed or has been replaced. Reacquire wrapper.Output.");
            return native;
        }

        internal OutputQueryResult<bool> GetAnalysisResultAvailability()
        {
            const string operation = "AreResultsAvailable";
            object value = Binding().AreResultsAvailable();
            return Result(operation, ReadFlag(value, operation));
        }

        internal OutputQueryResult<bool> GetBucklingResultAvailability()
        {
            const string operation = "IsBucklingAnalysisResultsAvailable";
            object value = Binding().IsBucklingAnalysisResultsAvailable();
            return Result(operation, ReadFlag(value, operation, allowVariantTrue: false));
        }

        internal OutputQueryResult<NodeDisplacements> GetNodeDisplacements(int nodeId, int loadCaseId) =>
            ReadArray("GetNodeDisplacements", nodeId, loadCaseId, 6, v => new NodeDisplacements(v));

        internal OutputQueryResult<ForceComponents> GetSupportReactions(int nodeId, int loadCaseId) =>
            ReadArray("GetSupportReactions", nodeId, loadCaseId, 6, v => new ForceComponents(v));

        internal OutputQueryResult<ForceComponents> GetMemberEndForces(int memberId, MemberEnd memberEnd, int loadCaseId)
        {
            Positive(memberId, nameof(memberId)); Positive(loadCaseId, nameof(loadCaseId));
            if (memberEnd != MemberEnd.Start && memberEnd != MemberEnd.End)
                throw new ArgumentOutOfRangeException(nameof(memberEnd));
            const string operation = "GetMemberEndForces";
            object buffer = Buffer(6);
            object status = Binding().GetMemberEndForces(memberId, (int)memberEnd, loadCaseId, ref buffer, 0);
            return ReadFlag(status, operation)
                ? new OutputQueryResult<ForceComponents>(operation, OutputQueryStatus.Success,
                    new ForceComponents(ReadArrayValues(buffer, 6, operation)), memberId, loadCaseId, memberEnd)
                : new OutputQueryResult<ForceComponents>(operation, OutputQueryStatus.NativeFailure, null,
                    memberId, loadCaseId, memberEnd);
        }

        internal OutputQueryResult<StaticCheckResult> GetStaticCheckResult(int loadCaseId)
        {
            Positive(loadCaseId, nameof(loadCaseId));
            const string operation = "GetStaticCheckResult";
            object forces = Buffer(6), reactions = Buffer(6);
            object status = Binding().GetStaticCheckResult(loadCaseId, ref forces, ref reactions);
            return ReadFlag(status, operation)
                ? Result(operation, new StaticCheckResult(ReadArrayValues(forces, 6, operation),
                    ReadArrayValues(reactions, 6, operation)), loadCaseId: loadCaseId)
                : Failed<StaticCheckResult>(operation, loadCaseId: loadCaseId);
        }

        internal OutputQueryResult<PlateCenterStressesAndMoments> GetPlateCenterStressesAndMoments(int plateId, int loadCaseId) =>
            ReadArray("GetAllPlateCenterStressesAndMoments", plateId, loadCaseId, 8, v => new PlateCenterStressesAndMoments(v));

        internal OutputQueryResult<PlateCenterForces> GetPlateCenterForces(int plateId, int loadCaseId) =>
            ReadArray("GetAllPlateCenterForces", plateId, loadCaseId, 5, v => new PlateCenterForces(v));

        internal OutputQueryResult<PlateCenterMoments> GetPlateCenterMoments(int plateId, int loadCaseId) =>
            ReadArray("GetAllPlateCenterMoments", plateId, loadCaseId, 3, v => new PlateCenterMoments(v));

        internal OutputQueryResult<PlatePrincipalStressesAndAngles> GetPlateCenterPrincipalStressesAndAngles(int plateId, int loadCaseId) =>
            ReadArray("GetAllPlateCenterPrincipalStressesAndAngles", plateId, loadCaseId, 8,
                v => new PlatePrincipalStressesAndAngles(v), allowSinglePrecision: true);

        internal OutputQueryResult<PlatePrincipalStressesAndAngles> GetPlateCenterPrincipalStressesAndAnglesEx(int plateId, int loadCaseId)
        {
            Positive(plateId, nameof(plateId)); Positive(loadCaseId, nameof(loadCaseId));
            const string operation = "GetAllPlateCenterPrincipalStressesAndAnglesEx";
            object stresses = Buffer(6), angles = Buffer(2);
            object status = Binding().GetAllPlateCenterPrincipalStressesAndAnglesEx(plateId, loadCaseId, ref stresses, ref angles);
            if (!ReadFlag(status, operation)) return Failed<PlatePrincipalStressesAndAngles>(operation, plateId, loadCaseId);
            var values = new double[8];
            Array.Copy(ReadArrayValues(stresses, 6, operation, true), 0, values, 0, 6);
            Array.Copy(ReadArrayValues(angles, 2, operation, true), 0, values, 6, 2);
            return Result(operation, new PlatePrincipalStressesAndAngles(values), plateId, loadCaseId);
        }

        internal OutputQueryResult<PlatePrincipalStresses> GetPlateCenterNormalPrincipalStresses(int plateId, int loadCaseId)
        {
            Positive(plateId, nameof(plateId)); Positive(loadCaseId, nameof(loadCaseId));
            const string operation = "GetPlateCenterNormalPrincipalStresses";
            object topMaximum = double.NaN, topMinimum = double.NaN, bottomMaximum = double.NaN, bottomMinimum = double.NaN;
            object status = Binding().GetPlateCenterNormalPrincipalStresses(plateId, loadCaseId,
                ref topMaximum, ref topMinimum, ref bottomMaximum, ref bottomMinimum);
            return ReadFlag(status, operation)
                ? Result(operation, new PlatePrincipalStresses(new[] { ReadNumber(topMaximum, operation), ReadNumber(topMinimum, operation),
                    ReadNumber(bottomMaximum, operation), ReadNumber(bottomMinimum, operation) }), plateId, loadCaseId)
                : Failed<PlatePrincipalStresses>(operation, plateId, loadCaseId);
        }

        internal OutputQueryResult<PlateCenterVonMises> GetPlateCenterVonMisesStresses(int plateId, int loadCaseId)
        {
            Positive(plateId, nameof(plateId)); Positive(loadCaseId, nameof(loadCaseId));
            const string operation = "GetPlateCenterVonMisesStresses";
            object top = double.NaN, bottom = double.NaN;
            object status = Binding().GetPlateCenterVonMisesStresses(plateId, loadCaseId, ref top, ref bottom);
            return ReadFlag(status, operation)
                ? Result(operation, new PlateCenterVonMises(ReadNumber(top, operation), ReadNumber(bottom, operation)), plateId, loadCaseId)
                : Failed<PlateCenterVonMises>(operation, plateId, loadCaseId);
        }

        internal OutputQueryResult<string> GetMemberDesignSectionName(int memberId)
        {
            Positive(memberId, nameof(memberId));
            const string operation = "GetMemberDesignSectionName";
            object value = Binding().GetMemberDesignSectionName(memberId);
            string section = NativeValues.ReadText(value, "design section name");
            return section.Length == 0
                ? new OutputQueryResult<string>(operation, OutputQueryStatus.NotFound, null, memberId)
                : Result(operation, section, memberId);
        }

        internal OutputQueryResult<double> GetMemberSteelUtilizationRatio(int memberId)
        {
            Positive(memberId, nameof(memberId));
            const string operation = "GetMemberSteelDesignRatio";
            object ratio = double.NaN;
            object status = Binding().GetMemberSteelDesignRatio(memberId, ref ratio);
            if (!ReadFlag(status, operation)) return Failed<double>(operation, memberId);
            double value = ReadNumber(ratio, operation);
            if (value == -999) return new OutputQueryResult<double>(operation, OutputQueryStatus.MemberNotDesigned, default, memberId);
            if (value == -1) return new OutputQueryResult<double>(operation, OutputQueryStatus.AnalysisNotPerformed, default, memberId);
            if (value < 0) throw new InvalidOperationException("OpenSTAAD returned an undocumented negative steel utilization ratio.");
            return Result(operation, value, memberId);
        }

        private OutputQueryResult<T> ReadArray<T>(string operation, int entityId, int loadCaseId, int count,
            Func<double[], T> decode, bool allowSinglePrecision = false)
        {
            Positive(entityId, operation == "GetNodeDisplacements" || operation == "GetSupportReactions" ? "nodeId" : "plateId");
            Positive(loadCaseId, nameof(loadCaseId));
            object values = Buffer(count);
            dynamic binding = Binding();
            object status;
            switch (operation)
            {
                case "GetNodeDisplacements": status = binding.GetNodeDisplacements(entityId, loadCaseId, ref values); break;
                case "GetSupportReactions": status = binding.GetSupportReactions(entityId, loadCaseId, ref values); break;
                case "GetAllPlateCenterStressesAndMoments": status = binding.GetAllPlateCenterStressesAndMoments(entityId, loadCaseId, ref values); break;
                case "GetAllPlateCenterForces": status = binding.GetAllPlateCenterForces(entityId, loadCaseId, ref values); break;
                case "GetAllPlateCenterMoments": status = binding.GetAllPlateCenterMoments(entityId, loadCaseId, ref values); break;
                case "GetAllPlateCenterPrincipalStressesAndAngles": status = binding.GetAllPlateCenterPrincipalStressesAndAngles(entityId, loadCaseId, ref values); break;
                default: throw new InvalidOperationException("Unknown Output array contract.");
            }
            return ReadFlag(status, operation)
                ? Result(operation, decode(ReadArrayValues(values, count, operation, allowSinglePrecision)), entityId, loadCaseId)
                : Failed<T>(operation, entityId, loadCaseId);
        }

        private static object Buffer(int count)
        {
            var doubles = new double[count];
            for (int i = 0; i < count; i++) doubles[i] = double.NaN;
            return doubles;
        }

        private static double[] ReadArrayValues(object value, int count, string operation, bool allowSingle = false)
        {
            if (!(value is Array array) || array.Rank != 1 || array.Length != count ||
                (array.GetType().GetElementType() != typeof(double) && !(allowSingle && array.GetType().GetElementType() == typeof(float))))
                throw new InvalidOperationException(operation + " returned an invalid result array shape or element type.");
            var values = new double[count];
            int lower = array.GetLowerBound(0);
            for (int i = 0; i < count; i++) values[i] = ReadNumber(array.GetValue(lower + i), operation);
            return values;
        }

        private static double ReadNumber(object value, string operation)
        {
            double number;
            if (value is double d) number = d;
            else if (value is float f) number = f;
            else throw new InvalidOperationException(operation + " returned a non-floating-point result.");
            if (double.IsNaN(number) || double.IsInfinity(number))
                throw new InvalidOperationException(operation + " returned unwritten or nonfinite output.");
            return number;
        }

        private static bool ReadFlag(object value, string operation, bool allowVariantTrue = true)
        {
            if (value is bool flag) return flag;
            long code = NativeValues.ReadInt64(value, operation + " status");
            if (code == 0) return false;
            if (code == 1 || (allowVariantTrue && code == -1)) return true;
            throw new InvalidOperationException(operation + " returned an undocumented status.");
        }

        private static void Positive(int value, string name)
        { if (value <= 0) throw new ArgumentOutOfRangeException(name, "A positive native identifier is required."); }
        private static OutputQueryResult<T> Result<T>(string operation, T data, int? entityId = null, int? loadCaseId = null) =>
            new OutputQueryResult<T>(operation, OutputQueryStatus.Success, data, entityId, loadCaseId);
        private static OutputQueryResult<T> Failed<T>(string operation, int? entityId = null, int? loadCaseId = null) =>
            new OutputQueryResult<T>(operation, OutputQueryStatus.NativeFailure, default, entityId, loadCaseId);
    }
}
