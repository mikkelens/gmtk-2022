using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Entities.Enemies;
using UnityEngine;

namespace Gameplay.Spawning
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Instance;
        
        [SerializeField] private Transform rootEnemyParent;
        [SerializeField] private List<CombatEvent> combatEvents = new List<CombatEvent>();

        private CombatEvent _currentCombatEvent;

        private void Awake()
        {
            Instance = this;
        }

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
                _currentCombatEvent = combatEvent;
                combatEvent.SetSpawningParent(rootEnemyParent);
                // Run this event
                yield return combatEvent.RunEvent();
                Debug.Log($"Ended event: {combatEvent.name}");
            }
            // game end?
        }
    }
}
