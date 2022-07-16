using System;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Gameplay.Entities
{
    // This entity can move around on its own. Think minecraft villager or boat.
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
        [SerializeField] protected bool freezeAffectsRotation = false;

        [Button]
        public void FreezeEntity(bool freeze)
        {
            IsFrozen = freeze;
        }
        
        protected bool IsFrozen;
        protected Vector2 PreviousLookDirection;
        protected Vector2 Velocity; // on the topdown plane view

        public override void Start()
        {
            base.Start();
            
            PreviousLookDirection = Vector2.down;
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
            Vector2 targetVelocity = IsFrozen ? Vector2.zero : GetTargetMoveDirection() * maxSpeed;;
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
            if (IsFrozen && freezeAffectsRotation) return;
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