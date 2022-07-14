using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Board
{
    public enum ItemType
    {
        Obstacle,
        Enemy,
        Player,
    }

    // In-engine gameobject component
    public class Item : MonoBehaviour
    {
        private Vector2Int _coordinates;
        private ItemType _type;
        
        [ShowInInspector]
        public Vector2Int Coordinates => _coordinates;
        [ShowInInspector]
        public ItemType Type => _type;

        public void SetCoordinates(Vector2Int coordinates)
        {
            _coordinates = coordinates;
            transform.position = _coordinates.CoordToPosition();
        }
    }
}