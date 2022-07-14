using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Board
{
    public class Board : MonoBehaviour
    {
        
        // configurable
        [SerializeField] private Vector2Int size = new Vector2Int(4, 4);
        [SerializeField] private List<Field> allFields = new List<Field>();
        [SerializeField] private List<Item> allItems = new List<Item>();
        
        // data
        private Item _player;
        private Field[,] _fields;
        private Vector2 _tileScale;
        
        // public data
        public static Board Instance;
        public Vector2 TileScale => _tileScale;

        private void Awake() => Instance = this;
        private void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        private void Start()
        {
            _fields = new Field[size.x, size.y];
            
            // for all fields we want
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    // Spawn a field...
                    Field field = _fields[x, y] = SpawnField(FieldType.Flat);
                    // TODO: make it choose a specific type other than flat?
                    // TODO: Give field a specific item?

                    field.SetCoordinates(new Vector2Int(x, y));
                }
            }
            
            // Spawn a player on tile (0, 0)
            _player = SpawnPlayer();
        }

        private Item SpawnPlayer()
        {
            Item player = SpawnItem(ItemType.Player);
            player.SetCoordinates(Vector2Int.zero);
            return player;
        }

    #region Position and coordinates
        private Vector2 CoordinatesToPosition(Vector2Int coordinate)
        {
            return coordinate * _tileScale;
        }
        private Vector2Int PositionToCoordinate(Vector2 position)
        {
            return Vector2Int.RoundToInt(position / _tileScale); // fucked and cringe
        }
    #endregion
        
        private Field SpawnField(FieldType desiredType)
        {
            // Get valid field assets
            List<Field> validFieldAssets = allFields.Where(fieldAsset => fieldAsset.Type == desiredType).ToList();
            if (validFieldAssets.Count == 0) throw new UnityException("No fields of type " + desiredType + " found!");
            
            // Choose field asset
            Field chosenFieldAsset = validFieldAssets[Random.Range(0, validFieldAssets.Count)];

            // Spawn asset and return it
            Field field = Instantiate(chosenFieldAsset, transform).GetComponent<Field>();
            return field;
        }
        
        private Item SpawnItem(ItemType desiredType)
        {
            // Get valid field assets
            List<Item> validItemAssets = allItems.Where(itemAsset => itemAsset.Type == desiredType).ToList();
            if (validItemAssets.Count == 0) throw new UnityException("No items of type " + desiredType + " found!");
            
            // Choose field asset
            Item chosenFieldAsset = validItemAssets[Random.Range(0, validItemAssets.Count)];

            // Spawn asset and return it
            Item item = Instantiate(chosenFieldAsset, transform).GetComponent<Item>();
            return item;
        }
    }
}
