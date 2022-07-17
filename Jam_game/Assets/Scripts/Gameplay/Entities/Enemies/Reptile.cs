using Tools;
using UnityEngine;

namespace Gameplay.Entities.Enemies
{
    public class Reptile : Enemy
    {
        protected override void TurnTowardsLookDirection(Vector2 targetDirection)
        {
            Vector2 forwards = Transform.forward.WorldToPlane();
            float angle = Vector2.SignedAngle(forwards, targetDirection);
            Animator.SetBool("Walking Left", angle > 1);
            Animator.SetBool("Walking Right", angle < -1);
            // make head turn
            base.TurnTowardsLookDirection(targetDirection);
        }
    }
}