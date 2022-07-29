using System.Diagnostics.CodeAnalysis;
using UnityEditor;

namespace Tools.Editor
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class EditorTimeTools
    {
    #region floats
        // math
        public static float TimeSinceEditor(this float time)
        {
            return (float)EditorApplication.timeSinceStartup - time;
        }
        public static float TimeUntillEditor(this float time)
        {
            return time - (float)EditorApplication.timeSinceStartup;
        }
        // bools
        public static bool HasPassedEditor(this float time)
        {
            return EditorApplication.timeSinceStartup >= time;
        }
        public static bool HasNotPassedEditor(this float time)
        {
            return EditorApplication.timeSinceStartup < time;
        }
    #endregion
    #region doubles
        // math
        public static double TimeSinceEditor(this double time)
        {
            return EditorApplication.timeSinceStartup - time;
        }
        public static double TimeUntillEditor(this double time)
        {
            return time - EditorApplication.timeSinceStartup;
        }
        // bools
        public static bool HasPassedEditor(this double time)
        {
            return EditorApplication.timeSinceStartup >= time;
        }
        public static bool HasNotPassedEditor(this double time)
        {
            return EditorApplication.timeSinceStartup < time;
        }
    #endregion
    }
}