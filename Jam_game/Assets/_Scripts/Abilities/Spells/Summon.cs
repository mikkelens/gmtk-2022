using System.Collections;
using System.Collections.Generic;
using Entities;
using Entities.Base;
using Events;
using Management;
using Stats.Stat.Variants;
using Tools;
using UnityEditor;
using UnityEngine;

namespace Abilities.Spells
{
	// can summon entities
	[CreateAssetMenu(fileName = "New Summon Ability", menuName = MenuPath + "Summon")]
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

		private Vector2 RandomPos
		{
			get
			{
				Vector2 location = Point + spawnPositionOffsets.Value.RandomItem();
				if (spawnPositionRandomness.Enabled) location += Random.insideUnitCircle * spawnPositionRandomness.Value;
				return location;
			}
		}

		protected override IEnumerator Use()
		{
			yield return SourceEntity.StartCoroutine(SummonRoutine(SourceEntity.SpawnOrigin));
		}

		public IEnumerator SummonRoutine(SpawnEvent spawnOrigin)
        {
            List<EntityData> entitiesToSummon = new List<EntityData>(summonEntities);
            List<Entity> summonedEntities = new List<Entity>();
            while (summonedEntities.Count <= maxSimultaneousSummons.Value && entitiesToSummon.Count > 0)
            {
                if (summonDelayTime.Enabled) yield return new WaitForSeconds(summonDelayTime.Value);
                EntityData data = entitiesToSummon.SelectEntityAsset();
                Vector2 pos = RandomPos;
                Entity entity = Instantiate(data.prefab, pos.PlaneToWorld(), Quaternion.identity, spawnOrigin.SpawningParent);
                summonedEntities.Add(entity);
                if (summonBehaviour == SummonBehaviours.SummonEachOnce) entitiesToSummon.Remove(data);
            }
        }
		
	#if UNITY_EDITOR
		private void OnValidate()
		{
			if (spawnPositionOffsets.Enabled)
			{
				foreach (Vector2 offset in spawnPositionOffsets.Value)
				{
					Vector2 pos = Point + offset;
					Handles.DrawWireCube(pos.PlaneToWorld(), Vector3.one);
				}
			}
			else
			{
				Handles.DrawWireCube(Point.PlaneToWorld(), Vector3.one);
			}
		}
	#endif
	}
}