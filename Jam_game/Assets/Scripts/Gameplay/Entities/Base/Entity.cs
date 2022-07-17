using System;
using Management;
using UnityEngine;

namespace Gameplay.Entities.Base
{
    [Tooltip("Entity: Hittable thing. think minecraft armor stand.")]
    [RequireComponent(typeof(Collider))]
    [SelectionBase]
    public class Entity : MonoBehaviour
    {
        [SerializeField] protected Stats startingStats;

        public Stats Stats { get; set; }

        // outside components
        protected GameManager Manager;

        // components with this entity
        protected Transform Transform;
        protected Animator Animator;

        // health status
        private int _health;

        private void Awake()
        {
            Stats = startingStats;
            Transform = transform;
            Animator = GetComponentInChildren<Animator>();
            if (Animator == null) Debug.LogWarning($"No animator component found on entity '{name}'");
        }

        protected virtual void Start()
        {
            Manager = GameManager.Instance;
        }
        protected virtual void Update()
        {
            // idk base thing here
        }

        public void TakeHit(int damage, Vector2 knockbackForce) // Main way of getting hit
        {
            ApplyDamage(damage);
            ApplyKnockback(knockbackForce);
        }
        private void ApplyDamage(int damage)
        {
            if (Stats.godMode) return;
            _health -= damage;
            if (_health <= 0) KillThis();

            // todo: damage animation etc?
        }

        protected virtual void ApplyKnockback(Vector2 force)
        {
            // idk
        }

        public virtual void KillThis()
        {
            Debug.Log($"Entity '{name}' was killed.");
            Animator.SetTrigger("Death");
        }

        public void HealToFull()
        {
            SetHealth(Stats.maxHealth);
        }
        private void SetHealth(int health)
        {
            _health = health;
        }
        private void ApplyHealing(int healing)
        {
            _health += healing;
        }
    }
}
