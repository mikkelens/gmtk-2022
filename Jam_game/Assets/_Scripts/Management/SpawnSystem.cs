using System.Collections.Generic;
using System.Linq;
using Entities;
using Level;
using UnityEngine;

namespace Management
{
	public static class SpawnSystem
	{
		private static System.Random _random;
		private static System.Random MyRandom
		{
			get
			{
				if (_random != null) return _random;
				return _random = new System.Random();
			}
		}
		public static EntityData SelectEntityAsset(this List<EntityData> allEnemies)
		{
			if (allEnemies.Count == 0) return null;
            
			// count up spawn chances as a range, then generate a number within the range. Enemy with lowest number but above generated number will be chosen.
			float relativeSum = allEnemies.Sum(enemy => enemy.relativeSpawnChance);
			float random = (float)MyRandom.NextDouble();
			float scaledRandom = random * relativeSum;
			float last = 0;
			foreach (EntityData entity in allEnemies)
			{
				last += entity.relativeSpawnChance;
				if (last >= scaledRandom) return entity;
			}
			return allEnemies.First();
		}

		public static Vector2 GetRandomLocationOutsideCamBounds(float minDistance) // Location outside of viewable area
        {
        	float randomValue = (float)(minDistance * MyRandom.NextDouble());
            float x = minDistance, y = minDistance;
            if (MyRandom.NextDouble() > 0.5f)
	            x = randomValue;
            else
	            y = randomValue;
            int xSign = MyRandom.NextDouble() > 0.5f ? 1 : -1;
            int ySign = MyRandom.NextDouble() > 0.5f ? 1 : -1;
        	Vector2 offset = new Vector2(x * xSign, y * ySign);
            return CameraController.Instance.PositionNoOffset + offset;
        }
		public static Quaternion GetRandomRotation() // Rotation around the z axis
        {
        	return Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        }
	}
}