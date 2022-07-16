using System.Collections;
using Gameplay.Entities.Enemies;
using UnityEngine;

namespace Management.Spawning
{
    public class BossCombatEvent : CombatEvent
    {
        [SerializeField] private Boss bossPrefabToSpawn;
        [SerializeField] private float lootValue;

        private Boss _spawnedBoss;

        public override IEnumerator RunEvent()
        {
            yield return base.RunEvent();
            
            yield return new WaitUntil(() => !_spawnedBoss.Alive);
        }
    }
}