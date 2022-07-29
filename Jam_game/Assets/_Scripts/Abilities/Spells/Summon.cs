using System.Collections.Generic;
using Abilities.Base;
using Entities;
using Stats.Stat.Variants;
using Tools;

namespace Abilities.Spells
{
	// can summon entities
	public class Summon : Spell
	{
		public List<EntityData> entitySummons = new List<EntityData>();
		public Optional<IntStat> maxSimultaneousSummons = (IntStat)3;

		protected override void Use()
		{
			throw new System.NotImplementedException();
		}
	}
}