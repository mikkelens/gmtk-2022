using Attacks;
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
        [SerializeField, Required] protected Weapon entityWeapon; // class instance with stats in it
        
        protected Weapon LastWeapon;
        protected float LastAttackTime;

        // todo: maybe make animation handled in child class?
        protected virtual bool WantsToAttack => autoAttacks; // will only use "autoAttacks" field if WantsToAttack is not overridden
        protected virtual bool CanAttack => LastWeapon is not { cooldown: { Enabled: true } } || LastAttackTime.TimeSince() >= LastWeapon.cooldown.Value;


        protected override void EntityUpdate()
        {
            base.EntityUpdate();

            // See if thing in attack box
            if (entityWeapon != null && WantsToAttack && CanAttack)
            {
                StartAttack(entityWeapon); // first attack. should probably be picked somehow
            }
        }

        protected virtual void StartAttack(Weapon weapon) // overridden to add animation
        {
            LastWeapon = weapon;
            LastAttackTime = Time.time;
            UseWeapon(weapon);
        }

        // seperates player and enemy logic, this is overriden in Enemy.cs
        protected virtual void UseWeapon(Weapon weapon)
        {
            TryHitWithWeapon(weapon);
        }
        
        protected void TryHitWithWeapon(Weapon weapon)
        {
            Vector2 lookDirection = transform.forward.WorldToPlane();
            bool hit = weapon.TryHitEntity(this, lookDirection, targetLayerMask);
            if (hit && weapon.selfKnockbackStrength.Enabled) ApplyKnockback(-lookDirection * weapon.selfKnockbackStrength.Value); // apply knockback to self
            EndAttack(weapon);
        }

        protected virtual void EndAttack(Weapon weapon)
        {
            // just for animation (?)
        }
    }
}