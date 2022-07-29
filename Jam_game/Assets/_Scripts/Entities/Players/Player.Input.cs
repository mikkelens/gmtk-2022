using Tools;
using UnityEngine;

namespace Entities.Players
{
    public partial class Player // input receiving
    {
        private Vector2 _moveInput;
        private bool _holdingThrow;
        private bool _holdingMelee;
        private bool _lastAimWasController;
        
        private bool IsMoving => _moveInput.magnitude > 0.0f;
        private bool IsAiming => (UpdatedLookDirection != Vector2.zero && _lastAimWasController) || _holdingThrow;

        protected override bool WantsToAttack => _holdingMelee;

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