using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entities.Base;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Events
{
    [CreateAssetMenu(fileName = "New Wave Event", menuName = "Events/Wave Event")]
    public class WaveEvent : SpawnEvent
    {
        public List<WaveEntityData> entitiesToSpawn = new List<WaveEntityData>();
        public float spawnDelay = 2f;
        public Optional<AnimationCurve> spawnDelayCurve;
        public Optional<int> endOnKillCount;

        private List<Entity> _spawnedEntities;
        private int _entityKills;


        private float CurrentSpawnDelay => spawnDelayCurve.Enabled && spawnDelayCurve.Value != null
            ? spawnDelayCurve.Value.Evaluate(EventCompletion) * spawnDelay
            : spawnDelay;
        private float KillCompletion => endOnKillCount.Enabled ? (float)_entityKills / endOnKillCount.Value : 0f;
        protected override float EventCompletion => Mathf.Max(TimeCompletion, KillCompletion);

        public override IEnumerator RunEvent()
        {
            yield return base.RunEvent();

            // Create wave parent
            Transform waveParent = Instantiate(new GameObject("Parent: Wave"), SpawningParent).transform;

            _spawnedEntities = new List<Entity>();
            
            // continuosly run wave
            while (!EndEvent)
            {
                // wait
                yield return new WaitUntil(() => !GameIsPaused);
                yield return new WaitForSeconds(CurrentSpawnDelay);
                
                // spawn enemies
                Entity asset = SelectEntityAsset(entitiesToSpawn);
                if (asset == null) continue;
                _spawnedEntities.Add(SpawnEntity(asset, waveParent));
            }
        }

        private static Entity SelectEntityAsset(IReadOnlyCollection<WaveEntityData> allEnemies)
        {
            if (allEnemies.Count == 0) return null;
            
            // count up spawn chances as a range, then generate a number within the range. Enemy with lowest number but above generated number will be chosen.
            float totalSpawnRange = allEnemies.Sum(enemy => enemy.relativeSpawnChance);
            float random = Random.Range(0, totalSpawnRange);
            float last = 0f;
            WaveEntityData selectedEnemy = allEnemies.First(enemy =>
            {
                last += enemy.relativeSpawnChance;
                return last >= random;
            });
            return selectedEnemy.prefab;
        }

        public override void DespawnEntity(Entity entity)
        {
            _entityKills++;
            _spawnedEntities.Remove(entity);
            base.DespawnEntity(entity);
        }
    }
}