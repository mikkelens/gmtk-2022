using System;
using Stats.Stat;
using Stats.Stat.Variants;
using UnityEngine;

namespace Abilities.Data
{
	[Serializable]
	public class MoveData : IStatCollection
	{
		public Vector2 moveDirection;
		public FloatStat moveSpeed;
		public BoolStat frozen;
	}
}