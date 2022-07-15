using System;
using Gameplay;
using UnityEngine;

namespace Input
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;
        private GameManager _manager;
        
        private InputSettings _settings;

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            _settings.Enable();
        }

        private void OnDisable()
        {
            _settings.Disable();
        }

        private void Start()
        {
            _manager = GameManager.Instance;
            _settings = new InputSettings();
            
            // binds
        }
    }
}