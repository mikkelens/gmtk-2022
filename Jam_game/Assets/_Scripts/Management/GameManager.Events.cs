using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Management
{
    public partial class GameManager
    {
        [FoldoutGroup("Events")]
        [SerializeField] private float minSpawnDistance = 10f;
        [FoldoutGroup("Events")]
        [SerializeField] private Transform rootEnemyParent;
        [FoldoutGroup("Events")]
        [SerializeField] private List<SpawnEvent> spawnEvents = new List<SpawnEvent>();

        [FoldoutGroup("Events")]
        [ShowInInspector]
        private float TotalWaveTime => spawnEvents.OfType<WaveEvent>().Sum(wave => wave.waveTime);

        private IEnumerator CombatEventLoop()
        {
            // Run each event
            foreach (SpawnEvent combatEvent in spawnEvents)
            {
                combatEvent.SetSpawningParent(rootEnemyParent);
                combatEvent.SetMinSpawnDistance(minSpawnDistance);
                // Run this event
                yield return combatEvent.RunEvent();
                Debug.Log($"Ended event: {combatEvent.name}");
            }
            // game end?
        }
    }
}
