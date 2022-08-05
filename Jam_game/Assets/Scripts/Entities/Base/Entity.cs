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
    public abstract class Entity : MonoBehaviour
    {
        // foldout group titling & ease
        protected const string QuirkCategory = "Quirks";
        protected const string StatCategory = "Stats";
        
        [FoldoutGroup(QuirkCategory)]
        [SerializeField] protected bool godMode;
        
        [FoldoutGroup(StatCategory)]
        [HideIf("godMode")]
        [SerializeField] protected IntStat maxHealth;
        
        // health status
        private int _currentHealth;
        
        // outside components
        protected GameManager Manager;

        // components with this entity
        protected Transform Transform;
        protected Animator Animator;

        protected bool Alive => _currentHealth > 0;
        public SpawnEvent SpawnOrigin { get; set; }
        public Metrics Metrics { get; } = new Metrics();

        
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

        protected void Update()
        {
            if (Alive) EntityUpdate();
        }
        protected virtual void EntityUpdate() // Update, but only ran if entity is alive
        {
            
        }

        public ImpactResultData RegisterImpact(ImpactData impact, Vector2 knockbackDirection) // Main way of getting hit
        {
            ImpactResultData resultData = new ImpactResultData();
            if (impact.effects.Enabled) ApplyEffects(impact.effects.Value); 
            if (impact.knockback.Enabled) ApplyKnockback(knockbackDirection * impact.knockback.Value);
            if (!Alive) return resultData;
            if (impact.healing.Enabled) resultData.Healing = ApplyHealing(impact.healing.Value);
            if (impact.damage.Enabled) resultData.Damage = ApplyDamage(impact.damage.Value);
            if (!Alive) resultData.Kills++;
           
            return resultData;
        }

        public virtual void ApplyKnockback(Vector2 force)
        {
            // only used in movable entities
        }
        private int ApplyDamage(int damage)
        {
            if (godMode) return 0;
            int previousHealth = _currentHealth;
            _currentHealth -= damage;
            if (_currentHealth <= 0) KillThis();
            return previousHealth - _currentHealth;
        }
        private int ApplyHealing(int healing)
        {
            int previousHealth = _currentHealth;
            _currentHealth += healing;
            return _currentHealth - previousHealth;
        }
        private void ApplyEffects(ModifierCollection effects)
        {
            this.ApplyModifierCollectionToObject(effects);
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
