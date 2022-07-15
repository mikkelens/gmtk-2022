using System;
using Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        [SerializeField, Required] public Player2D player; // player in scene. input needs this reference.

        private InputManager _inputManager;
        private int _killCount;
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _inputManager = InputManager.Instance;
            if (_inputManager == null) throw new Exception("InputManager is missing. Player input cannot be read.");
        }

        private void AddKillStatistic(int number = 1)
        {
            _killCount += number;
            // todo: update ui
        }
    }
}