using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Entities.Enemies;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Events
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Wave", menuName = "Spawning/Wave")]
    public class WaveEvent : CombatEvent
    {
        public float spawnDelay = 2f;
        public float waveTime = 30f;
        [AssetsOnly]
        public List<Enemy> enemyPrefabsToSpawn;
        
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

        private Enemy SelectEnemyAsset(List<Enemy> allEnemies)
        {
            // count up spawn chances as a range, then generate a number within the range. Enemy with lowest number but above generated number will be chosen.
            List<float> rangeStartList = new List<float>();
            float lastEnd = 0; // start at zero
            foreach (Enemy enemy in allEnemies)
            {
                // previous -> previous + spawnchance
                rangeStartList.Add(lastEnd);
                float spawnChance = enemy.relativeSpawnChance;
                lastEnd += spawnChance;
            }
            float randomValue = Random.Range(0, lastEnd);
            int chosenIndex = 0;
            for (int i = 0; i < allEnemies.Count; i++)
            {
                float thisRange = rangeStartList[i];
                if (thisRange >= randomValue)
                {
                    chosenIndex = i;
                    break;
                }
            }
            return allEnemies[chosenIndex];
        }

        public override void DespawnEnemy(Enemy enemyToDespawn)
        {
            _spawnedEnemies.Remove(enemyToDespawn);
            base.DespawnEnemy(enemyToDespawn);
        }
    }
}