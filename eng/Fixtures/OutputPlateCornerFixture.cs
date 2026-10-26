using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
namespace PackageVerification
{
    public static class OutputPlateCornerFixture
    {
        public sealed class Root
        {
            public object Output { get; set; } = new Native();
            public object Geometry { get; set; } = new Geometry();
            public object Load { get; set; } = new Load();
            public int Major = 26;
            public object GetApplicationVersion(ref object major, ref object update, ref object revision, ref object build)
            {
                major = Major;
                update = 0;
                revision = 0;
                build = 340;
                return Major + ".0.0.340";
            }
        }
        public sealed class Native
        {
            public readonly List<object[]> Calls = new List<object[]>();
            public readonly List<int> Threads = new List<int>();
            public readonly Queue<object> Responses = new Queue<object>();
            public object Status = true;
            public bool WriteOutputs = true;
            public int WriteCount = 6;
            public Func<Array, object> ReplaceArray;
            public Action OnCall;
            public object GetPlateCornerForces(int plate, int node, int loadCase, ref object values)
            {
                Calls.Add(new object[] { plate, node, loadCase });
                Threads.Add(Thread.CurrentThread.ManagedThreadId);
                OnCall?.Invoke();
                if (!(values is double[] a) || a.Length != 6 || a.Any(v => !double.IsNaN(v)))
                    throw new InvalidOperationException("Expected six unwritten doubles.");
                if (WriteOutputs)
                {
                    if (ReplaceArray != null)
                        values = ReplaceArray(a);
                    else
                        for (int i = 0; i < Math.Min(6, WriteCount); i++)
                            a[i] = i + 1.25;
                }
                return Responses.Count > 0 ? Responses.Dequeue() : Status;
            }
        }
        public sealed class Geometry
        {
            public object IncidenceStatus = 0;
            public bool WriteIncidence = true, WriteCoordinates = true;
            public int FourthNode = 4;
            public int IncidenceCalls, CoordinateCalls;
            public Action OnCall;
            public object GetPlateIncidence(int plate, ref object a, ref object b, ref object c, ref object d)
            {
                IncidenceCalls++;
                OnCall?.Invoke();
                if (WriteIncidence)
                {
                    a = 1;
                    b = 2;
                    c = 3;
                    d = FourthNode;
                }
                return IncidenceStatus;
            }
            public void GetNodeCoordinates(int node, ref object x, ref object y, ref object z)
            {
                CoordinateCalls++;
                OnCall?.Invoke();
                if (WriteCoordinates)
                {
                    x = node == 2 || node == 3 ? 2.0 : 0.0;
                    y = node == 3 || node == 4 ? 2.0 : 0.0;
                    z = 0.0;
                }
            }
        }
        public sealed class Load
        {
            public int[] PrimaryIds = { 1 }, ReferenceIds = new int[0], CombinationIds = new int[0];
            public readonly List<string> Calls = new List<string>();
            public Action OnCall;
            public string Title = "Dead";
            private object Count(string operation, int[] ids)
            {
                Calls.Add(operation);
                OnCall?.Invoke();
                return ids.Length;
            }
            private object Numbers(string operation, int[] ids, ref object values, bool flag)
            {
                Calls.Add(operation);
                OnCall?.Invoke();
                Array.Copy(ids, (int[])values, ids.Length);
                return flag ? (object)true : ids.Length;
            }
            public object GetPrimaryLoadCaseCount() => Count(nameof(GetPrimaryLoadCaseCount), PrimaryIds);
            public object GetPrimaryLoadCaseNumbers(ref object values) => Numbers(nameof(GetPrimaryLoadCaseNumbers), PrimaryIds, ref values, true);
            public object GetReferenceLoadCaseCount() => Count(nameof(GetReferenceLoadCaseCount), ReferenceIds);
            public object GetReferenceLoadCaseNumbers(ref object values) => Numbers(nameof(GetReferenceLoadCaseNumbers), ReferenceIds, ref values, false);
            public object GetLoadCombinationCaseCount() => Count(nameof(GetLoadCombinationCaseCount), CombinationIds);
            public object GetLoadCombinationCaseNumbers(ref object values) => Numbers(nameof(GetLoadCombinationCaseNumbers), CombinationIds, ref values, true);
            public object GetLoadCaseTitle(int id)
            {
                Calls.Add(nameof(GetLoadCaseTitle));
                return Title;
            }
            public object GetReferenceLoadCaseTitle(int id)
            {
                Calls.Add(nameof(GetReferenceLoadCaseTitle));
                return Title;
            }
            public object GetLoadType(int id)
            {
                Calls.Add(nameof(GetLoadType));
                return 0;
            }
            public object GetReferenceLoadType(int id)
            {
                Calls.Add(nameof(GetReferenceLoadType));
                return 0;
            }
        }
    }
}
