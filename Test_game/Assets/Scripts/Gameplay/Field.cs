using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay
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
        // settings
        [SerializeField] private FieldType type;
        [AssetsOnly]
        [SerializeField] private Entity startingEntity;
        
        public FieldType Type => type;
        
        // info
        private Vector2Int _coordinates;
        private Entity _currentEntity;


        [ShowInInspector]
        public Vector2Int Coordinates => _coordinates;
        [ShowInInspector]
        public Entity CurrentEntity => _currentEntity;

        public void SpawnStartingEntity() // On board generation
        {
            if (startingEntity == null) return;
            _currentEntity = Instantiate(startingEntity, transform);
            _currentEntity.SetParentField(this);
        }
        public void SetCoordinates(Vector2Int coordinates)
        {
            _coordinates = coordinates;
            transform.position = _coordinates.CoordToPosition();
            if (_currentEntity != null) _currentEntity.RefreshCoordinates();
        }

        public void LoseCurrentEntity()
        {
            _currentEntity = null;
        }
        public void GiveCurrentEntity(Field target)
        {
            if (target == this) throw new UnityException("Field cannot give entity to itself...");
            if (_currentEntity == null) throw new UnityException("Field has no entity...");
            target.ReceiveEntity(_currentEntity);
            _currentEntity = null;
        }
        public void ReceiveEntity(Entity entity)
        {
            if (_currentEntity != null) throw new UnityException("Field already has an entity...");
            entity.SetParentField(this);
            _currentEntity = entity;
        }
    }
}