﻿using System.Collections;
using Gameplay.Entities.Enemies;
using Management;
using UnityEngine;

namespace Gameplay.Events
{
    public class BossEvent : CombatEvent
    {
        [SerializeField] private Boss bossPrefabToSpawn;
        [SerializeField] private float lootValue;
        
        private bool _bossAlive;

        public override IEnumerator RunEvent()
        {
            yield return base.RunEvent();

            SpawnEnemy(bossPrefabToSpawn, SpawningParent); // dont need reference, boss will call overridden despawn method
            _bossAlive = true;
            
            yield return new WaitUntil(() => !_bossAlive);
            
            GameManager.Instance.SpawnUpgrade();
        }

        protected override Enemy SpawnEnemy(Enemy enemyPrefab, Transform enemyParent)
        {
            _bossAlive = true;
            return base.SpawnEnemy(enemyPrefab, enemyParent);
        }

        public override void DespawnEnemy(Enemy enemyToDespawn)
        {
            base.DespawnEnemy(enemyToDespawn);
            _bossAlive = false;
        }
    }
}