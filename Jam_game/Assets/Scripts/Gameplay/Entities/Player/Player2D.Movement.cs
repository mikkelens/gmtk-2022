using Tools;
using UnityEngine;

namespace Gameplay.Entities.Player
{
    public partial class PlayerController // movement
    {
        private void UpdateMovement()
        {
            // look at input direction
            Vector3 targetLook = _lookInput.PlaneToWorld();
            TurnTowardsWorldDirection(targetLook);

            // change velocity
            Vector2 target = _moveInput.normalized * maxSpeed;

            float likeness = Vector2.Dot(_velocity.normalized, target.normalized);
            float stopFactor = (1f - likeness) / 2f; // from (-1 to 1) to (0 to 1), and in reverse
            float accel = walkAccelSpeed;
            accel += walkAccelSpeed * stopBonus * stopFactor;

            _velocity = Vector2.MoveTowards(_velocity, target, accel * Time.deltaTime);

            // use velocity
            Vector3 worldVelocity = _velocity.PlaneToWorld();
            _transform.Translate(worldVelocity * Time.deltaTime, Space.World);

            bool isMoving = _velocity.magnitude > 0.01f;
            _animator.SetBool("Walking", isMoving);
        }

        private void TurnTowardsWorldDirection(Vector3 direction)
        {
            Quaternion currentRotation = _transform.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            float tFromAngle = Quaternion.Angle(currentRotation, targetRotation) / 180f; // 180 is max possible angle
            float dynamicTurnSpeed = turnSpeedCurve.Evaluate(1f - tFromAngle) * maxTurnSpeed * 180f;
            Quaternion newRotation = Quaternion.RotateTowards(currentRotation, targetRotation, dynamicTurnSpeed * Time.deltaTime);
            _transform.rotation = newRotation;
        }
    }
}