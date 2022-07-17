using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Management
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance;

        [SerializeField] private float minSpawnDistance = 10f;
        [SerializeField] private Transform rootEnemyParent;
        [SerializeField] private List<CombatEvent> combatEvents = new List<CombatEvent>();

        private void Awake()
        {
            Instance = this;
        }

        [ShowInInspector]
        private float TotalWaveTime => combatEvents.OfType<WaveEvent>().Sum(wave => wave.waveTime);

        public void Start()
        {
            // start of game, start combat
            StartCoroutine(CombatEventLoop());
        }

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
