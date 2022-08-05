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
		public List<Possible<Entity>> summonEntities = new List<Possible<Entity>>();
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

		private IEnumerator SummonRoutine(SpawnEvent spawnOrigin)
        {
            List<Possible<Entity>> entitiesToSummon = new List<Possible<Entity>>(summonEntities);
            List<Entity> summonedEntities = new List<Entity>();
            while (summonedEntities.Count <= maxSimultaneousSummons.Value && entitiesToSummon.Count > 0)
            {
                if (summonDelayTime.Enabled) yield return new WaitForSeconds(summonDelayTime.Value);
                Possible<Entity> selectedEntity = entitiesToSummon.SelectPossibleRelative();
                Vector2 pos = RandomPos;
                Entity entity = Instantiate(selectedEntity.Value, pos.PlaneToWorld(), Quaternion.identity, spawnOrigin.SpawningParent);
                summonedEntities.Add(entity);
                if (summonBehaviour == SummonBehaviours.SummonEachOnce) entitiesToSummon.Remove(selectedEntity);
            }
        }
		
		// todo: test if this works
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