using Entities.PlayerScripts;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSettings = Management.Inputs.InputSettings;

namespace Game.Inputs
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;
        private GameManager _gameManager;
        
        private InputSettings _settings;
        private Player _player;
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
        
        private void Start()
        {
            // game manager
            _gameManager = GameManager.Instance;
            if (_gameManager == null) Debug.LogWarning("No game manager in scene.");
            
            // camera
            _camera = Camera.main;
            if (_camera == null) Debug.LogWarning("No main camera in scene.");

            SetPlayerBinds();
        }

        private void SetPlayerBinds()
        {
            _player = Player.Instance;
            if (_player == null) Debug.LogWarning("No player on gamemanager.");
            
        #region movement
            // move
            _settings.Movement.Move.performed += ctx => _player.SetMoveInput(ctx.ReadValue<Vector2>());
            _settings.Movement.Move.canceled += _ => _player.SetMoveInput(Vector2.zero);
            // aim (mouse or controller)
            _settings.Movement.MouseAim.performed += ctx => Aim(ctx, false);
            _settings.Movement.MouseAim.canceled += ctx => Aim(ctx, false);
            _settings.Movement.ControllerAim.performed += ctx => Aim(ctx, true);
            _settings.Movement.ControllerAim.canceled += ctx => Aim(ctx, true);
        #endregion

        #region combat
            // primary ability
            _settings.Abilities.PrimaryAbility.performed += ctx => _player.SetPrimaryInput(ctx.ReadValueAsButton());
            _settings.Abilities.PrimaryAbility.canceled += _ => _player.SetPrimaryInput(false);
            // secondary ability
            _settings.Abilities.SecondaryAbility.performed += ctx => _player.SetSecondaryInput(ctx.ReadValueAsButton());
            _settings.Abilities.SecondaryAbility.canceled += _ => _player.SetSecondaryInput(false);
        #endregion
        }

        private void Aim(InputAction.CallbackContext ctx, bool controller)
        {
            // todo: get scheme and check instead of hardcoding
            // InputControlScheme kbm = _settings.KBMScheme;
            
            Vector2 aimInput = ctx.ReadValue<Vector2>();
            _player.SetAimInput(aimInput, controller);
        }
    }
}