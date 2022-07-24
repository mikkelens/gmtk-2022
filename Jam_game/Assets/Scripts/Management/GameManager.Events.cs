using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Events;
using Gameplay.Level;
using Sirenix.OdinInspector;
using Tools;
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
        [SerializeField] private List<CombatEvent> combatEvents = new List<CombatEvent>();

        [FoldoutGroup("Events")]
        [ShowInInspector]
        private float TotalWaveTime => combatEvents.OfType<WaveEvent>().Sum(wave => wave.waveTime);

        private IEnumerator CombatEventLoop()
        {
            // Run each event
            foreach (CombatEvent combatEvent in combatEvents)
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
