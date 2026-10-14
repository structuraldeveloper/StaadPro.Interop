using System;
using System.Diagnostics;
using System.Threading;

namespace StaadPro.Interop.Services
{
    internal interface IRootPollingClock
    {
        double ElapsedSeconds { get; }
        void Sleep(double seconds);
    }

    internal sealed class RootPollingClock : IRootPollingClock
    {
        private readonly Stopwatch timer = Stopwatch.StartNew();
        public double ElapsedSeconds => timer.Elapsed.TotalSeconds;
        public void Sleep(double seconds) => Thread.Sleep((int)Math.Max(1, Math.Min(int.MaxValue, Math.Ceiling(seconds * 1000))));
    }

    internal static class RootModelPolling
    {
        internal static void ValidateSeconds(double value, string parameterName)
        {
            if (double.IsNaN(value) || double.IsInfinity(value) || value <= 0 || value > TimeSpan.MaxValue.TotalSeconds)
                throw new ArgumentOutOfRangeException(parameterName, "A finite positive duration within TimeSpan limits is required.");
        }

        internal static bool Wait(Func<bool> matches, double timeoutSeconds, double intervalSeconds, IRootPollingClock clock)
        {
            while (clock.ElapsedSeconds < timeoutSeconds)
            {
                if (matches()) return clock.ElapsedSeconds <= timeoutSeconds;
                double remaining = timeoutSeconds - clock.ElapsedSeconds;
                if (remaining <= 0) return false;
                clock.Sleep(Math.Min(remaining, intervalSeconds));
            }
            return false;
        }
    }
}
