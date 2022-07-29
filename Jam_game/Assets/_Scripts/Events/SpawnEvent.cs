using System;
using System.Collections;
using Entities.Base;
using Level;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Events
{
	[Serializable]
	public class SpawnEvent : GameEvent
	{
		public Optional<Pickup> pickupToSpawnOnEnd;
		protected Transform SpawningParent;
		protected float MinSpawnDistance;

		public void SetSpawningParent(Transform spawningParent) => SpawningParent = spawningParent;
		public void SetMinSpawnDistance(float minSpawnDistance) => MinSpawnDistance = minSpawnDistance;

		private Vector2? _pickupSpawnLocation;
		protected Vector2 PickupSpawnLocation
		{
			get
			{
				if (_pickupSpawnLocation != null) return _pickupSpawnLocation.Value;
				return GetRandomSpawnLocation();
			}
			set => _pickupSpawnLocation = value;
		}

		public override IEnumerator RunEvent()
		{
			yield return PausingPoint();
			yield return base.RunEvent();
			if (pickupToSpawnOnEnd.Enabled) SpawnPickup(pickupToSpawnOnEnd.Value);
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

		protected void SpawnPickup(Pickup pickup)
		{
			Instantiate(pickup, PickupSpawnLocation.PlaneToWorld(), Quaternion.identity, SpawningParent);
		}
	}
}