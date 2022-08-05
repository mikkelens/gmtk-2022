using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Components;
using Game;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Events
{
    public class EventsManager : MonoBehaviour
    {
        public static EventsManager Instance;
        
        [SerializeField] private float extraSpawnDistance = 12f;
        [SerializeField] private Transform rootEnemyParent;
        [SerializeField] private Optional<float> standardEventDelay = 1f;
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
                if (standardEventDelay.Enabled) yield return new WaitForSeconds(standardEventDelay.Value);
			    Debug.Log($"Started event: {gameEvent.name}");
                if (gameEvent is SpawnEvent spawnEvent)
                {
                    spawnEvent.SpawningParent = rootEnemyParent;
                    spawnEvent.ExtraSpawnDistance = extraSpawnDistance;
                }
                gameEvent.Manager = this;
                yield return StartCoroutine(gameEvent.RunEvent()); // run this event
                Debug.Log($"Ended event: {gameEvent.name}");
            }
            Debug.Log("Game ended.");
            _manager.State = GameState.Ended; // game end?
        }

        private void OnDrawGizmos()
        {
            if (CameraController.Instance == null) return;
            Gizmos.color = Color.blue;
            Vector3 center = CameraTools.PositionNoOffset.PlaneToWorld() + Vector3.forward * CameraTools.AngleDistanceOffset();
            Vector3 box = Vector3.one.FlattenBox() * extraSpawnDistance + CameraTools.VisibleGroundFrustum.PlaneToWorld();
            Gizmos.DrawWireCube(center, box);
        }
    }
}
