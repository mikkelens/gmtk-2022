using Abilities.Data;
using Entities.Base;
using Tools;
using UnityEngine;

namespace Abilities.Attacks
{
	[RequireComponent(typeof(Rigidbody))]
	public class Projectile : MonoBehaviour
	{
		public Ability sourceAbility;
		
		public ProjectileData Data { private get; set; }

		private Rigidbody _rb;
		private int _entityHits;

		private void Start()
		{
			_rb = GetComponent<Rigidbody>();
			
			_rb.useGravity = false;
			_rb.constraints = RigidbodyConstraints.FreezePositionY;
			_rb.freezeRotation = true;
			
			if (!Data.move.frozen)
				_rb.velocity = (Data.move.moveDirection * Data.move.moveSpeed).PlaneToWorld();
			if (!Data.rotate.frozen)
				_rb.angularVelocity = Data.rotate.rotateDirection * Data.rotate.rotationSpeed;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.layer != sourceAbility.targetMask.Value) return;
			
			Entity entity = other.GetComponent<Entity>();
			if (entity == null) return;
			
			RegisterHit(entity);
		}

		private void RegisterHit(Entity entity)
		{
			_entityHits++;
			sourceAbility.AddDataToSource(entity.RegisterImpact(Data.impact, Data.move.moveDirection));
			if (!Data.maxEntityHitAmount.Enabled || _entityHits < Data.maxEntityHitAmount.Value) return;
			DespawnProjectile();
		}

		private void DespawnProjectile()
		{
			Destroy(this);
		}
	}
}