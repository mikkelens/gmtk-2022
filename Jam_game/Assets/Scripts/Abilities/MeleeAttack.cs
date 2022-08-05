using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entities.Base;
using Sirenix.OdinInspector;
using Stats.Stat.Variants;
using Tools;
using UnityEngine;

namespace Abilities
{
    [Serializable]
    public enum MeleeHitMethods
    {
        Raycast,
        Area
    }

    [CreateAssetMenu(fileName = "New Melee Attack", menuName = MenuPath + "Melee Attack")]
	public class MeleeAttack : Attack
	{
		[Header("Melee Attack")]
		public MeleeHitMethods hitMethod;

		[ShowIf("@hitMethod == MeleeHitMethods.Area")]
		public Optional<Vector2> physicsBox;

		[ShowIf("@hitMethod == MeleeHitMethods.Raycast")]
		public Optional<FloatStat> maxDistance;

		public Optional<IntStat> maxEntitiesHit;

		private float MaxDistance => maxDistance.Enabled ? maxDistance.Value : float.MaxValue; // for raycast

		protected override IEnumerator Use()
		{
			TryHitEntity();
			yield break;
		}

		private void TryHitEntity()
		{
			Collider[] colliders;
			if (hitMethod == MeleeHitMethods.Raycast)
				colliders = RaycastCheck();
			else
				colliders = AreaCheck();
			if (colliders.Length == 0) return;

			List<Entity> entities = colliders.Select(collider => collider.GetComponent<Entity>()).Where(entity => entity != null).ToList();
			if (entities.Count == 0) return;
			Debug.Log($"Weapon {name} hit something.");

			ImpactEntities(hitImpact, entities, new Optional<int>(maxEntitiesHit.Value, maxEntitiesHit.Enabled));
		}

	#region Check methods

		private Collider[] AreaCheck()
		{
			return Physics.OverlapBox(Point.PlaneToWorld(), Vector3.one, Quaternion.identity, TargetMask);
		}

		private Collider[] RaycastCheck()
		{
			Ray ray = new Ray(Point.PlaneToWorld(), Direction.PlaneToWorld());
			return Physics.RaycastAll(ray, MaxDistance, TargetMask).Select(hit => hit.collider).ToArray();
		}

	#endregion
	}
}