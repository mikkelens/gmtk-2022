using UnityEngine;

// ReSharper disable MemberCanBePrivate.Global
namespace Gameplay.Board
{
    public static class CoordinateHelpers
    {
        public static Vector3 CoordToPosition(this Vector2Int coordinates)
        {
            Vector2 verticalPos = coordinates * Board.Instance.TileScale;
            return verticalPos.VerticalToHorizontalPosition();
        }
        
        // "Vertical" position is on the Z plane in transform worldspace.
        public static Vector3 VerticalToHorizontalPosition(this Vector2 pos, float height = 0f)
        {
            return new Vector3(pos.x, height, pos.y);
        }
        // "Horizontal" position is on the Y plane in the transform worldspace.
        public static Vector2 HorizontalToVerticalPosition(this Vector3 pos)
        {
            return new Vector2(pos.x, pos.z);
        }
    }
}