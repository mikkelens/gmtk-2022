using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Gameplay.Entities.Enemies
{
    public class Reptile : Enemy
    {
        [Header("Reptile Specific")]
        [FoldoutGroup("Quirks")]
        [SerializeField] protected float angleBeforeHeadTurn;

        protected override void TurnTowardsLookDirection(Vector2 targetDirection)
        {
            Vector2 forwards = Transform.forward.WorldToPlane();
            float angle = Vector2.SignedAngle(forwards, targetDirection);
            Animator.SetBool("Looking Left", angle > angleBeforeHeadTurn);
            Animator.SetBool("Looking Right", angle < -angleBeforeHeadTurn);
            // make head turn
            base.TurnTowardsLookDirection(targetDirection);
        }
    }
}