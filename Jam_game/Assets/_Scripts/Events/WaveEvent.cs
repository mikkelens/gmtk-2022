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
        [Min(1)]
        public Optional<uint> endOnSpawnCount;
        [Min(1)]
        public Optional<uint> maxEnemiesSimultaneously = 5;
        [Min(1)]
        public Optional<uint> endOnKillCount;

        private List<Entity> _spawnedEntities;
        private uint _spawnCount;
        private uint _killCount;
        
        private float CurrentSpawnDelay => spawnDelayCurve.Enabled && spawnDelayCurve.Value != null
            ? spawnDelayCurve.Value.Evaluate(EventCompletion) * spawnDelay
            : spawnDelay;

        private float SpawnCompletion => endOnSpawnCount.Enabled ? (float)_spawnCount / endOnSpawnCount.Value : 0f;
        private float KillCompletion => endOnKillCount.Enabled ? (float)_killCount / endOnKillCount.Value : 0f;
        private float EntityCompletion => Mathf.Max(SpawnCompletion, KillCompletion);
        protected override float EventCompletion => Mathf.Max(TimeCompletion, EntityCompletion);
        protected override bool AllKilled => _spawnedEntities.Count == 0;

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

                if (!maxEnemiesSimultaneously.Enabled || _spawnedEntities.Count < maxEnemiesSimultaneously.Value)
                {
                    // select and spawn enemy
                    Entity asset = SelectEntityAsset(entitiesToSpawn);
                    if (asset == null) continue;
                    _spawnedEntities.Add(SpawnEntity(asset, waveParent));
                    _spawnCount++;
                }
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
            _killCount++;
            _spawnedEntities.Remove(entity);
            base.DespawnEntity(entity);
        }
    }
}