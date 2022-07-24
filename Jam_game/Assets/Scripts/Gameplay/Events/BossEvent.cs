using System.Collections;
using Gameplay.Entities.Enemies;
using Gameplay.Level;
using Management;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Gameplay.Events
{
    [CreateAssetMenu(fileName = "New Boss Event", menuName = "Events/Boss Event")]
    [TypeInfoBox("Event where a boss is spawned.")]
    public class BossEvent : CombatEvent
    {
        [SerializeField] private Enemy bossPrefabToSpawn;
        [SerializeField, AssetsOnly] private Pickup pickupToDrop;
        
        private bool _bossAlive;

        public override IEnumerator RunEvent()
        {
            yield return base.RunEvent();

            SpawnEnemy(bossPrefabToSpawn, SpawningParent); // dont need reference, boss will call overridden despawn method
            _bossAlive = true;
            
            yield return new WaitUntil(() => !_bossAlive);
        }

        public override void DespawnEnemy(Enemy enemyToDespawn)
        {
            GameManager.Instance.SpawnUpgrade(pickupToDrop, enemyToDespawn.transform.position.WorldToPlane());
            base.DespawnEnemy(enemyToDespawn);
            _bossAlive = false;
        }
    }
}