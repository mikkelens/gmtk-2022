using System.Collections;
using System.Collections.Generic;
using Gameplay.Events;
using UnityEngine;

namespace Management
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Instance;
        
        [SerializeField] private Transform rootEnemyParent;
        [SerializeField] private List<CombatEvent> combatEvents = new List<CombatEvent>();

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
                combatEvent.SetSpawningParent(rootEnemyParent);
                // Run this event
                yield return combatEvent.RunEvent();
                Debug.Log($"Ended event: {combatEvent.name}");
            }
            // game end?
        }
    }
}
