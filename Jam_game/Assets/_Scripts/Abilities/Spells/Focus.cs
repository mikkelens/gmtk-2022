using System.Collections;
using Abilities.Data;
using UnityEngine;

namespace Abilities.Spells
{
	// can modify stats
	[CreateAssetMenu(fileName = "New Focus Ability", menuName = MenuPath + "Focus")]
	public class Focus : Spell
	{
		[Header("Focus")]
		public ImpactData selfImpact;
		
		protected override IEnumerator Use()
		{
			ImpactEntity(selfImpact, SourceEntity);
			yield break;
		}
	}
}