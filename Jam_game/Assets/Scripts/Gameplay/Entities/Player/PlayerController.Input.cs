using UnityEngine;

namespace Gameplay.Entities.Player
{
    public partial class PlayerController // input receiving
    {
        private Vector2 _moveInput;
        private bool IsMoving => _moveInput.magnitude > 0.0f;
        
        private bool _holdingThrow;
        private bool _holdingMelee;
        private bool _lastAimWasController;
        private Vector2 _aimInput;
        private bool AimCounts => _lastAimWasController || _holdingThrow;
        private bool IsAiming => _aimInput.magnitude > 0.0f && AimCounts;
        
        
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

        public void SetAimInput(Vector2 input, bool fromController = false)
        {
            _aimInput = input;
            _lastAimWasController = fromController;
            // Debug.Log($"Aim input: {input}");
        }

    }
}