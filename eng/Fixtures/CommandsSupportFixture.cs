using System;
using System.Collections.Generic;
using System.Threading;

namespace PackageVerification
{
    public static class CommandsSupportFixture
    {
        public sealed class Root
        {
            public object Command { get; set; }
            public object Support { get; set; }

            public int Major = 26;
            public object GetApplicationVersion(ref object major, ref object update, ref object revision, ref object build)
            {
                major = Major;
                update = 0;
                revision = 0;
                build = 340;
                return Major + ".00.00.340";
            }
        }

        public sealed class Native
        {
            public readonly List<string> Calls = new List<string>();
            public readonly List<object[]> Arguments = new List<object[]>();
            public readonly List<int> Threads = new List<int>();
            public readonly Dictionary<string, Queue<object>> Returns = new Dictionary<string, Queue<object>>();
            public Action<string> OnCall;
            private object R(string op, object fallback, params object[] arguments)
            {
                Calls.Add(op);
                Arguments.Add(arguments);
                Threads.Add(Thread.CurrentThread.ManagedThreadId);
                OnCall?.Invoke(op);
                return Returns.TryGetValue(op, out var q) && q.Count > 0 ? q.Dequeue() : fallback;
            }

            public void PerformAnalysis(object p0)
            {
                R("PerformAnalysis", null, p0);
            }

            public void PerformPDeltaAnalysisNoConverge(object p0, object p1)
            {
                R("PerformPDeltaAnalysisNoConverge", null, p0, p1);
            }

            public void PerformPDeltaAnalysisEx(object p0, object p1, object p2, object p3)
            {
                R("PerformPDeltaAnalysisEx", null, p0, p1, p2, p3);
            }

            public void PerformCableAnalysis(object p0, object p1)
            {
                R("PerformCableAnalysis", null, p0, p1);
            }

            public object PerformCableAnalysisEx(object p0, object p1, object p2, object p3) => R("PerformCableAnalysisEx", true, p0, p1, p2, p3);
            public void PerformBucklingAnalysis(object p0, object p1)
            {
                R("PerformBucklingAnalysis", null, p0, p1);
            }

            public object PerformBucklingAnalysisEx(object p0, object p1, object p2) => R("PerformBucklingAnalysisEx", true, p0, p1, p2);
            public object PerformDirectAnalysis(object p0, object p1, object p2, object p3) => R("PerformDirectAnalysis", true, p0, p1, p2, p3);
            public object PerformNonlinearAnalysisEx(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7) => R("PerformNonlinearAnalysisEx", true, p0, p1, p2, p3, p4, p5, p6, p7);
            public object SetFloorDiaphragmBaseCommand(object p0) => R("SetFloorDiaphragmBaseCommand", true, p0);
            public object DeleteFloorDiaphragmBaseCommand() => R("DeleteFloorDiaphragmBaseCommand", true);
            public object SetCheckSoftStoryCommand(object p0) => R("SetCheckSoftStoryCommand", true, p0);
            public object DeleteCheckSoftStoryCommand() => R("DeleteCheckSoftStoryCommand", true);
            public object SetCheckIrregularitiesCommand(object p0) => R("SetCheckIrregularitiesCommand", true, p0);
            public object DeleteCheckIrregularitiesCommand() => R("DeleteCheckIrregularitiesCommand", true);
            public object DeleteAllAnalysisCommands() => R("DeleteAllAnalysisCommands", true);
            public object CreateElasticMat(object p0, object p1, object p2, object p3) => R("CreateElasticMat", 73, p0, p1, p2, p3);
            public object CreateElasticFooting(object p0, object p1, object p2, object p3) => R("CreateElasticFooting", 73, p0, p1, p2, p3);
            public object RemoveElasticMat(object p0) => R("RemoveElasticMat", true, p0);
            public object RemoveElasticMatFromNode(object p0) => R("RemoveElasticMatFromNode", true, p0);
            public object RemovePlateMat(object p0) => R("RemovePlateMat", true, p0);
            public object RemoveElasticFooting(object p0) => R("RemoveElasticFooting", true, p0);
            public object RemoveElasticFootingFromNode(object p0) => R("RemoveElasticFootingFromNode", true, p0);
            public object AssignSupportToEntityList(object p0, object p1) => R("AssignSupportToEntityList", true, p0, p1);
        }
    }
}
