using System;
using System.Diagnostics;

namespace Ameba.Common.Helpers
{
    public static class StopwatchUtil
    {
        public static TimeSpan Time(Action action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}
