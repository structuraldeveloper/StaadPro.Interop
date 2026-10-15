using System;
using System.Collections.Generic;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models.Root;
using StaadPro.Interop.Services.Interop;
namespace StaadPro.Interop.Services.Root.Analysis
{
    internal static class RootMemberForcesService
    {
        internal const int MaximumQueryCount = 100000;
        internal static MemberForcesResult Retrieve(object root, IEnumerable<int> beamIds, IEnumerable<int> loadCaseIds)
        {
            int[] beams = Snapshot(beamIds, nameof(beamIds));
            int[] cases = Snapshot(loadCaseIds, nameof(loadCaseIds));
            long queryCount = 2L * beams.Length * cases.Length;
            if (queryCount > MaximumQueryCount)
                throw new ArgumentException("At most 100,000 member-end queries may be requested in one batch.", nameof(beamIds));
            if (queryCount == 0) return new MemberForcesResult(null, new MemberForceQueryResult[0]);
            object output = ((dynamic)root).Output;
            if (output == null) throw new InvalidOperationException("The bound root supplies no Output object.");
            object availability = ((dynamic)output).AreResultsAvailable();
            if (!NativeValues.ReadBoolean(availability, "result availability"))
                return new MemberForcesResult(false, new MemberForceQueryResult[0]);
            object load = ((dynamic)root).Load;
            if (load == null) throw new InvalidOperationException("The bound root supplies no Load object.");
            var result = new List<MemberForceQueryResult>((int)queryCount);
            foreach (int loadCaseId in cases)
            {
                object nativeTitle = ((dynamic)load).GetLoadCaseTitle(loadCaseId);
                string title = NativeValues.ReadText(nativeTitle, "load-case title");
                foreach (int beamId in beams)
                for (int end = 0; end <= 1; end++)
                {
                    object buffer = new[] { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN };
                    object returned = ((dynamic)output).GetMemberEndForces(beamId, end, loadCaseId, ref buffer, 0);
                    bool success = NativeValues.ReadBoolean(returned, "member-end force flag");
                    MemberForces forces = success ? ReadForces(buffer, beamId, loadCaseId, title, (MemberEnd)end) : null;
                    result.Add(new MemberForceQueryResult(beamId, loadCaseId, title, (MemberEnd)end, success, forces));
                }
            }
            return new MemberForcesResult(true, result.ToArray());
        }
        private static int[] Snapshot(IEnumerable<int> values, string name)
        {
            if (values == null) throw new ArgumentNullException(name);
            var result = new List<int>();
            var seen = new HashSet<int>();
            foreach (int value in values)
            {
                if (value <= 0) throw new ArgumentException("Identifiers must be positive.", name);
                if (seen.Add(value))
                {
                    if (result.Count >= MaximumQueryCount)
                        throw new ArgumentException("The input exceeds the managed query budget.", name);
                    result.Add(value);
                }
            }
            return result.ToArray();
        }
        private static MemberForces ReadForces(object buffer, int beam, int loadCase, string title, MemberEnd end)
        {
            if (!(buffer is Array array) || array.Rank != 1 || array.Length != 6 || array.GetType().GetElementType() != typeof(double))
                throw new InvalidOperationException("OpenSTAAD returned a successful end-force response without six Double values.");
            var values = new double[6];
            int lower = array.GetLowerBound(0);
            for (int i = 0; i < 6; i++)
            {
                double value = (double)array.GetValue(lower + i);
                if (double.IsNaN(value) || double.IsInfinity(value))
                    throw new InvalidOperationException("OpenSTAAD returned unwritten or nonfinite end forces.");
                values[i] = value;
            }
            return new MemberForces { BeamId = beam, LoadCaseId = loadCase, LoadCaseTitle = title, End = end,
                Fx = values[0], Fy = values[1], Fz = values[2], Mx = values[3], My = values[4], Mz = values[5] };
        }
    }
}
