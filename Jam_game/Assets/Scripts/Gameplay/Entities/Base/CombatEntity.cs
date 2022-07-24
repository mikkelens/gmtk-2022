using Gameplay.Stats.Attacks;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Gameplay.Entities.Base
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
        [SerializeField, Required] protected Weapon activeWeapon; // class instance with stats in it
        
        protected Weapon LastWeapon;
        protected float LastAttackTime;

        protected static string AttackAnimationDirectionString(Weapon attack) => attack.animationName + "Direction";
        protected virtual bool WantsToAttack => autoAttacks; // will only use "autoAttacks" field if WantsToAttack is not overridden
        protected virtual bool CanAttack => LastWeapon is not { cooldown: { Enabled: true } } || LastAttackTime.TimeSince() >= LastWeapon.cooldown.Value;
        protected virtual Ray AttackRay => new Ray(Transform.position, Transform.forward);
        
        
        protected override void EntityUpdate()
        {
            base.EntityUpdate();

            // See if thing in attack box
            if (WantsToAttack && CanAttack)
            {
                StartAttack(activeWeapon); // first attack. should probably be picked somehow
            }
        }

        protected virtual void StartAttack(Weapon weapon)
        {
            if (weapon.hasDirectionalAnimation)
            {
                string directionString = AttackAnimationDirectionString(weapon);
                Animator.SetBool(directionString, Animator.GetBool(directionString));
            }
            Animator.SetTrigger(weapon.animationName);
            LastWeapon = weapon;
            LastAttackTime = Time.time;
            StartUseWeapon(weapon);
        }

        protected virtual void StartUseWeapon(Weapon attack)
        {
            TryHitWithWeapon(attack);
        }
        
        protected void TryHitWithWeapon(Weapon attack)
        {
            // Raycast for hit
            float maxdistance = attack.maxDistance.Enabled ? attack.maxDistance.Value : float.MaxValue;
            if (Physics.Raycast(AttackRay, out RaycastHit hitData, maxdistance, targetLayerMask.value)) // Within distance?
                HitOther(hitData.transform.GetComponent<Entity>(), attack);
            EndAttack(attack);
        }

        protected void EndAttack(Weapon attack)
        {
            Animator.ResetTrigger(attack.animationName);
        }

        private void HitOther(Entity entity, Weapon attack)
        {
            Vector2 lookDirection = GetLookDirection();
            if (attack.attackHit.Enabled) entity.TakeHit(attack.attackHit.Value, lookDirection);
            if (attack.selfKnockbackStrength.Enabled) ApplyKnockback(-lookDirection * attack.selfKnockbackStrength.Value); // apply knockback to self
        }
    }
}