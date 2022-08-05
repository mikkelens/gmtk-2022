using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Abilities.Data;
using Entities.Base;
using Sirenix.OdinInspector;
using Stats.Stat.Variants;
using Tools;
using UnityEngine;

namespace Abilities.Attacks
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
		public Optional<ImpactData> impact;
		public MeleeHitMethods hitMethod;

		[ShowIf("@hitMethod == MeleeHitMethods.Area")]
		public Optional<Vector2> physicsBox;

		[ShowIf("@hitMethod == MeleeHitMethods.Raycast")]
		public Optional<FloatStat> maxDistance;

		public float MaxDistance => maxDistance.Enabled ? maxDistance.Value : float.MaxValue; // for raycast

		protected override IEnumerator Use()
		{
			TryHitEntity();
			yield break;
		}

		private bool TryHitEntity()
		{
			Collider[] colliders;
			if (hitMethod == MeleeHitMethods.Raycast)
				colliders = RaycastCheck();
			else
				colliders = AreaCheck();
			if (colliders.Length == 0) return false;

			List<Entity> entities = colliders.Select(collider => collider.GetComponent<Entity>()).Where(entity => entity != null).ToList();
			if (entities.Count == 0) return false;
			Debug.Log($"Weapon {name} hit something.");

			ImpactEntities(impact.Value, entities);
			return true;
		}

	#region Check methods

		private Collider[] AreaCheck()
		{
			return Physics.OverlapBox(Point.PlaneToWorld(), Vector3.one, Quaternion.identity, HitMask);
		}

		private Collider[] RaycastCheck()
		{
			Ray ray = new Ray(Point.PlaneToWorld(), Direction.PlaneToWorld());
			return Physics.RaycastAll(ray, MaxDistance, HitMask).Select(hit => hit.collider).ToArray();
		}

	#endregion
	}
}