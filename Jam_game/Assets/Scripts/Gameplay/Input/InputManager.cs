using Gameplay.Entities.Players;
using Management;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Input
{
    public class InputManager : MonoBehaviour
    {
        [ShowIf("@cursorSprite == null")]
        [SerializeField] private Texture2D cursorTexture;
        [ShowIf("@cursorTexture == null")]
        [SerializeField] private Sprite cursorSprite;

        private void OnApplicationFocus(bool hasFocus)
        {
            Texture2D cursor;
            if (cursorTexture != null)
            {
                cursor = cursorTexture;
            }
            else if (cursorSprite != null)
            {
                cursor = cursorSprite.texture;
            }
            else return;
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }

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
            // InputControlScheme kbm = _settings.KBMScheme;
            
            Vector2 aimInput = ctx.ReadValue<Vector2>();
            _player.SetAimInput(aimInput, controller);
        }
    }
}