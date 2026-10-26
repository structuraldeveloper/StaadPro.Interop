using System;
using System.Linq;
namespace PackageVerification
{
    // Managed automation fixture only. It qualifies dispatch and owned-data behavior, not live COM.
    public static class OutputWorkflowFixture
    {
        public sealed class Root
        {
            public object Output { get; set; } = new Native();
            public object Geometry { get; set; } = new Geometry();
            public object Property { get; set; } = new Property();
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
        public sealed class Native : OutputAdvancedFixture.Native
        {
            private void Fill(ref object values, string role)
            {
                if (!(values is double[] a) || a.Any(x => !double.IsNaN(x)))
                    throw new InvalidOperationException("Expected unwritten double buffer.");
                if (!WriteOutputs)
                    return;
                if (ReplaceArray != null)
                {
                    values = ReplaceArray(role, a);
                    return;
                }
                for (int i = 0; i < a.Length; i++)
                    a[i] = i + 1.25;
                Arrays[role] = a;
            }
            public object GetMemberSteelDesignRatio(int member, ref object ratio)
            {
                var r = R(nameof(GetMemberSteelDesignRatio), true, member);
                if (WriteOutputs)
                    ratio = S("ratio", 0.75);
                return r;
            }
            public object GetMemberEndForces(int member, int end, int loadCase, ref object values, int axes)
            {
                var r = R(nameof(GetMemberEndForces), true, member, end, loadCase, axes);
                Fill(ref values, "endForces");
                return r;
            }
            public object GetSupportReactions(int node, int loadCase, ref object values)
            {
                var r = R(nameof(GetSupportReactions), true, node, loadCase);
                Fill(ref values, "reactions");
                return r;
            }
            public object GetStaticCheckResult(int loadCase, ref object loads, ref object reactions)
            {
                var r = R(nameof(GetStaticCheckResult), true, loadCase);
                Fill(ref loads, "loads");
                Fill(ref reactions, "reactions");
                return r;
            }
            public object GetAllPlateCenterForces(int plate, int loadCase, ref object values)
            {
                var r = R(nameof(GetAllPlateCenterForces), true, plate, loadCase);
                Fill(ref values, "plateForces");
                return r;
            }
            public object GetAllPlateCenterMoments(int plate, int loadCase, ref object values)
            {
                var r = R(nameof(GetAllPlateCenterMoments), true, plate, loadCase);
                Fill(ref values, "plateMoments");
                return r;
            }
            public object GetPlateCenterVonMisesStresses(int plate, int loadCase, ref object top, ref object bottom)
            {
                var r = R(nameof(GetPlateCenterVonMisesStresses), true, plate, loadCase);
                if (WriteOutputs)
                {
                    top = S("top", 1.25);
                    bottom = S("bottom", 2.25);
                }
                return r;
            }
            public object GetPlateCenterNormalPrincipalStresses(int plate, int loadCase, ref object a, ref object b, ref object c, ref object d)
            {
                var r = R(nameof(GetPlateCenterNormalPrincipalStresses), true, plate, loadCase);
                if (WriteOutputs)
                {
                    a = 1.25;
                    b = 2.25;
                    c = 3.25;
                    d = 4.25;
                }
                return r;
            }
        }
        public sealed class Property
        {
            public object Status = 0;
            public double X = 0, Y = 2, Z = 0;
            public int Calls;
            public Action OnCall;
            public object GetMemberGlobalOffSet(int id, int end, ref object x, ref object y, ref object z)
            {
                Calls++;
                OnCall?.Invoke();
                x = X;
                y = Y;
                z = Z;
                return Status;
            }
        }
        public sealed class Geometry
        {
            public object Status = 0;
            public bool WriteOutputs = true, WriteCoordinates = true;
            public int Calls;
            public Action OnCall;
            public object GetMemberIncidence(int id, ref object start, ref object end)
            {
                Calls++;
                OnCall?.Invoke();
                if (WriteOutputs)
                {
                    start = 1;
                    end = 2;
                }
                return Status;
            }
            public void GetNodeCoordinates(int id, ref object x, ref object y, ref object z)
            {
                Calls++;
                OnCall?.Invoke();
                if (WriteOutputs && WriteCoordinates)
                {
                    x = id == 1 ? 0.0 : 2.0;
                    y = 0.0;
                    z = 0.0;
                }
            }
        }
        public sealed class Load
        {
            public int Calls;
            public Action OnCall;
            public object GetPrimaryLoadCaseCount()
            {
                Calls++;
                OnCall?.Invoke();
                return 1;
            }
            public object GetPrimaryLoadCaseNumbers(ref object values)
            {
                Calls++;
                ((int[])values)[0] = 1;
                return true;
            }
            public object GetLoadCaseTitle(int id)
            {
                Calls++;
                return "Dead";
            }
            public object GetLoadType(int id)
            {
                Calls++;
                return 0;
            }
            public object GetLoadCombinationCaseCount()
            {
                Calls++;
                return 0;
            }
        }
    }
}
