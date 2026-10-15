using System;
using System.Collections.Generic;
using StaadPro.Interop.Models;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Services.Root.Metadata;
using StaadPro.Interop.Services.Root.Capabilities;

namespace StaadPro.Interop.Tests.Adapter
{
    public static class RootOptionalTestDoubles
    {
        public delegate int MessageResponder(string path, ref object buffer);
        public delegate int StatusResponder(string path, ref int warnings, ref int errors, ref double cpu);
        public class NativeRoot
        {
            public readonly List<string> Calls = new List<string>();
            public int ErrorCount = 2, WarningCount = 1, AnalysisCode = 2, ExportCode;
            public int Axis, LoadHandling;
            public string VersionText = "26.0.0.340";
            public int VersionReads;
            public Exception Failure;
            public MessageResponder Errors, Warnings;
            public StatusResponder Status;
            public int GetAnalysisStatus(string path, ref int warnings, ref int errors, ref double cpu)
            {
                Calls.Add("status:" + path);
                if (Failure != null) throw Failure;
                if (Status != null) return Status(path, ref warnings, ref errors, ref cpu);
                warnings = WarningCount; errors = ErrorCount; cpu = 0;
                return AnalysisCode;
            }
            public int GetAnalysisErrorMessages(string path, ref object buffer)
            {
                Calls.Add("errors:" + path);
                if (Failure != null) throw Failure;
                if (Errors != null) return Errors(path, ref buffer);
                Fill(buffer, "error");
                return 1;
            }
            public int GetAnalysisWarningMessages(string path, ref object buffer)
            {
                Calls.Add("warnings:" + path);
                if (Failure != null) throw Failure;
                if (Warnings != null) return Warnings(path, ref buffer);
                Fill(buffer, "warning");
                return 1;
            }
            private static void Fill(object buffer, string prefix)
            {
                var array = (string[])buffer;
                for (int i = 0; i < array.Length; i++) array[i] = prefix + i;
            }
            public int ExportModelToADINA(int axis, int loadHandling)
            {
                Calls.Add("export"); Axis = axis; LoadHandling = loadHandling;
                if (Failure != null) throw Failure;
                return ExportCode;
            }
            public string GetApplicationVersion(ref object major, ref object update, ref object revision, ref object build)
            {
                VersionReads++;
                var version = ApplicationVersion.Parse(VersionText);
                major = version.Major; update = version.Update; revision = version.Revision; build = version.Build;
                return VersionText;
            }
        }
        public class Metadata : IRootMetadataAccess
        {
            public ApplicationVersion Version = ApplicationVersion.Parse("26.0.0.340");
            public int Reads;
            public Exception Failure;
            public ApplicationVersion GetApplicationVersion() { Reads++; if (Failure != null) throw Failure; return Version; }
            public BaseUnitSystem GetBaseUnit() => BaseUnitSystem.Metric;
            public ForceInputUnit GetInputUnitForForce() => ForceInputUnit.KiloNewton;
            public LengthInputUnit GetInputUnitForLength() => LengthInputUnit.Meter;
            public IntPtr GetMainWindowHandle() => IntPtr.Zero;
        }
        internal class Inspector : IRootMemberInspector
        {
            internal readonly List<string> Calls = new List<string>();
            internal readonly Dictionary<string, RootMemberEvidence> Results = new Dictionary<string, RootMemberEvidence>();
            internal Exception Failure;
            public RootMemberEvidence Inspect(object root, RootNativeContract contract)
            {
                Calls.Add(contract.Name);
                if (Failure != null) throw Failure;
                return Results.TryGetValue(contract.Name, out var result) ? result : RootMemberEvidence.Compatible;
            }
        }
    }
}
