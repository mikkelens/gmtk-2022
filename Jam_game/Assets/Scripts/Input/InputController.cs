using System.Diagnostics.CodeAnalysis;
using Gameplay.Entities.Player;
using Management;
using UnityEngine;

namespace Input
{
    public class InputController : MonoBehaviour
    {
        // testing
        public bool useControllerAim = false;
        
        public static InputController Instance;
        private GameManager _gameManager;
        
        private InputSettings _settings;

        private void Awake()
        {
            Instance = this;
            
            _settings = new InputSettings();
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
            // game manager
            _gameManager = GameManager.Instance;
            if (_gameManager == null) throw new UnityException("No game manager in scene.");

            SetPlayerBinds();
        }

        private void SetPlayerBinds()
        {
            PlayerController player = _gameManager.player;
            if (player == null) throw new UnityException("No player on gamemanager.");
            // movement //
            // move
            _settings.Movement.Move.performed += ctx => player.SetMoveInput(ctx.ReadValue<Vector2>());
            _settings.Movement.Move.canceled += _ => player.SetMoveInput(Vector2.zero);
            
            // combat //
            // aim
            if (useControllerAim)
            {
                _settings.Combat.MouseAim.performed += ctx => player.SetAimInput(ctx.ReadValue<Vector2>());
                _settings.Combat.MouseAim.canceled += _ => player.SetAimInput(Vector2.zero);
            }
            else
            {
                _settings.Combat.ControllerAim.performed += ctx => player.SetAimInput(ctx.ReadValue<Vector2>());
                _settings.Combat.ControllerAim.canceled += _ => player.SetAimInput(Vector2.zero);
            }
        }
    }
}