using System.Collections.Generic;
using System.Linq;
using Entities;
using Level;
using Tools;
using UnityEngine;

namespace Management
{
	public static class SpawnSystem
	{
		public static EntityData SelectEntityAsset(this List<EntityData> allEnemies)
		{
			if (allEnemies.Count == 0) return null;
            
			// count up spawn chances as a range, then generate a number within the range.
			float relativeSum = allEnemies.Sum(enemy => enemy.relativeSpawnChance);
			// float random = (float)MyRandom.NextDouble();
			float scaledRandom = RandomTools.NextFloat() * relativeSum;
			float last = 0;
			foreach (EntityData entity in allEnemies)
			{
				last += entity.relativeSpawnChance;
				if (last >= scaledRandom) return entity; // Enemy with lowest number but above generated number will be chosen.
			}
			return allEnemies.First();
		}

		public static Vector2 GetRandomLocationOutsideCamBounds(float bonusDistance) // Location outside of viewable area
		{
            Vector2 bounds = CameraTools.VisibleGroundFrustum + Vector2.one * bonusDistance;
            // offsetRange changes x or y, meaning that entities can spawn just outside camera in a random position
            if (RandomTools.NextBool())
	            bounds.x *= RandomTools.NextFloat();
            else
	            bounds.y *= RandomTools.NextFloat();
            Vector2 offsets = new Vector2(bounds.x * RandomTools.NextIntSign(), bounds.y * RandomTools.NextIntSign());
			Vector2 groundPosOnCameraCenter = CameraTools.PositionNoOffset;
			Vector2 spawnPosition = groundPosOnCameraCenter + offsets;
            return spawnPosition;
        }
		public static Quaternion GetRandomRotation() // Rotation around the z axis
        {
        	return Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        }
	}
}