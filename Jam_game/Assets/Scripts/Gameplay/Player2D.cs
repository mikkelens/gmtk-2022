using Tools;
using UnityEngine;

namespace Gameplay
{
    public class Player2D : MonoBehaviour
    {
        [SerializeField] private float maxSpeed = 5f;
        [SerializeField] private float walkAccelSpeed = 65f;
        [SerializeField] private float stopBonus = 3f;

        Transform _transform;
        private Vector2 _moveInput;
        private Vector2 _velocity; // on the topdown plane view
    
        private void Start()
        {
            _transform = transform;
            _velocity = Vector2.zero;
        }

        private void Update() // set visuals and use input here
        {
            // look at input direction
            TurnTowardsWorldDirection(_moveInput.PlaneToWorld().normalized);
        }

        private void FixedUpdate() // use velocity and physics here
        {
            // change velocity
            Vector2 target = _moveInput.normalized * maxSpeed;
        
            float stopFactor = Vector2.Dot(_velocity, target);
            Debug.Log($"Stopfactor: {stopFactor}");
            float accel = walkAccelSpeed;
            accel += walkAccelSpeed * stopBonus * stopFactor;
        
            _velocity = Vector2.MoveTowards(_velocity, target, accel * Time.fixedDeltaTime);
        
            // use velocity
            Vector3 worldVelocity = _velocity.PlaneToWorld();
            _transform.Translate(worldVelocity * Time.fixedDeltaTime, Space.World);
        
        
        }
    
        private void TurnTowardsWorldDirection(Vector3 direction)
        {
            float angle = Vector3.Angle(_transform.forward, direction);
            _transform.Rotate(_transform.up, angle);
        }
    }
}
