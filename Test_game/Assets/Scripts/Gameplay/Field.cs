using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay
{
    public enum FieldTypes
    {
        Flat,
        Hill,
        Void,
    }
    
    // In-engine gameobject component
    public class Field : MonoBehaviour
    {
        // settings
        [SerializeField] private float highlightModifier = 1.5f;
        [SerializeField] private FieldTypes type;
        [AssetsOnly]
        [SerializeField] private Entity startingEntity;
        
        public FieldTypes Type => type;
        
        // info
        private Vector2Int _coordinates;
        private Entity _currentEntity;

        private Transform _meshTransform;
        private bool _highlighted;
        private Vector3 _normalSize;

        [ShowInInspector]
        public Vector2Int Coordinates => _coordinates;
        [ShowInInspector]
        public Entity CurrentEntity => _currentEntity;

        private void Awake()
        {
            _meshTransform = GetComponentInChildren<MeshRenderer>().transform;
            _normalSize = _meshTransform.localScale;
        }

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

        public void HighlightField(bool highlight) // On mouse hover
        {
            if (_highlighted == highlight) return;
            _meshTransform.localScale = highlight ? _normalSize * highlightModifier : _normalSize;
            _highlighted = highlight;
        }
    }
}