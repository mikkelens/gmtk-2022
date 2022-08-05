using System;
using Abilities.Data;

namespace Entities
{
	[Serializable]
	public class Metrics
	{
		public int totalDamage;
		public int totalHealing;
		public int totalKills;

		public void AddData(ImpactResultData resultData)
		{
			totalDamage += resultData.Damage;
			totalHealing += resultData.Healing;
			totalKills += resultData.Kills;
		}
	}
}