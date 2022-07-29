using UnityEngine;

namespace Entities.Players
{
    public partial class Player
    {
        protected override Vector2 GetLookDirection()
        {
            if (IsAiming) return UpdatedLookDirection;
            if (IsMoving) return GetTargetMoveDirection(); // updates look direction
            return PreviousLookDirection; // use previous look (aim) direction
        }

        protected override Vector2 GetTargetMoveDirection()
        {
            return _moveInput.normalized;
        }

        protected override float GetTurnSpeed(Quaternion currentRotation, Quaternion targetRotation)
        {
            float turnSpeed = base.GetTurnSpeed(currentRotation, targetRotation);
            if (IsAiming) turnSpeed += turnSpeed * aimTurnSpeedBonus;
            return turnSpeed;
        }
    }
}