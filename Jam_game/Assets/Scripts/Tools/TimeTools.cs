using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Tools
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class TimeTools
    {
    #region math
        // floats
        public static float TimeSince(this float time)
        {
            return Time.time - time;
        }
        public static float TimeUntill(this float time)
        {
            return time - Time.time;
        }
        // doubles
        public static double TimeSince(this double time)
        {
            return Time.timeAsDouble - time;
        }
        public static double TimeUntill(this double time)
        {
            return time - Time.timeAsDouble;
        }
    #endregion
    #region bools
        // floats
        public static bool HasPassed(this float time)
        {
            return Time.time >= time;
        }
        public static bool HasNotPassed(this float time)
        {
            return Time.time < time;
        }
        // doubles
        public static bool HasPassed(this double time)
        {
            return Time.timeAsDouble >= time;
        }
        public static bool HasNotPassed(this double time)
        {
            return Time.timeAsDouble < time;
        }
    #endregion
    }
}