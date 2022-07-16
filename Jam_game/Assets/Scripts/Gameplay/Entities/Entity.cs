using System;
using Gameplay.Entities.Player;
using Management;
using UnityEngine;

namespace Gameplay.Entities
{
    // Hittable (static) thing. think minecraft armor stand.
    public class Entity : MonoBehaviour
    {
        // base settings
        [SerializeField] protected int startingHealth = 10;
        [SerializeField] private bool godMode = false;
        
        // outside components
        protected GameManager Manager;

        // components with this entity
        protected Transform Transform;
        protected Animator Animator;

        // health status
        protected bool Alive => Health > 0;
        protected int Health;
        
        public virtual void Start()
        {
            Transform = transform;
            Animator = GetComponentInChildren<Animator>();
            
            Manager = GameManager.Instance;
            
            Health = startingHealth;
        }

        public virtual void Update()
        {
            
        }

        public virtual void TakeHit(int damage, Vector2 knockbackForce)
        {
            // damage
            TakeDamage(damage);
            // knockback
            Knockback(knockbackForce);
        }
        
        protected virtual void TakeDamage(int damage)
        {
            if (godMode) return;
            Health -= damage;
            if (!Alive) KillThis();

            // todo: damage animation etc?
        }

        protected virtual void Knockback(Vector2 force)
        {
            
        }

        public virtual void KillThis()
        {
            Animator.SetTrigger("Death");
            Debug.Log($"Entity '{name}' was killed.");
        }
    }
}
