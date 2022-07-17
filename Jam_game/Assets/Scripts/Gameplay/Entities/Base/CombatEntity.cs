using Gameplay.Entities.Enemies;
using Tools;
using UnityEngine;

namespace Gameplay.Entities.Base
{
    [Tooltip("Combat Entity: This can hit other entites.")]
    public class CombatEntity : MovableEntity
    {
        [SerializeField] protected LayerMask targetLayerMask;
        
        protected float LastAttackTime;
        
        protected virtual bool WantsToAttack => myStats.autoAttacks;
        protected virtual bool CanAttack => LastAttackTime.TimeSince() >= myStats.meleeCooldown;
        protected virtual Ray AttackRay => new Ray(Transform.position, Transform.forward);
        
        protected override void Update()
        {
            base.Update();
            
            // See thing in attack box
            if (WantsToAttack && CanAttack)
            {
                StartAttack();
            }
        }

        private void StartAttack()
        {
            LastAttackTime = Time.time;
            Animator.SetTrigger("Attack");
            StartMelee();
        }

        protected virtual void StartMelee()
        {
            TryMelee();
        }
        
        protected void TryMelee()
        {
            // Raycast for hit
            if (Physics.Raycast(AttackRay, out RaycastHit hitData, myStats.meleeDistance, targetLayerMask.value)) // Within distance?
                HitOther(hitData.transform.GetComponent<Entity>());
            Stopping = false;
            Animator.ResetTrigger("Attack");
        }

        protected void HitOther(Entity entity)
        {
            Vector2 knockback = GetTargetLookDirection() * myStats.basicKnockbackStrength;
            ApplyKnockback(-knockback); // apply knockback to self
            entity.TakeHit(myStats.meleeDamage, knockback);
        }
    }
}