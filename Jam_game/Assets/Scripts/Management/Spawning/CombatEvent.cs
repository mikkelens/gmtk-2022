﻿using System.Collections;
using Gameplay.Entities.Enemies;
using UnityEngine;

namespace Management.Spawning
{
    public class CombatEvent : ScriptableObject
    {
        protected float StartTime;
        protected Transform SpawningParent;
        public void SetSpawningParent(Transform spawningParent)
        {
            SpawningParent = spawningParent;
        }
        
        public virtual IEnumerator RunEvent() // base as setup
        {
            Debug.Log($"Started combat event: {name}");
            StartTime = Time.time;
            yield break;
        }

        // spawn enemy from enemy asset/prefab, return reference (in scene)
        public Enemy SpawnEnemy(Enemy enemyPrefab, Transform enemyParent)
        {
            return Instantiate(enemyPrefab, enemyParent).GetComponent<Enemy>();
        }
    }
}