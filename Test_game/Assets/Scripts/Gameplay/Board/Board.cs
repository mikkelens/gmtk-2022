using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Gameplay.Board
{
    public class Board : MonoBehaviour
    {
        // configurable
        [SerializeField] private Vector2Int size = new Vector2Int(4, 4);
        [SerializeField] private List<Field> allFields = new List<Field>();
        [SerializeField] private List<Item> allItems = new List<Item>();
        
        // local instances
        private Field[,] _fields;
        
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
                    
                    field.transform.position = new Vector3(x, 0, y);
                }
            }
        }

        
        private Field SpawnField(FieldType desiredType)
        {
            // Get valid field assets
            List<Field> validFieldAssets = allFields.Where(fieldAsset => fieldAsset.type == desiredType).ToList();
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
            List<Item> validItemAssets = allItems.Where(itemAsset => itemAsset.type == desiredType).ToList();
            if (validItemAssets.Count == 0) throw new UnityException("No items of type " + desiredType + " found!");
            
            // Choose field asset
            Item chosenFieldAsset = validItemAssets[Random.Range(0, validItemAssets.Count)];

            // Spawn asset and return it
            Item item = Instantiate(chosenFieldAsset, transform).GetComponent<Item>();
            return item;
        }
    }
}
