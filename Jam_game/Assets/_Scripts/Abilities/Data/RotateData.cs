using System;
using Stats.Stat;
using Stats.Stat.Variants;
using Vector3 = UnityEngine.Vector3;

namespace Abilities.Data
{
	[Serializable]
	public class RotateData : IStatCollection
	{
		public Vector3 rotateDirection = Vector3.up;
		public FloatStat rotationSpeed = 3f;
		public BoolStat frozen;
	}
}