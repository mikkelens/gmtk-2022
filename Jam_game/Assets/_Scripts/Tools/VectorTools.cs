using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Tools
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class VectorTools
    {
    #region Component functions
        // vector3
        public static Vector3 WithX(this Vector3 vector3, float value)
        {
            vector3.x = value;
            return vector3;
        }
        public static Vector3 WithY(this Vector3 vector3, float value)
        {
            vector3.y = value;
            return vector3;
        }
        public static Vector3 WithZ(this Vector3 vector3, float value)
        {
            vector3.z = value;
            return vector3;
        }
        // vector2
        public static Vector2 WithX(this Vector2 vector2, float value)
        {
            vector2.x = value;
            return vector2;
        }
        public static Vector2 WithY(this Vector2 vector2, float value)
        {
            vector2.y = value;
            return vector2;
        }
    #endregion
        
    #region 3D/2D conversions
        public static Vector3 PlaneToWorld(this Vector2 vector2) => new Vector3(vector2.x, 0, vector2.y);
        public static Vector3 PlaneToWorldBox(this Vector2 vector2, float height = 1f) => new Vector3(vector2.x, height, vector2.y); // converts plane to box (with custom height)
        public static Vector3 PlaneToWorldOffset(this Vector2 vector2, float offset = 0.5f) => new Vector3(vector2.x, offset, vector2.y); // for showing gizmos etc more visibly
        public static Vector2 WorldToPlane(this Vector3 vector3) => new Vector2(vector3.x, vector3.z);
        public static Vector3 WorldToWorldBox(this Vector3 vector3, float height = 1f) => vector3.WithY(height);
        public static Vector3 WorldToWorldOffset(this Vector3 vector3, float offset = 0.5f) => vector3.WithY(offset);
    #endregion
    }
}
