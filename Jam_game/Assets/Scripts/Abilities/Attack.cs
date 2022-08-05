using Abilities.Data;
using UnityEngine;

namespace Abilities
{
	// something is considered a "weapon" when it or its projectile uses a hitbox/cast.
	public abstract class Attack : Ability
	{
		public ImpactData hitImpact;
		public LayerMask TargetMask => SourceEntity == null ? LayerMask.NameToLayer("Enemy") : SourceEntity.TargetLayerMask;
	}
}