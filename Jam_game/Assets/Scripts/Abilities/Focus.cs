using System.Collections;
using Abilities.Data;
using UnityEngine;

namespace Abilities
{
	// can modify stats
	[CreateAssetMenu(fileName = "New Focus Ability", menuName = MenuPath + "Focus")]
	public class Focus : Ability
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