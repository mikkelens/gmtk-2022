using Abilities.Base;
using Abilities.Data;
using Stats.Stat.Variants;
using Tools;
using UnityEngine;

namespace Abilities.Weapons
{
	public class ProjectileWeapon : Weapon
	{
		public FloatStat fireDelay = 0.8f;
		public Projectile projectilePrefab;
		public ProjectileData projectileData;
		public BoolStat autoFire = false;

		private float _lastFireTime;
		private Transform _projectilesParent;
		public Transform ProjectilesParent
		{
			get
			{
				if (_projectilesParent != null) return _projectilesParent;
				return _projectilesParent = Instantiate(new GameObject(), SourceEntity.SpawnOrigin.SpawningParent).transform;
			}	
		}

		protected override void Use()
		{
			if (_lastFireTime.TimeSince() < fireDelay) return;
			FireProjectile();
		}

		private void FireProjectile()
		{
			_lastFireTime = Time.time;
			Instantiate(projectilePrefab, AttackPoint.PlaneToWorld(), Quaternion.LookRotation(AttackDirection.PlaneToWorld()));
		}
	}
}