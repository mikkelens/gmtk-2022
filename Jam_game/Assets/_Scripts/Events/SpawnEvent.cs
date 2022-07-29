using System.Collections;
using Entities.Base;
using Level;
using Tools;
using UnityEngine;

namespace Events
{
	public class SpawnEvent : ExpandableScriptableObject
	{
		protected float StartTime;
		protected Transform SpawningParent;
		protected float MinSpawnDistance;
		
		public void SetSpawningParent(Transform spawningParent) => SpawningParent = spawningParent;
		public void SetMinSpawnDistance(float minSpawnDistance) => MinSpawnDistance = minSpawnDistance;

		public virtual IEnumerator RunEvent() // base as setup
		{
			Debug.Log($"Started spawn event: {name}");
			StartTime = Time.time;
			yield break;
		}

		protected Entity SpawnEntity(Entity enemyPrefab, Transform enemyParent)
		{
			Entity spawnedEntity = Instantiate(enemyPrefab, GetRandomSpawnLocation().PlaneToWorld(), GetRandomSpawnRotation(), enemyParent);
			spawnedEntity.SetSpawnOrigin(this);
			return spawnedEntity;
		}
		public virtual void DespawnEntity(Entity entity)
		{
			Destroy(entity.gameObject);
		}
		
		protected Vector2 GetRandomSpawnLocation() // Location outside of viewable area
		{
			bool randomX = Random.value > 0.5f;
			float randomValue = Random.Range(0, MinSpawnDistance);
			return new Vector2(randomX ? randomValue : MinSpawnDistance, randomX ? MinSpawnDistance : randomValue);
		}

		protected Quaternion GetRandomSpawnRotation() // Rotation around the z axis
		{
			return Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
		}

		protected void SpawnPickup(Pickup pickup, Vector2 location)
		{
			Instantiate(pickup, location.PlaneToWorld(), Quaternion.identity, SpawningParent);
		}
	}
}