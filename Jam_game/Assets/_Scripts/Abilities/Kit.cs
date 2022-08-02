using System;
using Stats.Stat;
using Tools;

namespace Abilities
{
	[Serializable]
	public class Kit : IStatCollection
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