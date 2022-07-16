using Gameplay.Entities.Player;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Gameplay.Entities
{
    public class Enemy : Entity
    {
        [FoldoutGroup("Enemy Settings")]
        [SerializeField] private float maxWalkSpeed = 1.25f;
        [FoldoutGroup("Enemy Settings")]
        [SerializeField] private float accelSpeed = 12f;
        [FoldoutGroup("Enemy Settings")]
        [SerializeField] private int damage = 1;
        
        private Vector2 _velocity;

        private void Update()
        {
            // walk towards player
            Vector2 pos = transform.position.WorldToPlane();
            Vector2 playerPos = _player.transform.position.WorldToPlane();
            Vector2 walkDir = (playerPos - pos).normalized;
            Vector2 targetVelocity = walkDir * maxWalkSpeed;
            _velocity = Vector2.MoveTowards(_velocity, targetVelocity, accelSpeed * Time.deltaTime);

            Vector3 worldVelocity = _velocity.PlaneToWorld();
            transform.Translate(worldVelocity * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player == null) return;
            
            player.TakeDamage(damage);
        }
    }
}