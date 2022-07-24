using System.Collections;
using Gameplay.Entities.Enemies;
using Management;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Events
{
    [CreateAssetMenu(fileName = "New Boss Event", menuName = "Events/Boss Event")]
    [TypeInfoBox("Event where a boss is spawned.")]
    public class BossEvent : CombatEvent
    {
        [SerializeField] private Enemy bossPrefabToSpawn;
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

        public override void DespawnEnemy(Enemy enemyToDespawn)
        {
            base.DespawnEnemy(enemyToDespawn);
            _bossAlive = false;
        }
    }
}