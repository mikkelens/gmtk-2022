using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Stats.Stat.Variants;
using Tools;
using UnityEngine;

namespace Entities.Base
{
    [Tooltip("Movable Entity: This entity can move around on its own. Think minecraft villager or boat.")]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class MovingEntity : Entity
    {
        [FoldoutGroup(QuirkCategory)]
        [PropertyOrder(-5)]
        [UsedImplicitly] private string FreezeButtonText => isFrozen ? "Unfreeze" : "Freeze";
        [Button("$FreezeButtonText")] private void ToggleFreezeEntity() => isFrozen = !isFrozen;
        [FoldoutGroup(QuirkCategory)]
        [SerializeField] protected bool freezingAffectsRotation = true;
        [FoldoutGroup(QuirkCategory)]
        [SerializeField] protected bool stoppingAffectsRotation;
        [FoldoutGroup(QuirkCategory)]
        [SerializeField] protected AnimationCurve turnSpeedCurve = new AnimationCurve();
        
        [FoldoutGroup(StatCategory)]
        [SerializeField] protected FloatStat maxSpeed;
        [FoldoutGroup(StatCategory)]
        [SerializeField] protected FloatStat walkAccelSpeed;
        [FoldoutGroup(StatCategory)]
        [SerializeField] protected FloatStat maxTurnSpeed;
        [FoldoutGroup(StatCategory)]
        [SerializeField] protected FloatStat maxStoppingBonus;

        [FoldoutGroup(StatCategory)]
        [SerializeField] protected FloatStat accelerationWhenStunned = 5f;

        [FoldoutGroup(StatCategory)]
        [SerializeField] protected BoolStat stunned; // cannot accelerate

        private Rigidbody _rb;

        private bool isFrozen;
        protected bool stopping;
        protected Vector2 previousLookDirection;
        
        // [ShowInInspector] // for debugging
        private Vector2 _velocity; // on the topdown plane view
        private static readonly int Walking = Animator.StringToHash("Walking");


        protected virtual bool CanMove => !isFrozen && !stopping;
        private bool CanRotate => (!isFrozen || !freezingAffectsRotation) && (!stopping || !stoppingAffectsRotation);

        protected override void Start()
        {
            base.Start();
            _rb = GetComponent<Rigidbody>();
            if (_rb == null) Debug.LogError($"No rigidbody on movable entity {name}.");
                
            stopping = false;
            // PreviousLookDirection = Vector2.down; // looking down by default
        }

        protected override void AliveUpdate()
        {
            base.AliveUpdate();
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            // Look towards goal in some way
            TurnTowardsLookDirection(GetLookDirection());

            // Update velocity from goal/player/input
            Vector2 targetVelocity = CanMove ? GetMoveDirection() * maxSpeed : Vector2.zero;
            _velocity = Vector2.MoveTowards(_rb.velocity.WorldToPlane(), targetVelocity, GetAcceleration() * Time.deltaTime);
            _rb.velocity = _velocity.PlaneToWorld();

            bool isMoving = _velocity.magnitude > 0.0f;
            if (Animator != null) Animator.SetBool(Walking, isMoving);
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
            previousLookDirection = targetDirection;
        }
        protected virtual float GetTurnSpeed(Quaternion currentRotation, Quaternion targetRotation)
        {
            float tFromAngle = Quaternion.Angle(currentRotation, targetRotation) / 180f; // 180 is max possible angle
            float dynamicTurnSpeed = turnSpeedCurve.Evaluate(1f - tFromAngle) * 180;
            return dynamicTurnSpeed * maxTurnSpeed;
        }
        
        private float GetAcceleration()
        {
            if (stunned) return accelerationWhenStunned;
            float accel = walkAccelSpeed;
            if (stopping) return accel * maxStoppingBonus;

            float likeness = Vector2.Dot(_velocity.normalized, GetTargetMoveDirection().normalized);
            float stopFactor = (1 - likeness) / 2f; // from (-1 to 1) to (0 to 1), and in reverse
            float stopBonus = maxStoppingBonus * stopFactor;
            accel *= 1 + stopBonus;
            return accel;
        }

        public override void ApplyKnockback(Vector2 force)
        {
            base.ApplyKnockback(force);
            _rb.velocity += force.PlaneToWorld();
        }
    }
}