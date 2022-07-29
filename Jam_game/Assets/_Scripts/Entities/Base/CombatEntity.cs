using Abilities;
using Abilities.Weapons;
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
        [SerializeField] protected Optional<MeleeWeapon> defaultWeapon; // class instance with stats in it

        private MeleeWeapon _lastWeapon;
        private MeleeWeapon _activeWeapon;
        public virtual MeleeWeapon ActiveWeapon
        {
            get => _activeWeapon == null ? defaultWeapon.Enabled ? defaultWeapon.Value : null : _activeWeapon;
            set => _lastWeapon = _activeWeapon = value;
        }
        private float _lastAttackTime;

        // todo: maybe make animation handled in child class?
        protected virtual bool WantsToAttack => autoAttacks; // will only use "autoAttacks" field if WantsToAttack is not overridden
        protected bool CanAttack => _lastWeapon == null || !_lastWeapon.cooldown.Enabled || _lastAttackTime.TimeSince() >= _lastWeapon.cooldown.Value;

        protected override void EntityUpdate()
        {
            base.EntityUpdate();
            CombatUpdate();
        }

        protected virtual void CombatUpdate()
        {
            // See if thing in attack box
            if (ActiveWeapon != null && WantsToAttack && CanAttack)
            {
                StartAttack(ActiveWeapon); // first attack. should probably be picked somehow
            }
        }

        protected virtual void StartAttack(MeleeWeapon weapon) // overridden to add animation
        {
            _lastWeapon = weapon;
            _lastAttackTime = Time.time;
            UseWeapon(weapon);
        }

        // seperates player and enemy logic, this is overriden in Enemy.cs
        protected virtual void UseWeapon(MeleeWeapon weapon)
        {
            TryHitWithWeapon(weapon);
        }
        
        protected void TryHitWithWeapon(MeleeWeapon weapon)
        {
            Vector2 lookDirection = transform.forward.WorldToPlane();
            bool hit = weapon.TryHitEntity(this, lookDirection, targetLayerMask);
            if (hit && weapon.usageSelfKnockback.Enabled) ApplyKnockback(-lookDirection * weapon.usageSelfKnockback.Value); // apply knockback to self
            EndAttack(weapon);
        }

        protected virtual void EndAttack(MeleeWeapon weapon)
        {
            // just for animation (?)
        }
    }
}