using Gameplay.Entities.Base;
using UnityEngine;

namespace Gameplay.Entities.PlayerScripts
{
    [Tooltip("Player: This is the player script. It also derives from entity scripts.")]
    public partial class Player : CombatEntity // main
    {
        public static Player Instance;
        
        [SerializeField] private float aimTurnSpeedBonus = 2f;

        private void Awake()
        {
            Instance = this;
        }

        // movement is decided by input set in "Player.Input.cs"
        public override Vector2 GetTargetLookDirection()
        {
            if (IsAiming) return AimInput;
            if (IsMoving) return base.GetTargetLookDirection(); // updates look direction
            return PreviousLookDirection; // use previous look (aim) direction
        }
        public override Vector2 GetTargetMoveDirection()
        {
            return _moveInput;
        }

        public override void StartAttack()
        {
            TryMelee();
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
