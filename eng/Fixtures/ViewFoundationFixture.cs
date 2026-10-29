using System;
using System.Collections.Generic;
using System.Threading;

namespace PackageVerification
{
    public static class ViewFoundationFixture
    {
        public sealed class Root
        {
            public object View { get; set; }
            public int Major = 26;
            public object GetApplicationVersion(ref object major, ref object update, ref object revision, ref object build)
            { major = Major; update = 0; revision = 0; build = 340; return Major + ".00.00.340"; }
        }
        public sealed class Native
        {
            public readonly List<string> Calls = new List<string>();
            public readonly List<int> Threads = new List<int>();
            public Action OnCall;
            public void ShowFront()
            { Calls.Add("ShowFront"); Threads.Add(Thread.CurrentThread.ManagedThreadId); OnCall?.Invoke(); }
        }
    }
}
