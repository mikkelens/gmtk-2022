using Level;
using UnityEngine;

namespace Entities.PlayerScripts
{
    public partial class Player // input receiving
    {
        private Vector2 _moveInput;
        private bool _holdingMelee;
        private bool _holdingThrow;
        private bool _lastAimWasController;
        
        private bool IsMoving => _moveInput.magnitude > 0.0f;
        private bool IsAiming => (UpdatedLookDirection != Vector2.zero && _lastAimWasController) || _holdingThrow;

        private bool _hasPressedMeleeSinceLastRead;
        protected bool ReadMelee
        {
            get
            {
                if (!_hasPressedMeleeSinceLastRead) return false;
                _hasPressedMeleeSinceLastRead = false;
                return true;
            }
        }
        protected override bool WantsToUseAbility => ReadMelee;

        private bool _aimIsDirty;
        private Vector2 _rawAimInput;
        private Vector2 _lookDirection;
        private Vector2 UpdatedLookDirection
        {
            get
            {
                if (!_aimIsDirty) return _lookDirection;
                _aimIsDirty = false;
                if (_lastAimWasController) return _lookDirection = _rawAimInput;
                Vector3 worldPosition = Transform.position;
                Vector2 offset = worldPosition.PositionToScreenPoint();
                return _lookDirection = (_rawAimInput - offset).ScreenToViewportPoint(); // mouse
            }
        }
        
        public void SetMeleeInput(bool pressingMelee)
        {
            if (pressingMelee) _hasPressedMeleeSinceLastRead = true;
            _holdingMelee = pressingMelee;
        }
        public void SetThrowInput(bool holdingThrow)
        {
            _holdingThrow = holdingThrow;
        }
        public void SetMoveInput(Vector2 input)
        {
            _moveInput = input;
            // Debug.Log($"Move input: {input}");
        }

        public void SetAimInput(Vector2 input, bool fromController)
        {
            _rawAimInput = input;
            _lastAimWasController = fromController;
            _aimIsDirty = true;
        }
    }
}