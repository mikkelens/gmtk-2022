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
		public ImpactData impact;
		public Optional<IntStat> maxEntityHitAmount = (IntStat)1;
		public Optional<FloatStat> maxTravelTime = (FloatStat)10f;
	}
}