using System.Collections;
using Entities.Base;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "New Boss Event", menuName = "Events/Boss Event")]
    public class BossEvent : SpawnEvent
    {
        [AssetsOnly]
        [SerializeField] private Entity bossPrefabToSpawn;

        private Entity _boss;
        private bool _bossAlive;

        protected override bool EndEvent => base.EndEvent && !_bossAlive;
        protected override bool AllKilled => !_bossAlive;

        public override IEnumerator RunEvent()
        {
            yield return base.RunEvent();
            
            _boss = SpawnEntity(bossPrefabToSpawn, SpawningParent); // dont need reference, boss will call overridden despawn method
            _bossAlive = true;
            
            yield return new WaitUntil(() => EndEvent);
        }

        public override void DespawnEntity(Entity entity)
        {
            if (pickupToSpawnOnEnd.Enabled) PickupSpawnLocation = entity.transform.position.WorldToPlane();
            _bossAlive = false;
            _boss = null;
            base.DespawnEntity(entity);
        }
    }
}