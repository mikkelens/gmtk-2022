using Gameplay.Stats.DataTypes;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Gameplay.Entities.Base
{
    [Tooltip("Movable Entity: This entity can move around on its own. Think minecraft villager or boat.")]
    [RequireComponent(typeof(Rigidbody))]
    public class MovingEntity : Entity
    {
        [FoldoutGroup(QuirkCategory)]
        [SerializeField] protected bool freezingAffectsRotation = true;
        [FoldoutGroup(QuirkCategory)]
        [SerializeField] protected bool stoppingAffectsRotation;
        [FoldoutGroup(QuirkCategory)]
        [SerializeField] protected float maxTurnSpeed;
        [FoldoutGroup(QuirkCategory)]
        [SerializeField] protected AnimationCurve turnSpeedCurve;
        
        [FoldoutGroup(StatCategory)]
        [SerializeField] protected FloatStat maxSpeed;
        [FoldoutGroup(StatCategory)]
        [SerializeField] protected FloatStat walkAccelSpeed;
        [FoldoutGroup(StatCategory)]
        [SerializeField] protected FloatStat maxStoppingBonus;
        
        [ButtonGroup("FreezeButtons")]
        [HideIf("@IsFrozen")]
        [Button("Freeze")] public void FreezeEntity() => IsFrozen = true;
        [ButtonGroup("FreezeButtons")]
        [HideIf("@!IsFrozen")]
        [Button("Unfreeze")] public void UnfreezeEntity() => IsFrozen = false;

        private Rigidbody _rb;
        
        protected bool Stopping;
        protected bool IsFrozen;
        protected Vector2 PreviousLookDirection;
        
        // [ShowInInspector] // for debugging
        private Vector2 _velocity; // on the topdown plane view


        protected virtual bool CanMove => !IsFrozen && !Stopping;
        protected virtual bool CanRotate => (!IsFrozen || !freezingAffectsRotation) && (!Stopping || !stoppingAffectsRotation);

        protected override void Start()
        {
            base.Start();
            _rb = GetComponent<Rigidbody>();
            if (_rb == null) Debug.LogError($"No rigidbody on movable entity {name}.");
                
            Stopping = false;
            // PreviousLookDirection = Vector2.down; // looking down by default
        }

        protected override void EntityUpdate()
        {
            base.EntityUpdate();
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            // Look towards goal in some way
            TurnTowardsLookDirection(GetLookDirection());

            // Update velocity from goal/player/input
            // Debug.Log($"MyStats maxSpeed: {myStats.maxSpeed}");
            Vector2 targetVelocity = CanMove ? GetMoveDirection() * maxSpeed : Vector2.zero;
            _velocity = Vector2.MoveTowards(_rb.velocity.WorldToPlane(), targetVelocity, GetAcceleration() * Time.deltaTime);
            _rb.velocity = _velocity.PlaneToWorld();
            // MoveByWorldVelocity(Velocity.PlaneToWorld());
            
            bool isMoving = _velocity.magnitude > 0.0f;
            Animator.SetBool("Walking", isMoving);
        }

        protected virtual Vector2 GetLookDirection()
        {
            return GetTargetMoveDirection();
        }
        protected virtual Vector2 GetMoveDirection()
        {
            return GetTargetMoveDirection();
        }
        protected virtual Vector2 GetTargetMoveDirection()
        {
            return Vector2.down; // default move direction in case nothing is used..?
        }


        protected virtual void TurnTowardsLookDirection(Vector2 targetDirection)
        {
            if (!CanRotate) return;
            if (targetDirection.magnitude == 0f) return;
            Quaternion currentRotation = Transform.rotation; // rotations are in world space
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection.PlaneToWorld());
            float turnSpeed = GetTurnSpeed(currentRotation, targetRotation);
            Quaternion newRotation = Quaternion.RotateTowards(currentRotation, targetRotation, turnSpeed * Time.deltaTime);
            Transform.rotation = newRotation;
            PreviousLookDirection = targetDirection;
        }
        protected virtual float GetTurnSpeed(Quaternion currentRotation, Quaternion targetRotation)
        {
            float tFromAngle = Quaternion.Angle(currentRotation, targetRotation) / 180f; // 180 is max possible angle
            float dynamicTurnSpeed = turnSpeedCurve.Evaluate(1f - tFromAngle) * 180;
            return dynamicTurnSpeed * maxTurnSpeed;
        }
        
        private float GetAcceleration()
        {
            float accel = walkAccelSpeed;
            if (Stopping) return accel * maxStoppingBonus;
            
            float likeness = Vector2.Dot(_velocity.normalized, GetTargetMoveDirection().normalized);
            float stopFactor = (1 - likeness) / 2f; // from (-1 to 1) to (0 to 1), and in reverse
            float stopBonus = maxStoppingBonus * stopFactor;
            return accel * (1 + stopBonus);
        }

        protected override void ApplyKnockback(Vector2 force)
        {
            base.ApplyKnockback(force);
            _rb.velocity += force.PlaneToWorld();
        }
    }
}
