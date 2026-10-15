using System;
namespace StaadPro.Interop.Services.Root.Capabilities
{
    internal sealed class RootNativeContract
    {
        internal RootNativeContract(string name, params Type[] parameterTypes)
        { Name = name; ParameterTypes = parameterTypes; }
        internal string Name { get; }
        internal Type[] ParameterTypes { get; }
        internal static readonly RootNativeContract Export = new RootNativeContract("ExportModelToADINA", typeof(int), typeof(int));
        internal static readonly RootNativeContract Errors = new RootNativeContract("GetAnalysisErrorMessages", typeof(string), typeof(object).MakeByRefType());
        internal static readonly RootNativeContract Warnings = new RootNativeContract("GetAnalysisWarningMessages", typeof(string), typeof(object).MakeByRefType());
        internal static readonly RootNativeContract AnalysisStatus = new RootNativeContract("GetAnalysisStatus", typeof(string), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(double).MakeByRefType());
    }
    internal enum RootMemberEvidence { Compatible, Absent, SignatureUnverified }
    internal interface IRootMemberInspector
    {
        RootMemberEvidence Inspect(object root, RootNativeContract contract);
    }
}
