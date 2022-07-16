using Tools;
using UnityEngine;

namespace Gameplay
{
    public partial class Player2D : MonoBehaviour // main
    {
        [SerializeField] private float turnSpeed; // in angles per second
        [SerializeField] private float maxSpeed = 5f;
        [SerializeField] private float walkAccelSpeed = 65f;
        [SerializeField] private float stopBonus = 3f;

        private Transform _transform;
        private Animator _animator;
        private Vector2 _velocity; // on the topdown plane view
    
        // player input, set in partial class "Player2D.Input.cs"
        private Vector2 _moveInput;
        private Vector2 _lookInput; // moveInput, but not affected by letting go of input
        private Vector2 _aimInput;

        private void Start()
        {
            _transform = transform;
            _animator = GetComponentInChildren<Animator>();
            _velocity = Vector2.zero;
            _lookInput = Vector2.down;
        }

        private void Update() // set visuals and use input here
        {
            UpdateMovement();
        }

    }
}
