using UnityEngine;

namespace Tools
{
    public static class TimeTools
    {
        // math
        public static float TimeSince(this float time)
        {
            return Time.time - time;
        }
        public static float TimeUntill(this float time)
        {
            return time - Time.time;
        }
        
        // bools
        public static bool HasPassed(this float time)
        {
            return Time.time >= time;
        }
        public static bool HasNotPassed(this float time)
        {
            return Time.time < time;
        }
    }
}