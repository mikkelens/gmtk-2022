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
		public static Vector2 GetRandomLocationOutsideCamBounds(float bonusDistance) // Location outside of viewable area
		{
            Vector2 maxBounds = CameraTools.VisibleGroundFrustum;
            maxBounds += Vector2.one * bonusDistance;
            // offsetRange changes x or y, meaning that entities can spawn just outside camera in a random position
            if (RandomTools.NextBool())
	            maxBounds.x *= RandomTools.NextFloat();
            else
	            maxBounds.y *= RandomTools.NextFloat();
            Vector2 offsets = new Vector2(maxBounds.x * RandomTools.NextIntSign(), maxBounds.y * RandomTools.NextIntSign());
			// Debug.Log($"Random offsets: {offsets.ToString()}");
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