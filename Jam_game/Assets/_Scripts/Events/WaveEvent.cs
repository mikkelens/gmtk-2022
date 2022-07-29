using System.Collections;
using System.Collections.Generic;
using Entities;
using Entities.Base;
using Management;
using Tools;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "New Wave Event", menuName = "Events/Wave Event")]
    public class WaveEvent : SpawnEvent
    {
        public List<EntityData> entitiesToSpawn = new List<EntityData>();
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
                    Entity asset = SpawnSystem.SelectEntityAsset(entitiesToSpawn);
                    if (asset == null) continue;
                    _spawnedEntities.Add(SpawnEntity(asset, waveParent));
                    _spawnCount++;
                }
            }
        }

        public override void DespawnEntity(Entity entity)
        {
            _killCount++;
            _spawnedEntities.Remove(entity);
            base.DespawnEntity(entity);
        }
    }
}