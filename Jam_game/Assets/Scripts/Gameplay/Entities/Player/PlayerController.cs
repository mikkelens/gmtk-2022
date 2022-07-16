using UnityEngine;

namespace Gameplay.Entities.Player
{
    public partial class PlayerController : CombatEntity // main
    {
        [SerializeField] private float aimTurnSpeedBonus = 2f;
        
        // player input is set in partial class "Player2D.Input.cs"

        // movement is decided by input
        public override Vector2 GetTargetLookDirection()
        {
            if (IsAiming) return _aimInput;
            if (IsMoving) return base.GetTargetLookDirection(); // updates look direction
            return PreviousLookDirection; // use previous look (aim) direction
        }
        public override Vector2 GetTargetMoveDirection()
        {
            return _moveInput;
        }

        public override float GetTurnSpeed(Quaternion currentRotation, Quaternion targetRotation)
        {
            float turnSpeed = base.GetTurnSpeed(currentRotation, targetRotation);
            if (IsAiming) turnSpeed += turnSpeed * aimTurnSpeedBonus;
            return turnSpeed;
        }

        public override void KillThis()
        {
            base.KillThis();
            
            Debug.Log("Player was killed!");
        }
    }
}
