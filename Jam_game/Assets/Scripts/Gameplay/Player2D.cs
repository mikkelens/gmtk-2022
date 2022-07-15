using Tools;
using UnityEngine;

namespace Gameplay
{
    public partial class Player2D : MonoBehaviour
    {
        [SerializeField] private float turnSpeed = 10; // in angles per second
        [SerializeField] private float maxSpeed = 5f;
        [SerializeField] private float walkAccelSpeed = 65f;
        [SerializeField] private float stopBonus = 3f;

        Transform _transform;
        private Vector2 _velocity; // on the topdown plane view
    
        // player input, set in partial class "Player2D.Input.cs"
        private float _lastMoveInputChangeTime; // time of last change of move input, for rotating player in a realistic way
        private Vector2 _moveInput;
        private Vector2 _nonZeroMoveInput; // moveInput, but not affected by letting go of input
        private Vector2 _aimInput;

        
        private void Start()
        {
            _transform = transform;
            _velocity = Vector2.zero;
        }

        private void Update() // set visuals and use input here
        {
            // look at input direction
            Vector3 targetLook = _nonZeroMoveInput.PlaneToWorld().normalized;
            if (_transform.forward != targetLook)
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
        }

        private void TurnTowardsWorldDirection(Vector3 direction)
        {
            Quaternion rotation = _transform.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.Lerp(rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
}
