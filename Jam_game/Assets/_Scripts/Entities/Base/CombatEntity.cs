using Abilities.Base;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Entities.Base
{
    [Tooltip("Combat Entity: This can hit other entites.")]
    public class CombatEntity : MovingEntity
    {
        [FoldoutGroup(QuirkCategory)]
        [SerializeField] protected LayerMask targetLayerMask;
        [FoldoutGroup(QuirkCategory)]
        [ShowIf("Headless")] // this should only be visible to designer if this entity is a dummy
        [SerializeField] protected bool autoAttacks;
        [UsedImplicitly] protected virtual bool Headless => true;
     
        [FoldoutGroup(StatCategory)]
        [SerializeField] protected Optional<Ability> defaultAbility; // class instance with stats in it

        private Ability _lastAbility;
        private Ability _activeAbility;
        public virtual Ability ActiveAbility
        {
            get => _activeAbility == null ? defaultAbility.Enabled ? defaultAbility.Value : null : _activeAbility;
            set => _activeAbility = value;
        }
        private float _lastAttackTime;

        // todo: maybe make animation handled in child class?
        protected virtual bool WantsToAttack => autoAttacks; // will only use "autoAttacks" field if WantsToAttack is not overridden
        protected bool CanAttack => _lastAbility == null || !_lastAbility.cooldown.Enabled || _lastAttackTime.TimeSince() >= _lastAbility.cooldown.Value;

        protected override void EntityUpdate()
        {
            base.EntityUpdate();
            CombatUpdate();
        }

        protected virtual void CombatUpdate()
        {
            // See if thing in attack box
            if (ActiveAbility != null && WantsToAttack && CanAttack)
            {
                StartAbilityUse(ActiveAbility);
            }
        }

        protected virtual void StartAbilityUse(Ability ability) // overridden to add animation
        {
            _lastAbility = ability;
            _lastAttackTime = Time.time;
            UseAbility(ability);
        }

        // seperates player and enemy logic, this is overriden in Enemy.cs
        protected virtual void UseAbility(Ability ability)
        {
            TryHitWithAbility(ability);
        }
        
        protected void TryHitWithAbility(Ability ability)
        {
            ability.UseAbility(this);
            FinishAbilityUse(ability);
        }

        protected virtual void FinishAbilityUse(Ability ability)
        {
            // just for animation (?)
        }
    }
}