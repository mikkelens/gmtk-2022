using Components;
using UnityEngine;

namespace Entities.PlayerScripts
{
    public partial class Player // input receiving
    {
        
        private Vector2 _moveInput;
        private bool _pressedPrimarySinceUse;
        private bool _holdingPrimary;
        private bool _pressedSecondarySinceUse;
        private bool _holdingSecondary;
        private bool _lastAimWasController;
        
        private bool IsMoving => _moveInput.magnitude > 0.0f;
        private bool IsAiming => (UpdatedLookDirection != Vector2.zero && _lastAimWasController) || _holdingSecondary;

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
        
        public void SetPrimaryInput(bool primary)
        {
            if (primary) _pressedPrimarySinceUse = true;
            _holdingPrimary = primary;
        }
        public void SetSecondaryInput(bool secondary)
        {
            if (secondary) _pressedSecondarySinceUse = true;
            _holdingSecondary = secondary;
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