using UnityEngine;

namespace Gameplay
{
    public static class CoordinateHelpers
    {
        public static (int, int) Deconstruct(this Vector2Int coordinates)
        {
            return (coordinates.x, coordinates.y);
        }
        
        public static Vector2Int Abs(this Vector2Int vector2Int)
        {
            return new Vector2Int(Mathf.Abs(vector2Int.x), Mathf.Abs(vector2Int.y));
        }
        public static Vector3 CoordToPosition(this Vector2Int coordinates)
        {
            Vector2 verticalPos = (Vector2)coordinates * Board.Instance.tileSpawnSpacing;
            return verticalPos.VerticalToHorizontalPosition();
        }
        
        // "Vertical" position is on the Z plane in transform worldspace.
        private static Vector3 VerticalToHorizontalPosition(this Vector2 pos)
        {
            return new Vector3(pos.x, Board.Instance.tileSpawnHeight, pos.y);
        }
        // "Horizontal" position is on the Y plane in the transform worldspace.
        private static Vector2 HorizontalToVerticalPosition(this Vector3 pos)
        {
            return new Vector2(pos.x, pos.z);
        }
    }
}