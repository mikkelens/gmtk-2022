using System.Collections;
using Gameplay.Entities.Enemies;
using UnityEngine;

namespace Gameplay.Events
{
    public class CombatEvent : ScriptableObject
    {
        protected float StartTime;
        protected Transform SpawningParent;
        public void SetSpawningParent(Transform spawningParent)
        {
            SpawningParent = spawningParent;
        }
        
        // EVENT
        public virtual IEnumerator RunEvent() // base as setup
        {
            Debug.Log($"Started combat event: {name}");
            StartTime = Time.time;
            yield break;
        }
        
        // spawn enemy from enemy asset/prefab, return reference (in scene)
        protected virtual Enemy SpawnEnemy(Enemy enemyPrefab, Transform enemyParent)
        {
            Enemy spawnedEnemy = Instantiate(enemyPrefab, enemyParent).GetComponent<Enemy>();
            spawnedEnemy.SetSpawnOrigin(this);
            return spawnedEnemy;
        }
        public virtual void DespawnEnemy(Enemy enemyToDespawn)
        {
            Destroy(enemyToDespawn);
        }
    }
}