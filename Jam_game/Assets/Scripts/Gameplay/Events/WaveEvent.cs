using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Entities.Base;
using Gameplay.Level;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Events
{
    [CreateAssetMenu(fileName = "New Wave", menuName = "Events/Wave Event")]
    [TypeInfoBox("Event where a wave of enemies spawn for a while.")]
    public class WaveEvent : CombatEvent
    {
        public float spawnDelay = 2f;
        public float waveTime = 30f;
        [AssetsOnly]
        public List<EntityData> entitiesToSpawn = new List<EntityData>();
        public Optional<Pickup> pickupToSpawn;

        // ReSharper disable once CollectionNeverQueried.Local
        private List<Entity> _spawnedEntities;

        public override IEnumerator RunEvent()
        {
            yield return base.RunEvent();

            // Create wave parent
            Transform waveParent = Instantiate(new GameObject("Parent: Wave"), SpawningParent).transform;

            _spawnedEntities = new List<Entity>();
            
            // continuosly run wave
            while (StartTime.TimeSince() <= waveTime)
            {
                // spawn enemies
                Entity asset = SelectEntityAsset(entitiesToSpawn);
                _spawnedEntities.Add(SpawnEntity(asset, waveParent));
                
                // wait
                yield return new WaitForSeconds(spawnDelay);
            }
            if (pickupToSpawn.Enabled) SpawnPickup(pickupToSpawn.Value, GetRandomSpawnLocation());
        }

        private Entity SelectEntityAsset(IReadOnlyCollection<EntityData> allEnemies)
        {
            // count up spawn chances as a range, then generate a number within the range. Enemy with lowest number but above generated number will be chosen.
            float totalSpawnRange = allEnemies.Sum(enemy => enemy.relativeSpawnChance);
            float random = Random.Range(0, totalSpawnRange);
            float last = 0f;
            EntityData selectedEnemy = allEnemies.First(enemy =>
            {
                last += enemy.relativeSpawnChance;
                return last >= random;
            });
            return selectedEnemy.prefab;
        }

        public override void DespawnEntity(Entity entity)
        {
            _spawnedEntities.Remove(entity);
            base.DespawnEntity(entity);
        }
    }
}