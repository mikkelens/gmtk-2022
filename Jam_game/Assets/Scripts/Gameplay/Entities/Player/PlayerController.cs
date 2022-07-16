using UnityEngine;

namespace Gameplay.Entities.Player
{
    public partial class PlayerController : Entity // main
    {
        [SerializeField] private float maxTurnSpeed; // in angles per second
        [SerializeField] private AnimationCurve turnSpeedCurve; // changes the turn speed dynamically
        [SerializeField] private float maxSpeed = 5f;
        [SerializeField] private float walkAccelSpeed = 65f;
        [SerializeField] private float stopBonus = 3f;

        private Transform _transform;
        private Vector2 _velocity; // on the topdown plane view
    
        // player input, set in partial class "Player2D.Input.cs"
        private Vector2 _moveInput;
        private Vector2 _lookInput; // moveInput, but not affected by letting go of input
        private Vector2 _aimInput;

        public override void Start()
        {
            base.Start(); // entity
            
            _transform = transform;
            _velocity = Vector2.zero;
            _lookInput = Vector2.down;
        }

        private void Update() // set visuals and use input here
        {
            UpdateMovement();
        }

        public override void KillThis()
        {
            Debug.Log("Player was killed!");
            _animator.SetTrigger("Death");
        }
    }
}
