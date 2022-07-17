using System;
using Gameplay.Input;
using UnityEngine;

namespace Management
{
    [DefaultExecutionOrder(-10)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private InputController _inputController;
        private int _killCount;
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _inputController = InputController.Instance;
            if (_inputController == null) throw new Exception("InputManager is missing. Player input cannot be read.");
        }

        public void IncreaseKillcount()
        {
            _killCount++;
            // todo: update ui
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