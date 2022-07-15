using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public partial class Board : MonoBehaviour
    {
        // game/board settings
        [SerializeField] private Vector2Int size = new Vector2Int(8, 8);
        [SerializeField] private List<Field> allFields = new List<Field>();
        [SerializeField] private List<Entity> allEntities = new List<Entity>();
        [SerializeField] public float tileSpawnSpacing = 1f;
        [SerializeField] public float tileSpawnHeight = 0.25f;
        [SerializeField] private Transform fieldsParent;
        
        [SerializeField] private float hillFieldRate = 0.0f;
        [SerializeField] private float voidFieldRate = 0.0f; 

        // data
        private Field[,] _fields; // fields on the board
        private Entity _playerEntity; // direct reference to the entity that represent the player

        // public data
        public static Board Instance;

        private void Awake() => Instance = this;

        private void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        private void Start()
        {
            if (fieldsParent == null) fieldsParent = transform;

            // Spawn fields
            _fields = new Field[size.x, size.y];
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    Field field = _fields[x, y] = SpawnRandomField();

                    // TODO: make it choose a specific type other than flat?
                    // TODO: Give field a specific entity?
                    field.SpawnStartingEntity();
                    field.SetCoordinates(new Vector2Int(x, y));
                }
            }

            // Spawn a player on tile (0, 0)
            _playerEntity = SpawnPlayer();
        }


        public void TryClickField(Field target)
        {
            // Check the interaction space is valid/relevant
            Vector2Int distances = (_playerEntity.Coordinates - target.Coordinates).Abs();
            if (distances.x > 1 || distances.y > 1) return; // Only allow target field if in player range
            if (distances.x == 0 && distances.y == 0) // player is already on target field
            {
                // todo: some self-interaction? This requires that move keys would be able to do the same thing 
            }
            else // target field is around player
            {
                PlayerInteract(target);
            }
        }

        public void TryMove(Vector2Int move)
        {
            Vector2Int targetCoords = _playerEntity.FieldParent.Coordinates + move;
            targetCoords.x = Mathf.Clamp(targetCoords.x, 0, size.x - 1);
            targetCoords.y = Mathf.Clamp(targetCoords.y, 0, size.y - 1);
            Field targetField = GetFieldAtCoordinates(targetCoords);
            if (targetField == null) throw new UnityException("Tried to move player to invalid coordinates!");

            PlayerInteract(targetField);
        }

        private void PlayerInteract(Field target)
        {
            if (target.CurrentEntity == null) // target has no entity, can receive player
            {
                _playerEntity.FieldParent.GiveCurrentEntity(target);
            }
            else if (target.CurrentEntity.Type == EntityTypes.Monster)
            {
                target.CurrentEntity.TakeDamage();
            }
            CheckAfterTurn();
        }

        private Field GetFieldAtCoordinates(Vector2Int coordinates)
        {
            return _fields[coordinates.x, coordinates.y];
        }

        private void CheckAfterTurn() // enemy hit player, etc.
        {
            List<Entity> monsters = allEntities.Where(entity => entity.Type == EntityTypes.Monster).ToList();
            foreach (Entity monster in monsters)
            {
                Vector2Int distances = (monster.Coordinates - _playerEntity.Coordinates).Abs();
                if (distances.x > 1 || distances.y > 1) continue;
                _playerEntity.TakeDamage();
            }
        }
    }
}
