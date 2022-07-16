using Gameplay.Entities.Player;
using Management;
using UnityEngine;

namespace Gameplay.Entities
{
    // Hittable thing. think minecraft boat or mob.
    public class Entity : MonoBehaviour
    {
        [SerializeField] private int startingHealth = 10;
        
        protected GameManager _manager;
        protected PlayerController _player;
        protected Animator _animator;

        
        private int _health;
        
        public virtual void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _manager = GameManager.Instance;
            _player = _manager.player;

            _health = startingHealth;
        }

        public virtual void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                KillThis();
            }
            // todo: damage animation etc?
        }

        public virtual void KillThis()
        {
            Debug.Log($"Entity '{name}' was killed.");
            Destroy(gameObject);
        }
    }
}
