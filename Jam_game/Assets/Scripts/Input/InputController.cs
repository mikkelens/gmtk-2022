using System;
using System.Diagnostics.CodeAnalysis;
using Gameplay.Entities.Player;
using Management;
using Tools;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private Texture2D cursorTexture;

        private void OnApplicationFocus(bool hasFocus)
        {
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }

        public static InputController Instance;
        private GameManager _gameManager;
        
        private InputSettings _settings;
        private PlayerController _player;
        private Camera _camera;

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
            
            // camera
            _camera = Camera.main;
            if (_camera == null) throw new UnityException("No main camera in scene.");

            SetPlayerBinds();
        }

        private void SetPlayerBinds()
        {
            _player = _gameManager.player;
            if (_player == null) throw new UnityException("No player on gamemanager.");
            
            // movement //
            // move
            _settings.Movement.Move.performed += ctx => _player.SetMoveInput(ctx.ReadValue<Vector2>());
            _settings.Movement.Move.canceled += _ => _player.SetMoveInput(Vector2.zero);
            
            // combat //
            // melee attack
            _settings.Combat.Melee.performed += ctx => _player.SetMeleeInput(ctx.ReadValueAsButton());
            _settings.Combat.Melee.canceled += _ => _player.SetMeleeInput(false);
            // throw attack
            _settings.Combat.Throw.performed += ctx => _player.SetThrowInput(ctx.ReadValueAsButton());
            _settings.Combat.Throw.canceled += _ => _player.SetThrowInput(false);
            // aim (mouse or controller)
            _settings.Combat.MouseAim.performed += ctx => Aim(ctx, false);
            _settings.Combat.MouseAim.canceled += ctx => Aim(ctx, false);
            _settings.Combat.ControllerAim.performed += ctx => Aim(ctx, true);
            _settings.Combat.ControllerAim.canceled += ctx => Aim(ctx, true);
        }

        private void Aim(InputAction.CallbackContext ctx, bool controller)
        {
            // todo: get scheme and check instead of hardcoding
            // Vector2 aimInput = ctx.ReadValue<Vector2>();
            // InputControlScheme kbm = _settings.KBMScheme;
            
            _player.SetAimInput(ctx.ReadValue<Vector2>(), controller);
        }
    }
}