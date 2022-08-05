using System;
using Stats.Stat;
using Stats.Stat.Variants;
using Tools;

namespace Abilities.Data
{
	[Serializable]
	public class ProjectileData : IStatCollection
	{
		public MoveData move;
		public RotateData rotate;
		public Optional<IntStat> maxEntityHitAmount = (IntStat)1;
		public Optional<FloatStat> maxTravelTime = (FloatStat)10f;
	}
}