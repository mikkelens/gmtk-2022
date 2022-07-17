using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Entities.Enemies;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Events
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Wave Asset", menuName = "CombatEvents/Wave Event Asset")]
    public class WaveEvent : CombatEvent
    {
        public float spawnDelay = 2f;
        public float waveTime = 30f;
        [AssetsOnly]
        public List<Enemy> enemyPrefabsToSpawn = new List<Enemy>();
        
        private List<Enemy> _spawnedEnemies = new List<Enemy>();

        public override IEnumerator RunEvent()
        {
            yield return base.RunEvent();

            // Create wave parent
            Transform waveParent = Instantiate(new GameObject("Parent: Wave"), SpawningParent).transform;
            
            // continuosly run wave
            while (StartTime.TimeSince() <= waveTime)
            {
                // spawn enemies
                Enemy enemyAsset = SelectEnemyAsset(enemyPrefabsToSpawn);
                _spawnedEnemies.Add(SpawnEnemy(enemyAsset, waveParent));
                
                // wait
                yield return new WaitForSeconds(spawnDelay);
            }
        }

        private Enemy SelectEnemyAsset(IReadOnlyCollection<Enemy> allEnemies)
        {
            // count up spawn chances as a range, then generate a number within the range. Enemy with lowest number but above generated number will be chosen.
            float totalSpawnRange = allEnemies.Sum(enemy => enemy.stats.relativeSpawnChance);
            float random = Random.Range(0, totalSpawnRange);
            float last = 0f;
            return allEnemies.First(enemy =>
            {
                last += enemy.stats.relativeSpawnChance;
                return last >= random;
            });
        }

        public override void DespawnEnemy(Enemy enemyToDespawn)
        {
            _spawnedEnemies.Remove(enemyToDespawn);
            base.DespawnEnemy(enemyToDespawn);
        }
    }
}