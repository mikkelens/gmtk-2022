using System.Collections;
using Abilities.Data;
using Stats.Stat.Variants;
using Tools;
using UnityEngine;

namespace Abilities.Attacks
{
	[CreateAssetMenu(fileName = "New Projectile Attack", menuName = MenuPath + "Projectile Attack")]
	public class RangedAttack : Attack
	{
		[Header("Ranged Attack")]
		public ProjectileComponent projectilePrefab;
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

		public override bool CanUse => _lastFireTime.TimeSince() >= cooldown.Value;

		protected override IEnumerator Use()
		{
			FireProjectile();
			yield break;
		}

		private void FireProjectile()
		{
			_lastFireTime = Time.time;
			ProjectileComponent projectile = Instantiate(projectilePrefab, Point.PlaneToWorld(), Quaternion.LookRotation(Direction.PlaneToWorld()));
			projectile.Data = projectileData;
		}
	}
}