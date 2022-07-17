using Tools;
using UnityEngine;

namespace Gameplay.Entities.PlayerScripts
{
    public partial class Player // input receiving
    {
        private Vector2 _moveInput;
        private bool _holdingThrow;
        private bool _holdingMelee;
        private bool _lastAimWasController;
        
        private bool IsMoving => _moveInput.magnitude > 0.0f;
        private bool AimCounts => _lastAimWasController || _holdingThrow;
        private bool IsAiming => /*LookDirection() != Vector2.zero &&*/ AimCounts;

        protected override bool WantsToAttack => _holdingMelee;

        private Vector2 _rawAimInput;
        private Vector2 LookDirection()
        {
            Vector2 aimInput = _rawAimInput;
            if (_lastAimWasController)
            {
                Vector3 worldPosition = Transform.position;
                Vector2 offset = worldPosition.PositionToScreenPoint();
                aimInput = (_rawAimInput - offset).ScreenToViewportPoint();
            }
            return aimInput; // mouse
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
            // Debug.Log($"Aim input: {input}");
        }

    }
}