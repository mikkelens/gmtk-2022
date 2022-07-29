using UnityEngine;

namespace Tools
{
    // [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class PlaneConversion
    {
    #region Caching
        private static Camera _main;
        private static Camera Main
        {
            get
            {
                if (_main == null) _main = Camera.main;
                if (_main == null) Debug.LogWarning("No main camera found.");
                return _main;
            }
        }
    #endregion

    #region 3D/2D conversions
        public static Vector3 PlaneToWorld(this Vector2 vector2)
        {
            return new Vector3(vector2.x, 0, vector2.y);
        }
        public static Vector2 WorldToPlane(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.z);
        }
    #endregion

    #region Screen/Mouse conversions
        public static Vector2 ScreenToCenter(this Vector2 screenPos)
        {
            return Main.ScreenToViewportPoint(screenPos) - Vector3.one * 0.5f;
        }
        public static Vector2 PositionToScreenPoint(this Vector3 position)
        {
            return Main.WorldToScreenPoint(position);
        }
        public static Vector2 ScreenToViewportPoint(this Vector2 screenPos)
        {
            return Main.ScreenToViewportPoint(screenPos);
        }
    #endregion
    }
}
