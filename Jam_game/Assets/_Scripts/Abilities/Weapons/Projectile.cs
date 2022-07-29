using Abilities.Base;
using Abilities.Data;
using Entities.Base;
using Tools;
using UnityEngine;

namespace Abilities.Weapons
{
	[RequireComponent(typeof(Rigidbody))]
	public class Projectile : MonoBehaviour
	{
		public Ability source;
		public ProjectileData data;

		private Rigidbody _rb;

		private void Start()
		{
			_rb = GetComponent<Rigidbody>();
			
			_rb.useGravity = false;
			_rb.constraints = RigidbodyConstraints.FreezePositionY;
			_rb.freezeRotation = true;
			
			if (!data.move.frozen)
				_rb.velocity = (data.move.moveDirection * data.move.moveSpeed).PlaneToWorld();
			if (!data.rotate.frozen)
				_rb.angularVelocity = data.rotate.rotateDirection * data.rotate.rotationSpeed;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.layer != source.targetMask.Value) return;
			
			Entity entity = other.GetComponent<Entity>();
			if (entity == null) return;
			
			entity.RegisterImpact(data.impact, data.move.moveDirection);
			DespawnProjectile();
		}

		private void DespawnProjectile()
		{
			Destroy(this);
		}
	}
}