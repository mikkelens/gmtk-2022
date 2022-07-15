using UnityEngine;

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
}
