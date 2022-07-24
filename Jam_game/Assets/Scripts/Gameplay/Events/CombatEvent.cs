using System.Collections;
using Gameplay.Entities.Enemies;
using Tools;
using UnityEngine;

namespace Gameplay.Events
{
    public abstract class CombatEvent : ScriptableObject
    {
        protected float StartTime;
        protected Transform SpawningParent;
        protected float MinSpawnDistance;
        public void SetSpawningParent(Transform spawningParent) => SpawningParent = spawningParent;
        public void SetMinSpawnDistance(float minSpawnDistance) => MinSpawnDistance = minSpawnDistance;

        public virtual IEnumerator RunEvent() // base as setup
        {
            Debug.Log($"Started combat event: {name}");
            StartTime = Time.time;
            yield break;
        }

        protected Vector2 GetRandomSpawnLocation() // Location outside of viewable area
        {
            bool randomX = Random.value > 0.5f;
            float randomValue = Random.Range(0, MinSpawnDistance);
            return new Vector2(randomX ? randomValue : MinSpawnDistance, randomX ? MinSpawnDistance : randomValue);
        }
        
        protected Quaternion GetRandomSpawnRotation() // Rotation around the z axis
        {
            return Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        }
        
        // spawn enemy from enemy asset/prefab, return reference (in scene)
        protected Enemy SpawnEnemy(Enemy enemyPrefab, Transform enemyParent)
        {
            Enemy spawnedEnemy = Instantiate(enemyPrefab, GetRandomSpawnLocation().PlaneToWorld(), GetRandomSpawnRotation(), enemyParent).GetComponent<Enemy>();
            spawnedEnemy.SetSpawnOrigin(this);
            return spawnedEnemy;
        }

        public virtual void DespawnEnemy(Enemy enemyToDespawn)
        {
            Destroy(enemyToDespawn.gameObject);
        }

    }
}