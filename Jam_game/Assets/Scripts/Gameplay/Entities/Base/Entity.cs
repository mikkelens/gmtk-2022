using Management;
using UnityEngine;

namespace Gameplay.Entities.Base
{
    [Tooltip("Entity: Hittable thing. think minecraft armor stand.")]
    [RequireComponent(typeof(Collider))]
    [SelectionBase]
    public class Entity : MonoBehaviour
    {
        [SerializeField] private EntityStats startingStats;

        [HideInInspector]
        public EntityStats myStats;

        // outside components
        protected GameManager Manager;

        // components with this entity
        protected Transform Transform;
        protected Animator Animator;

        // health status
        private int _currentHealth;

        protected virtual void Awake()
        {
            Transform = transform;
            if (Transform == null) Debug.Log("Transform is null???");
            
            Animator = GetComponentInChildren<Animator>();
            if (Animator == null) Debug.Log($"No animator component found on entity '{name}'.");
            
            myStats = startingStats.Clone() as EntityStats;
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
            if (myStats.godMode) return;
            _currentHealth -= damage;
            if (_currentHealth <= 0) KillThis();

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
            SetHealth(myStats.maxHealth);
        }
        private void SetHealth(int health)
        {
            _currentHealth = health;
        }
        private void ApplyHealing(int healing)
        {
            _currentHealth += healing;
        }
    }
}
