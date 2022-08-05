using System;
using System.Collections;
using Components;
using Entities.Base;
using Management;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Events
{
	[Serializable]
	public abstract class SpawnEvent : GameEvent
	{
		[PropertyOrder(10)]
		public Optional<PickupComponent> pickupToSpawnOnEnd;

		public Transform SpawningParent { get; set; }
		public float ExtraSpawnDistance { get; set; }

		private Vector2? _pickupSpawnLocation;
		protected Vector2 PickupSpawnLocation
		{
			get
			{
				if (_pickupSpawnLocation != null) return _pickupSpawnLocation.Value;
				return SpawnSystem.GetRandomLocationOutsideCamBounds(ExtraSpawnDistance);
			}
			set => _pickupSpawnLocation = value;
		}

		public override IEnumerator RunEvent()
		{
			yield return Manager.StartCoroutine(base.RunEvent());
			if (pickupToSpawnOnEnd.Enabled) SpawnPickup(pickupToSpawnOnEnd.Value);
		}

		protected Entity SpawnEntity(Entity enemyPrefab, Transform enemyParent)
		{
			Vector2 pos = SpawnSystem.GetRandomLocationOutsideCamBounds(ExtraSpawnDistance);
			Entity spawnedEntity = Instantiate(enemyPrefab, pos.PlaneToWorld(), SpawnSystem.GetRandomRotation(), enemyParent);
			spawnedEntity.SpawnOrigin = this;
			return spawnedEntity;
		}
		public virtual void DespawnEntity(Entity entity)
		{
			Destroy(entity.gameObject);
		}

		protected void SpawnPickup(PickupComponent pickupComponent)
		{
			Instantiate(pickupComponent, PickupSpawnLocation.PlaneToWorld(), Quaternion.identity, SpawningParent);
		}
	}
}