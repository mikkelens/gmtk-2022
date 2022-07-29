using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Management
{
    public class EventsManager : MonoBehaviour
    {
        public static EventsManager Instance;
        
        [SerializeField] private float minSpawnDistance = 10f;
        [SerializeField] private Transform rootEnemyParent;
        [SerializeField] private List<GameEvent> gameEvents = new List<GameEvent>();

        [ShowInInspector]
        private float MinSpawnEventTime => gameEvents.OfType<SpawnEvent>()
            .Where(spawnEvent => spawnEvent.eventTime.Enabled)
            .Sum(spawnEvent => spawnEvent.eventTime.Value);


        private GameManager _manager;

        private void Awake()
        {
            Instance = this;
        }

        public void StartEventLoop(GameManager sourceManager)
        {
            _manager = sourceManager;
            StartCoroutine(GameEventLoop());
        }
        
        private IEnumerator GameEventLoop()
        {
            foreach (GameEvent gameEvent in gameEvents) // run each event
            {
			    Debug.Log($"Started event: {name}");
                if (gameEvent is SpawnEvent spawnEvent)
                {
                    spawnEvent.SetSpawningParent(rootEnemyParent);
                    spawnEvent.SetMinSpawnDistance(minSpawnDistance);
                }
                yield return gameEvent.RunEvent(); // run this event
                Debug.Log($"Ended event: {gameEvent.name}");
            }
            Debug.Log("Game ended.");
            _manager.State = GameState.Ended; // game end?
        }
    }
}
