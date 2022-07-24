using System.Collections;
using Gameplay.Entities.Base;
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
        [SerializeField] private Entity bossPrefabToSpawn;
        [SerializeField, AssetsOnly] private Pickup pickupToDrop;
        
        private bool _bossAlive;

        public override IEnumerator RunEvent()
        {
            yield return base.RunEvent();

            SpawnEntity(bossPrefabToSpawn, SpawningParent); // dont need reference, boss will call overridden despawn method
            _bossAlive = true;
            
            yield return new WaitUntil(() => !_bossAlive);
        }

        public override void DespawnEntity(Entity entity)
        {
            GameManager.Instance.SpawnUpgrade(pickupToDrop, entity.transform.position.WorldToPlane());
            base.DespawnEntity(entity);
            _bossAlive = false;
        }
    }
}