using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Board
{
    public enum FieldType
    {
        Flat,
        Hill,
        Void,
    }
    
    // In-engine gameobject component
    public class Field : MonoBehaviour
    {
        // own details
        private Vector2Int _coordinates;
        private FieldType _type;
        
        [ShowInInspector]
        public Vector2Int Coordinates => _coordinates;
        [ShowInInspector]
        public FieldType Type => _type;

        // exterior details
        [AssetsOnly]
        public Item item;

        public void SetCoordinates(Vector2Int coordinates)
        {
            _coordinates = coordinates;
            transform.position = _coordinates.CoordToPosition();
        }
    }
}