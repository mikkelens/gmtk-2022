using Gameplay.Entities.Enemies;
using Tools;
using UnityEngine;

namespace Gameplay.Entities.Base
{
    [Tooltip("Combat Entity: This can hit other entites.")]
    public class CombatEntity : MovingEntity
    {
        [SerializeField] protected LayerMask targetLayerMask;
        
        protected float LastAttackTime;
        protected float LastAttackCooldown;
        
        protected virtual bool WantsToAttack => stats.autoAttacks;
        protected virtual bool CanAttack => LastAttackTime.TimeSince() >= LastAttackCooldown;
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

        private void StartAttack(Attack attack)
        {
            LastAttackTime = Time.time;
            Animator.SetTrigger("Attack");
            StartMelee(attack);
        }

        protected virtual void StartMelee(Attack attack)
        {
            TryMelee(attack);
        }
        
        protected void TryMelee(Attack attack)
        {
            // Raycast for hit
            if (Physics.Raycast(AttackRay, out RaycastHit hitData, attack.maxDistance, targetLayerMask.value)) // Within distance?
                HitOther(hitData.transform.GetComponent<Entity>(), attack);
            Animator.ResetTrigger("Attack");
        }

        protected void HitOther(Entity entity, Attack attack)
        {
            Vector2 lookDirection = GetTargetLookDirection();
            entity.TakeHit(attack.damage, lookDirection * attack.targetKnockbackStrength);
            ApplyKnockback(-lookDirection * attack.selfKnockbackStrength); // apply knockback to self
        }
    }
}