using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public partial class Board
    {
        // player specific
        private Entity SpawnPlayer()
        {
            Entity player = SpawnEntityOfType(EntityTypes.Player);
            Field playerStartField = _fields[0, 0];
            playerStartField.LoseCurrentEntity();
            playerStartField.ReceiveEntity(player);
            return player;
        }

        // fields
        private Field SpawnRandomField()
        {
            float random = Random.value;
            FieldTypes selectedType;
            if (random < voidFieldRate)
                selectedType = FieldTypes.Void;
            else if (random < voidFieldRate + hillFieldRate)
                selectedType = FieldTypes.Hill;
            else
                selectedType = FieldTypes.Flat;
            return SpawnFieldOfType(selectedType);
        }
        private Field SpawnFieldOfType(FieldTypes desiredType)
        {
            Field chosenFieldAsset = RandomFieldAssetOfType(desiredType);

            // Spawn asset and return reference
            return Instantiate(chosenFieldAsset, fieldsParent).GetComponent<Field>();
        }
        private Field RandomFieldAssetOfType(FieldTypes type)
        {
            List<Field> validFieldAssets = allFields.Where(fieldAsset => fieldAsset.Type == type).ToList();
            if (validFieldAssets.Count == 0) throw new UnityException("No fields of type " + type + " found!");
            // Choose random field asset, return
            return validFieldAssets[Random.Range(0, validFieldAssets.Count)];
        }
        
        // entities
        private Entity SpawnEntityOfType(EntityTypes desiredType)
        {
            // Get valid field assets
            Entity chosenEntityAsset = RandomEntityOfType(desiredType);

            // Spawn asset and return reference
            return Instantiate(chosenEntityAsset).GetComponent<Entity>();
        }
        private Entity RandomEntityOfType(EntityTypes type)
        {
            List<Entity> validEntityAssets = allEntities.Where(entityAsset => entityAsset.Type == type).ToList();
            if (validEntityAssets.Count == 0) throw new UnityException("No entity of type " + type + " found!");
            // Choose random entity asset, return
            return validEntityAssets[Random.Range(0, validEntityAssets.Count)];
        }
    }
}