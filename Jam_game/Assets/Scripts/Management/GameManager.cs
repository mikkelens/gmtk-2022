using System;
using Gameplay.Entities.Players;
using Gameplay.Input;
using Gameplay.Level;
using Tools;
using UnityEngine;

namespace Management
{

    [DefaultExecutionOrder(-10)]
    public partial class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        private InputManager _inputManager;
        private Player _player;

        private GameState _state;

        private void Awake() => Instance = this;

        private void Start()
        {
            _inputManager = InputManager.Instance;
            if (_inputManager == null) Debug.LogError("InputManager is missing. Player input cannot be read.");

            _state = GameState.Playing;
            
            // start of game, start combat
            StartCoroutine(CombatEventLoop());
        }
    }
}