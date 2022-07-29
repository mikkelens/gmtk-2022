using System.Collections.Generic;
using System.Linq;
using Abilities.Data;
using Entities.Base;
using Stats.Stat.Variants;
using Tools;
using UnityEngine;

namespace Abilities.Base
{
	// something is considered a "weapon" when it or its projectile uses a hitbox/cast.
	public abstract class Weapon : Ability
	{
		public Optional<IntStat> maxEntitiesHit;

		protected void ImpactEntities(ImpactData impact, List<Entity> entities, Vector2 direction)
		{
			if (maxEntitiesHit.Value <= 1)
				ImpactEntity(impact, entities.First(), direction);
			else
				entities.ForEach(entity => ImpactEntity(impact, entity, direction));
		}
	}
}