using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public partial class Board
    {
        private Entity SpawnPlayer()
        {
            Entity player = SpawnItem(EntityType.Player);
            Field playerStartField = _fields[0, 0];
            playerStartField.LoseCurrentEntity();
            playerStartField.ReceiveEntity(player);
            return player;
        }

        private Field SpawnField(FieldType desiredType)
        {
            // Get valid field assets
            List<Field> validFieldAssets = allFields.Where(fieldAsset => fieldAsset.Type == desiredType).ToList();
            if (validFieldAssets.Count == 0) throw new UnityException("No fields of type " + desiredType + " found!");

            // Choose field asset
            Field chosenFieldAsset = validFieldAssets[Random.Range(0, validFieldAssets.Count)];

            // Spawn asset and return it
            return Instantiate(chosenFieldAsset, fieldsParent).GetComponent<Field>();
        }

        private Entity SpawnItem(EntityType desiredType)
        {
            // Get valid field assets
            List<Entity> validItemAssets = allEntities.Where(itemAsset => itemAsset.Type == desiredType).ToList();
            if (validItemAssets.Count == 0) throw new UnityException("No entity of type " + desiredType + " found!");

            // Choose (random) field asset
            Entity chosenFieldAsset = validItemAssets[Random.Range(0, validItemAssets.Count)];

            // Spawn asset and return it
            return Instantiate(chosenFieldAsset).GetComponent<Entity>();;
        }
    }
}