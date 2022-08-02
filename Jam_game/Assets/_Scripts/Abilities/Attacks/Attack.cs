using System.Collections.Generic;
using System.Linq;
using Abilities.Data;
using Entities.Base;
using Stats.Stat.Variants;
using Tools;

namespace Abilities.Attacks
{
	// something is considered a "weapon" when it or its projectile uses a hitbox/cast.
	public abstract class Attack : Ability
	{
		public Optional<IntStat> maxEntitiesHit;

		protected void ImpactEntities(ImpactData impact, List<Entity> entities)
		{
			if (maxEntitiesHit.Value <= 1)
				ImpactEntity(impact, entities.First());
			else
				entities.ForEach(entity => ImpactEntity(impact, entity));
		}
	}
}