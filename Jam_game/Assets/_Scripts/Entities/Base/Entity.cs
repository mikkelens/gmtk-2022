using System.Collections.Generic;
using Abilities;
using Abilities.Data;
using Events;
using Management;
using Sirenix.OdinInspector;
using Stats.Stat.Modifier;
using Stats.Stat.Variants;
using UnityEngine;

namespace Entities.Base
{
    [Tooltip("Entity: Hittable thing. think minecraft armor stand.")]
    [RequireComponent(typeof(Collider))]
    [SelectionBase]
    public class Entity : MonoBehaviour
    {
        // foldout group titling & ease
        protected const string QuirkCategory = "Quirks";
        protected const string StatCategory = "Stats";
        
        [FoldoutGroup(QuirkCategory)]
        [SerializeField] protected bool godMode = false;
        
        [FoldoutGroup(StatCategory)]
        [HideIf("godMode")]
        [SerializeField] protected IntStat maxHealth;


        // outside components
        protected GameManager Manager;
        protected SpawnEvent SpawnOrigin;

        // components with this entity
        protected Transform Transform;
        protected Animator Animator;

        // health status
        private int _currentHealth;
        protected bool Alive => _currentHealth > 0;

        protected virtual void Awake()
        {
            Transform = transform;
            if (Transform == null) Debug.Log("Transform is null???");
            
            Animator = GetComponentInChildren<Animator>();
            if (Animator == null) Debug.Log($"No animator component found on entity '{name}'.");
        }

        protected virtual void Start()
        {
            SetHealth(maxHealth.CurrentValue);
            Manager = GameManager.Instance;
            
        }
        public void SetSpawnOrigin(SpawnEvent origin)
        {
            SpawnOrigin = origin;
        }

        protected void Update()
        {
            if (Alive) EntityUpdate();
        }
        protected virtual void EntityUpdate() // Update, but only ran if entity is alive
        {
            
        }

        public void RegisterImpact(ImpactData impact, Vector2 knockbackDirection) // Main way of getting hit
        {
            if (impact.knockback.Enabled) ApplyKnockback(knockbackDirection * impact.knockback.Value);
            if (!Alive) return;
            if (impact.healing.Enabled) ApplyHealing(impact.healing.Value);
            if (impact.damage.Enabled) ApplyDamage(impact.damage.Value);
            
            if (impact.boolEffects.Enabled) ApplyEffects(impact.boolEffects.Value);
            if (impact.intEffects.Enabled) ApplyEffects(impact.intEffects.Value);
            if (impact.floatEffects.Enabled) ApplyEffects(impact.floatEffects.Value);
        }

        protected virtual void ApplyKnockback(Vector2 force)
        {
            // only used in movable entities
        }
        private void ApplyDamage(int damage)
        {
            if (godMode) return;
            _currentHealth -= damage;
            if (_currentHealth <= 0) KillThis();
        }
        private void ApplyHealing(int healing)
        {
            _currentHealth += healing;
        }
        private void ApplyEffects<T>(List<Modifier<T>> effects)
        {
            this.FindAllStatsOnObject<T>().ApplyModifiers(effects);
        }

        public virtual void KillThis()
        {
            Debug.Log($"Entity '{name}' was killed.");
            Animator.SetTrigger("Death");
            SpawnOrigin.DespawnEntity(this);
        }
        public void HealToFull()
        {
            SetHealth(maxHealth);
        }
        private void SetHealth(int health)
        {
            _currentHealth = health;
        }
    }
}
