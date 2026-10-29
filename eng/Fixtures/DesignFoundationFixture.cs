using System;
using System.Collections.Generic;
using System.Threading;

namespace PackageVerification
{
    public static class DesignFoundationFixture
    {
        public sealed class Root
        {
            public object Design { get; set; }

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

        public sealed class Row
        {
            public object Name = "FYLD", Value = "250", Unit = "N/mm2", Description = "Yield strength", Default = 0;
        }

        public sealed class Holder
        {
            public object Count { get; set; }

            public readonly List<Row> Rows = new List<Row>();
            public Action OnRead;
            private Row At(int index)
            {
                OnRead?.Invoke();
                return Rows[index];
            }

            public object get_Name(int index) => At(index).Name;
            public object get_Value(int index) => At(index).Value;
            public object get_Unit(int index) => At(index).Unit;
            public object get_Description(int index) => At(index).Description;
            public object get_Default(int index) => At(index).Default;
        }

        public sealed class Native
        {
            public readonly List<string> Calls = new List<string>();
            public readonly List<object[]> Arguments = new List<object[]>();
            public readonly List<int> Threads = new List<int>();
            public readonly Dictionary<string, Queue<object>> Returns = new Dictionary<string, Queue<object>>();
            public Action<string> OnCall;
            public Action<Holder> Fill;
            private object Result(string function, object fallback, params object[] args)
            {
                Calls.Add(function);
                Arguments.Add(args);
                Threads.Add(Thread.CurrentThread.ManagedThreadId);
                OnCall?.Invoke(function);
                return Returns.TryGetValue(function, out var q) && q.Count > 0 ? q.Dequeue() : fallback;
            }

            public object CreateDesignBrief(int designCode) => Result("CreateDesignBrief", 7, designCode);
            public object AssignDesignCommand(int brief, string name, string value, object members) => Result("AssignDesignCommand", 0, brief, name, value, members);
            public object AssignDesignParameter(int brief, string name, string value, object members) => Result("AssignDesignParameter", 0, brief, name, value, members);
            public object GetDesignBriefCode(int brief) => Result("GetDesignBriefCode", 1067, brief);
            public object GetMemberDesignParameters(int brief, int member, object holder)
            {
                var h = (Holder)holder;
                object result = Result("GetMemberDesignParameters", 0, brief, member, holder);
                if (Fill != null)
                    Fill(h);
                else
                {
                    h.Rows.Add(new Row());
                    h.Count = 1;
                }

                return result;
            }
        }
    }
}
