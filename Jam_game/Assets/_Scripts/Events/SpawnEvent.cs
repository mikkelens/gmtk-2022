using System;
using System.Collections;
using Entities.Base;
using Level;
using Management;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Events
{
	[Serializable]
	public class SpawnEvent : GameEvent
	{
		[PropertyOrder(10)]
		public Optional<Pickup> pickupToSpawnOnEnd;
		[PropertyOrder(10)]
		public bool requireAllKilledToContinue = true;

		public Transform SpawningParent { get; set; }
		protected float MinSpawnDistance;
		
		public void SetMinSpawnDistance(float minSpawnDistance) => MinSpawnDistance = minSpawnDistance;

		private Vector2? _pickupSpawnLocation;
		protected Vector2 PickupSpawnLocation
		{
			get
			{
				if (_pickupSpawnLocation != null) return _pickupSpawnLocation.Value;
				return SpawnSystem.GetRandomLocationOutsideCamBounds(MinSpawnDistance);
			}
			set => _pickupSpawnLocation = value;
		}

		protected virtual bool AllKilled => false;

		public override IEnumerator RunEvent()
		{
			yield return PausingPoint();
			yield return base.RunEvent();
			if (pickupToSpawnOnEnd.Enabled) SpawnPickup(pickupToSpawnOnEnd.Value);
			if (requireAllKilledToContinue) yield return new WaitUntil(() => AllKilled);
		}

		protected Entity SpawnEntity(Entity enemyPrefab, Transform enemyParent)
		{
			Vector2 pos = SpawnSystem.GetRandomLocationOutsideCamBounds(MinSpawnDistance);
			Entity spawnedEntity = Instantiate(enemyPrefab, pos.PlaneToWorld(), SpawnSystem.GetRandomRotation(), enemyParent);
			spawnedEntity.SpawnOrigin = this;
			return spawnedEntity;
		}
		public virtual void DespawnEntity(Entity entity)
		{
			Destroy(entity.gameObject);
		}

		protected void SpawnPickup(Pickup pickup)
		{
			Instantiate(pickup, PickupSpawnLocation.PlaneToWorld(), Quaternion.identity, SpawningParent);
		}
	}
}