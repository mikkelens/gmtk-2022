using Entities.Base;
using Tools;
using UnityEditor;
using UnityEngine;

namespace Abilities.Weapons
{
	public class AreaMeleeWeapon : MeleeWeapon
	{
		public Optional<Transform> physicsBox;

	#if UNITY_EDITOR
		private void OnValidate()
		{
			if (!physicsBox.Enabled) return;
			if (physicsBox.Value == null) return;
			Handles.DrawWireCube(physicsBox.Value.position, physicsBox.Value.localScale);
		}
	#endif

		protected override Collider[] CastForEntity(CombatEntity source, Vector2 direction, LayerMask targetLayerMask)
		{
			Vector2 center = physicsBox.Enabled ? physicsBox.Value.position.WorldToPlane() : source.transform.position.WorldToPlane();
			return Physics.OverlapBox(center.PlaneToWorld(), Vector3.one, Quaternion.identity, ~targetLayerMask.value);
		}

		public override void UseAbility()
		{
			throw new System.NotImplementedException();
		}
	}
}