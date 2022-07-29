using System.Collections.Generic;
using Abilities.Base;
using Entities;
using Stats.Stat.Variants;
using Tools;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Abilities.Spells
{
	// can summon entities
	public class Summon : Spell
	{
		public enum SummonBehaviours
		{
			SummonUntillDead,
			SummonEachOnce
		}
		public SummonBehaviours summonBehaviour;
		public List<EntityData> summonEntities = new List<EntityData>();
		public Optional<IntStat> maxSimultaneousSummons = (IntStat)3;
		public Optional<FloatStat> summonDelayTime;
		public Optional<FloatStat> spawnPositionRandomness = (FloatStat) 0.35f;
		public Optional<List<Vector2>> spawnPositionOffsets;

		public Vector2 RandomPos
		{
			get
			{
				Vector2 location = AttackPoint + spawnPositionOffsets.Value.RandomItem();
				if (spawnPositionRandomness.Enabled) location += Random.insideUnitCircle * spawnPositionRandomness.Value;
				return location;
			}
		}

	#if UNITY_EDITOR
		private void OnValidate()
		{
			if (spawnPositionOffsets.Enabled)
			{
				foreach (Vector2 offset in spawnPositionOffsets.Value)
				{
					Vector2 pos = AttackPoint + offset;
					Handles.DrawWireCube(pos.PlaneToWorld(), Vector3.one);
				}
			}
			else
			{
				Handles.DrawWireCube(AttackPoint.PlaneToWorld(), Vector3.one);
			}
		}
	#endif
	}
}