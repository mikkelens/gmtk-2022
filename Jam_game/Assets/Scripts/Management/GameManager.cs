using System;
using Gameplay.Input;
using UnityEngine;

namespace Management
{
    [DefaultExecutionOrder(-10)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public enum GameState
        {
            Playing,
            Paused
        }

        private InputManager _inputManager;
        private EventManager _eventManager;
        private UpgradeManager _upgradeManager;
        private UIManager _uiManager;

        public GameState State { get; private set; }

        private int _killCount;

        private void Awake() => Instance = this;

        private void Start()
        {
            _inputManager = InputManager.Instance;
            if (_inputManager == null) Debug.LogError("InputManager is missing. Player input cannot be read.");
            
            _eventManager = EventManager.Instance;
            if (_eventManager == null) Debug.LogWarning("EventManager is missing. Events will not start.");
            
            _uiManager = UIManager.Instance;
            if (_uiManager == null) Debug.LogWarning("UIManager is missing. UI will not respond to game.");
            
            _upgradeManager = UpgradeManager.Instance;

            State = GameState.Playing;
        }

        public void IncreaseKillcount()
        {
            _killCount++;
            _uiManager.UpdateKillCount(_killCount);
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