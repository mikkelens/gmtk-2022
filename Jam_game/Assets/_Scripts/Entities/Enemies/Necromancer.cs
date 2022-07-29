using Sirenix.OdinInspector;
using Stats.Stat.Variants;
using UnityEngine;

namespace Entities.Enemies
{
	public class Necromancer : Enemy
	{
		[Header("Necromancer specific")]
		[FoldoutGroup("Stats")]
		[SerializeField] protected FloatStat summonSpeed;
	}
}