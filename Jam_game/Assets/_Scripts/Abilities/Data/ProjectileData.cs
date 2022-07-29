using System;
using Stats.Stat;

namespace Abilities.Data
{
	[Serializable]
	public class ProjectileData : IStatCollection
	{
		public MoveData move;
		public RotateData rotate;
		public ImpactData impact;
	}
}