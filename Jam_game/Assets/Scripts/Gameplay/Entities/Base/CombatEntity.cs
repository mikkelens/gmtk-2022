using Gameplay.Entities.Enemies;
using Gameplay.Entities.OLD_Stats;
using Tools;
using UnityEngine;

namespace Gameplay.Entities.Base
{
    [Tooltip("Combat Entity: This can hit other entites.")]
    public class CombatEntity : MovingEntity
    {
        [SerializeField] protected LayerMask targetLayerMask;
        
        protected Attack LastAttack;
        protected float LastAttackTime;
        
        private static string AttackAnimationDirectionString(Attack attack) => attack.animationName + "Direction";
        protected virtual bool WantsToAttack => stats.autoAttacks;
        protected virtual bool CanAttack => LastAttackTime.TimeSince() >= LastAttack.cooldown;
        protected virtual Ray AttackRay => new Ray(Transform.position, Transform.forward);
        
        
        protected override void Update()
        {
            base.Update();
            
            // See thing in attack box
            if (WantsToAttack && CanAttack)
            {
                StartAttack(stats.mainMeleeAttack);
            }
        }

        protected virtual void StartAttack(Attack attack)
        {
            if (attack.hasDirectionalAnimation)
            {
                string directionString = AttackAnimationDirectionString(attack);
                Animator.SetBool(directionString, Animator.GetBool(directionString));
            }
            Animator.SetTrigger(attack.animationName);
            LastAttack = attack;
            LastAttackTime = Time.time;
            StartMelee(attack);
        }

        protected virtual void StartMelee(Attack attack)
        {
            TryHitWithAttack(attack);
        }
        
        protected void TryHitWithAttack(Attack attack)
        {
            // Raycast for hit
            if (Physics.Raycast(AttackRay, out RaycastHit hitData, attack.maxDistance, targetLayerMask.value)) // Within distance?
                HitOther(hitData.transform.GetComponent<Entity>(), attack);
            EndAttack(attack);
        }

        protected void EndAttack(Attack attack)
        {
            Animator.ResetTrigger(attack.animationName);
        }

        private void HitOther(Entity entity, Attack attack)
        {
            Vector2 lookDirection = GetLookDirection();
            entity.TakeHit(attack.damage, lookDirection * attack.targetKnockbackStrength);
            ApplyKnockback(-lookDirection * attack.selfKnockbackStrength); // apply knockback to self
        }
    }
}