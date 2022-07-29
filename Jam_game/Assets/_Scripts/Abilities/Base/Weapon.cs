using Stats.Stat.Variants;
using Tools;

namespace Abilities.Base
{
	// something is considered a "weapon" when it or its projectile uses a hitbox/cast.
	public abstract class Weapon : Ability
	{
		public Optional<IntStat> maxEntitiesHit = (IntStat)1;
	}
}