using System;
using Gameplay.Entities.Base;
using Tools;
using UnityEditor;
using UnityEngine;

namespace Gameplay.Attacks
{
	public class AreaBoxWeapon : Weapon
	{
		public Transform physicsBox;

		private void OnValidate()
		{
			if (physicsBox == null) return;
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(physicsBox.position, physicsBox.localScale);
		}

		protected override Collider[] CastForEntity(CombatEntity source, Vector2 direction, LayerMask targetLayerMask)
		{
			Vector2 center = physicsBox.position.WorldToPlane();
			return Physics.OverlapBox(center.PlaneToWorld(), physicsBox.localScale, Quaternion.identity, ~targetLayerMask.value);
		}
	}
}