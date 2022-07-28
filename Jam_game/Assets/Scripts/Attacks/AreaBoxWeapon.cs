using Entities.Base;
using Tools;
using UnityEditor;
using UnityEngine;

namespace Attacks
{
	public class AreaBoxWeapon : Weapon
	{
		public Transform physicsBox;

	#if UNITY_EDITOR
		private void OnValidate()
		{
			if (physicsBox == null) return;
			Handles.DrawWireCube(physicsBox.position, physicsBox.localScale);
		}
	#endif

		protected override Collider[] CastForEntity(CombatEntity source, Vector2 direction, LayerMask targetLayerMask)
		{
			Vector2 center = physicsBox.position.WorldToPlane();
			return Physics.OverlapBox(center.PlaneToWorld(), physicsBox.localScale, Quaternion.identity, ~targetLayerMask.value);
		}
	}
}