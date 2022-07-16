using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Management.Spawning
{
    public class CombatEventManager : MonoBehaviour
    {
        [SerializeField] private Transform rootEnemyParent;
        [SerializeField] private List<CombatEvent> combatEvents = new List<CombatEvent>();

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
