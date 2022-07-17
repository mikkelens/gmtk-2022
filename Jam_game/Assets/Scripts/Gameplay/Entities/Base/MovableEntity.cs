using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Gameplay.Entities.Base
{
    [Tooltip("Movable Entity: This entity can move around on its own. Think minecraft villager or boat.")]
    [RequireComponent(typeof(Rigidbody))]
    public class MovableEntity : Entity
    {
        // moving
        [SerializeField] protected float maxSpeed = 5f;
        [SerializeField] protected float walkAccelSpeed = 65f;
        [SerializeField] protected float stopBonus = 3f;
        
        // turning
        [SerializeField] protected float maxTurnSpeed; // in angles per second
        [SerializeField] protected AnimationCurve turnSpeedCurve; // changes the turn speed dynamically

        // misc
        [SerializeField] protected bool freezeAffectsRotation;

        [ButtonGroup("FreezeButtons")]
        [HideIf("@IsFrozen")]
        [Button("Freeze")] public void FreezeEntity() => IsFrozen = true;
        [ButtonGroup("FreezeButtons")]
        [HideIf("@!IsFrozen")]
        [Button("Unfreeze")] public void UnfreezeEntity() => IsFrozen = false;

        protected Rigidbody Rb;
        
        protected bool Stopping;
        protected bool IsFrozen;
        protected Vector2 PreviousLookDirection;
        protected Vector2 Velocity; // on the topdown plane view
        
        protected virtual bool CanMove => !IsFrozen && !Stopping;
        protected virtual bool CanRotate => !IsFrozen || !freezeAffectsRotation;

        protected override void Start()
        {
            base.Start();
            Rb = GetComponent<Rigidbody>();
            if (Rb == null) Debug.LogError($"No rigidbody on movable entity {name}.");
                
            Stopping = false;
            PreviousLookDirection = Vector2.down; // looking down by default
        }

        protected override void Update()
        {
            base.Update();
            UpdateMovement();
        }

        public virtual void UpdateMovement()
        {
            // Look towards goal in some way
            TurnTowardsLookDirection();

            // Update velocity from goal/player/input
            Vector2 targetVelocity = CanMove ? GetTargetMoveDirection() * maxSpeed : Vector2.zero;
            Velocity = Vector2.MoveTowards(Rb.velocity.WorldToPlane(), targetVelocity, GetAcceleration() * Time.deltaTime);
            Rb.velocity = Velocity.PlaneToWorld();
            // MoveByWorldVelocity(Velocity.PlaneToWorld());
            
            bool isMoving = Velocity.magnitude > 0.0f;
            Animator.SetBool("Walking", isMoving);
        }

        protected virtual Vector2 GetTargetLookDirection()
        {
            return GetTargetMoveDirection();
        }
        protected virtual Vector2 GetTargetMoveDirection()
        {
            return Vector2.zero; // default move direction in case nothing is used..?
        }

        private void TurnTowardsLookDirection()
        {
            if (!CanRotate) return;
            Vector2 direction = GetTargetLookDirection();
            if (direction.magnitude == 0f) return;
            Quaternion currentRotation = Transform.rotation; // rotations are in world space
            Quaternion targetRotation = Quaternion.LookRotation(direction.PlaneToWorld());
            float turnSpeed = GetTurnSpeed(currentRotation, targetRotation);
            Quaternion newRotation = Quaternion.RotateTowards(currentRotation, targetRotation, turnSpeed * Time.deltaTime);
            Transform.rotation = newRotation;
            PreviousLookDirection = direction;
        }
        protected virtual float GetTurnSpeed(Quaternion currentRotation, Quaternion targetRotation)
        {
            float tFromAngle = Quaternion.Angle(currentRotation, targetRotation) / 180f; // 180 is max possible angle
            float dynamicTurnSpeed = turnSpeedCurve.Evaluate(1f - tFromAngle) * 180;
            return dynamicTurnSpeed * maxTurnSpeed;
        }
        private float GetAcceleration()
        {
            float likeness = Vector2.Dot(Velocity.normalized, GetTargetMoveDirection().normalized);
            float stopFactor = (1f - likeness) / 2f; // from (-1 to 1) to (0 to 1), and in reverse
            float accel = walkAccelSpeed;
            accel += walkAccelSpeed * stopBonus * stopFactor;
            return accel;
        }

        protected override void ApplyKnockback(Vector2 force)
        {
            base.ApplyKnockback(force);
            Rb.velocity += force.PlaneToWorld();
        }
    }
}