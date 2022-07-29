using System;
using Abilities.Data;

namespace Abilities.Base
{
	[Serializable]
	public class AbilityMetrics
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
		
		// public static AbilityMetrics operator +(AbilityMetrics a, ImpactResultData b)
		// {
		// 	return a.WithData(b);
		// }
	}
}