using Stats.Stat;
using Tools;
using UnityEngine;

namespace Abilities
{
	[CreateAssetMenu(fileName = "New Ability Kit", menuName = Ability.MenuPath + "Ability Kit")]
	public class AbilityKit : ExpandableScriptableObject, IStatCollection
	{
		public Ability primary;
		public Optional<Ability> secondary;

		public Ability GetRandomAbility()
		{
			if (!secondary.Enabled) return primary;
			return RandomTools.NextBool() ? primary : secondary.Value;
		}
	}
}