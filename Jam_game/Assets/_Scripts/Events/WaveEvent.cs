using System.Collections;
using System.Collections.Generic;
using Entities;
using Entities.Base;
using Management;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "New Wave Event", menuName = "Events/Wave Event")]
    public class WaveEvent : SpawnEvent
    {
        [Required]
        public List<EntityData> entitiesToSpawn = new List<EntityData>();
        public Optional<float> spawnDelay = 2f;
        public Optional<AnimationCurve> spawnDelayCurve;
        // [Min(1)]
        public Optional<int> endOnSpawnCount;
        // [Min(1)]
        public Optional<int> maxEnemiesSimultaneously = 5;
        // [Min(1)]
        public Optional<int> endOnKillCount;

        private List<Entity> _spawnedEntities;
        private int _spawnCount;
        private int _killCount;

        [PropertyOrder(10)]
        public bool requireAllKilledToContinue = true;

        private float CurrentSpawnDelay => spawnDelay.Enabled
        ? spawnDelayCurve.Enabled && spawnDelayCurve.Value != null
        ? spawnDelayCurve.Value.Evaluate(EventCompletion) * spawnDelay.Value
        : spawnDelay.Value
        : 0f;

        private float SpawnCompletion => endOnSpawnCount.Enabled ? (float)_spawnCount / endOnSpawnCount.Value : 0f;
        private float KillCompletion => endOnKillCount.Enabled ? (float)_killCount / endOnKillCount.Value : 0f;
        private float EntityCompletion => Mathf.Max(SpawnCompletion, KillCompletion);
        protected override float EventCompletion => Mathf.Max(TimeCompletion, EntityCompletion);
        protected override bool AllKilled => _spawnedEntities.Count == 0;

        public override IEnumerator RunEvent()
        {
            yield return Manager.StartCoroutine(base.RunEvent());
            if (entitiesToSpawn.Count == 0)
            {
                Debug.LogWarning($"There are no entities attached to wave event: {name}");
                yield break;
            }

            // Create wave parent
            Transform waveParent = Instantiate(new GameObject("Parent: Wave"), SpawningParent).transform;
            _spawnedEntities = new List<Entity>();
            
            // continuosly spawn entities
            while (!EndEvent)
            {
                // wait
                yield return new WaitUntil(() => !GameIsPaused);
                yield return new WaitForSeconds(CurrentSpawnDelay);

                if (!maxEnemiesSimultaneously.Enabled || _spawnedEntities.Count < maxEnemiesSimultaneously.Value)
                {
                    // select and spawn enemy
                    Entity asset = entitiesToSpawn.SelectEntityAsset().prefab;
                    if (asset == null) continue;
                    _spawnedEntities.Add(SpawnEntity(asset, waveParent));
                    _spawnCount++;
                    Debug.Log($"Spawned Entity: {asset.name}");
                }
            }
            // done spawning, maybe wait for entities to die
            if (requireAllKilledToContinue) yield return new WaitUntil(() => AllKilled);
        }

        public override void DespawnEntity(Entity entity)
        {
            _killCount++;
            _spawnedEntities.Remove(entity);
            base.DespawnEntity(entity);
        }
    }
}