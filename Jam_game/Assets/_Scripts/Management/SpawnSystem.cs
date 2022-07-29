using System.Collections.Generic;
using System.Linq;
using Entities;
using Entities.Base;
using Entities.Players;
using UnityEngine;

namespace Management
{
	public static class SpawnSystem
	{
		public static Entity SelectEntityAsset(IReadOnlyCollection<EntityData> allEnemies)
		{
			if (allEnemies.Count == 0) return null;
            
			// count up spawn chances as a range, then generate a number within the range. Enemy with lowest number but above generated number will be chosen.
			float totalSpawnRange = allEnemies.Sum(enemy => enemy.relativeSpawnChance);
			float random = Random.Range(0, totalSpawnRange);
			float last = 0f;
			EntityData selectedEnemy = allEnemies.First(enemy =>
			{
				last += enemy.relativeSpawnChance;
				return last >= random;
			});
			return selectedEnemy.prefab;
		}

		public static Vector2 GetRandomLocationOutsideCamBounds(float minDistance) // Location outside of viewable area
        {
        	bool randomX = Random.value > 0.5f;
        	float randomValue = Random.Range(0, minDistance);
        	Vector2 offset = new Vector2(randomX ? randomValue : minDistance, randomX ? minDistance : randomValue);
            return CameraController.Instance.PositionNoOffset + offset;
        }
		public static Quaternion GetRandomRotation() // Rotation around the z axis
        {
        	return Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        }
	}
}