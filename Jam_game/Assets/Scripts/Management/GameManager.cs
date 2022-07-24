using System;
using Gameplay.Entities.Players;
using Gameplay.Input;
using UnityEngine;

namespace Management
{
    [DefaultExecutionOrder(-10)]
    public partial class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public enum GameState
        {
            Playing,
            Paused
        }

        private InputManager _inputManager;
        private Player _player;

        public GameState State { get; private set; }

        private int _killCount;

        private void Awake() => Instance = this;

        private void Start()
        {
            _inputManager = InputManager.Instance;
            if (_inputManager == null) Debug.LogError("InputManager is missing. Player input cannot be read.");

            State = GameState.Playing;
            
            // start of game, start combat
            StartCoroutine(CombatEventLoop());
        }

        public void SpawnUpgrade()
        {
            
        }
        
        private void GetNextUpgrade()
        {
            throw new NotImplementedException();
        }

    }
}