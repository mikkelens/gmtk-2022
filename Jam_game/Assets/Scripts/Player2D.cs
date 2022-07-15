using System;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float accelSpeed = 65f;
    [SerializeField] private float stopAccelFactor = 3f;

    Transform _transform;
    private Vector2 _moveInput;
    private Vector2 _velocity; // on the topdown plane view
    
    private void Start()
    {
        _transform = transform;
        _velocity = Vector2.zero;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // make player walk
        Vector3 worldVelocity = _velocity.PlaneToWorld();
        _transform.Translate(worldVelocity * Time.fixedDeltaTime, Space.World);
    }
}
