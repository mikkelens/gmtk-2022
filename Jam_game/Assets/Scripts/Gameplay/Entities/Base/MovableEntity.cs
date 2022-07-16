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

        protected bool Stopping;
        protected bool IsFrozen;
        protected Vector2 PreviousLookDirection;
        protected Vector2 Velocity; // on the topdown plane view
        
        protected bool CanMove => !IsFrozen && !Stopping;
        protected bool CanRotate => !IsFrozen || !freezeAffectsRotation;

        public override void Start()
        {
            base.Start();
            Stopping = false;
            PreviousLookDirection = Vector2.down; // looking down by default
        }

        public override void Update()
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
            Velocity = Vector2.MoveTowards(Velocity, targetVelocity, GetAcceleration() * Time.deltaTime);;
            MoveByWorldVelocity(Velocity.PlaneToWorld());
            
            bool isMoving = Velocity.magnitude > 0.01f;
            Animator.SetBool("Walking", isMoving);
        }
        
        public virtual Vector2 GetTargetLookDirection()
        {
            return GetTargetMoveDirection();
        }
        public virtual Vector2 GetTargetMoveDirection()
        {
            return Vector2.zero; // default move direction in case nothing is used..?
        }
        
        public virtual void TurnTowardsLookDirection()
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

        public virtual float GetTurnSpeed(Quaternion currentRotation, Quaternion targetRotation)
        {
            float tFromAngle = Quaternion.Angle(currentRotation, targetRotation) / 180f; // 180 is max possible angle
            float dynamicTurnSpeed = turnSpeedCurve.Evaluate(1f - tFromAngle) * 180;
            return dynamicTurnSpeed * maxTurnSpeed;
        }
        
        public virtual float GetAcceleration()
        {
            float likeness = Vector2.Dot(Velocity.normalized, GetTargetMoveDirection().normalized);
            float stopFactor = (1f - likeness) / 2f; // from (-1 to 1) to (0 to 1), and in reverse
            float accel = walkAccelSpeed;
            accel += walkAccelSpeed * stopBonus * stopFactor;
            return accel;
        }
        
        public virtual void MoveByWorldVelocity(Vector3 worldVelocity)
        {
            Transform.Translate(worldVelocity * Time.deltaTime, Space.World); // space.world is important, else it is relative to player rotation
        }

        protected override void Knockback(Vector2 force)
        {
            base.Knockback(force);

            Velocity += force;
        }
    }
}