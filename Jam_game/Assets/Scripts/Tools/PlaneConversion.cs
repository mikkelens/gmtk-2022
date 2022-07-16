using UnityEngine;

namespace Tools
{
    public static class PlaneConversion
    {

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
            return Camera.main!.ScreenToViewportPoint(screenPos) - Vector3.one * 0.5f;
        }
    #endregion
    }
}
