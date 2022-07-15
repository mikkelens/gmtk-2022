using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay
{
    public enum EntityTypes
    {
        Obstacle,
        Monster,
        Player,
    }

    // In-engine gameobject component
    public class Entity : MonoBehaviour
    {
        // settings
        [SerializeField] private EntityTypes type;
        [SerializeField] private int health;
        
        public EntityTypes Type => type;
        
        // info
        private Vector2Int _coordinates;
        private Field _fieldParent;

        [ShowInInspector]
        public Vector2Int Coordinates => _coordinates;
        [ShowInInspector]
        public Field FieldParent => _fieldParent;

        public void TakeDamage()
        {
            health--;
            if (health <= 0)
            {
                _fieldParent.LoseCurrentEntity();
                
                if (type == EntityTypes.Monster) Destroy(gameObject); // todo: graphical death
            }
        }

        // This should be set by the parentfield, since they should control items logically
        public void SetParentField(Field field)
        {
            _fieldParent = field;
            transform.parent = _fieldParent.transform;
            RefreshCoordinates();
        }

        public void RefreshCoordinates()
        {
            _coordinates = _fieldParent.Coordinates;
            transform.position = _coordinates.CoordToPosition();
        }
    }
}