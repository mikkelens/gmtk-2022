using Tools;
using UnityEngine;

namespace Gameplay.Entities.PlayerScripts
{
    public sealed partial class Player // input receiving
    {
        private Vector2 _moveInput;
        private bool IsMoving => _moveInput.magnitude > 0.0f;
        
        private bool _holdingThrow;
        private bool _holdingMelee;
        private bool _lastAimWasController;
        
        private bool AimCounts => _lastAimWasController || _holdingThrow;
        private bool IsAiming => AimInput.magnitude > 0.0f && AimCounts;

        protected override bool WantsToAttack => _holdingMelee;

        private Vector2 _rawAimInput;
        private Vector2 AimInput
        {
            get
            {
                Vector2 aimInput = _rawAimInput;
                if (_lastAimWasController) return aimInput; // controller

                // aimInput = aimInput.ScreenToCenter();
                Vector2 offset = Transform.position.PositionToScreenPoint();
                aimInput = (aimInput - offset).ScreenToViewportPoint();
                return aimInput; // mouse
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
            // Debug.Log($"Aim input: {input}");
        }

    }
}