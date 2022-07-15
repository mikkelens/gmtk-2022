using System;
using System.Diagnostics.CodeAnalysis;
using Gameplay;
using UnityEngine;

namespace Input
{
    public class InputManager : MonoBehaviour
    {
        // testing
        public bool useControllerAim = false;
        
        public static InputManager Instance;
        private GameManager _gameManager;
        
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

        [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
        private void Start()
        {
            _settings = new InputSettings();

            // game manager
            _gameManager = GameManager.Instance;
            if (_gameManager == null) throw new UnityException("No game manager in scene.");

            SetPlayerBinds();
        }

        private void SetPlayerBinds()
        {
            Player2D player = _gameManager.player;
            if (player == null) throw new UnityException("No player on gamemanager.");
            // movement //
            // move
            _settings.Movement.Move.performed += ctx => player.SetMoveInput(ctx.ReadValue<Vector2>());
            _settings.Movement.Move.canceled += ctx => player.SetMoveInput(Vector2.zero);
            
            // combat //
            // aim
            if (useControllerAim)
            {
                _settings.Combat.MouseAim.performed += ctx => player.SetAimInput(ctx.ReadValue<Vector2>());
            }
            else
            {
                _settings.Combat.ControllerAim.performed += ctx => player.SetAimInput(ctx.ReadValue<Vector2>());
            }
        }
    }
}