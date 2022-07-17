using System.Collections;
using Gameplay.Entities.Enemies;
using Tools;
using UnityEngine;

namespace Gameplay.Entities.Base
{
    /// <summary>
    ///  reeeeeee
    /// </summary>
    
    [Tooltip("Combat Entity: This can hit other entites.")]
    public class CombatEntity : MovableEntity
    {
        protected int TargetLayerMask;
        protected float LastAttackTime;
        
        protected virtual bool WantsToAttack => Stats.autoAttacks;
        protected bool CanAttack => LastAttackTime.TimeSince() >= Stats.meleeCooldown;
        protected Ray AttackRay => new Ray(Transform.position, Transform.forward);

        private void Awake()
        {
            string target = GetType() == typeof(Enemy) ? "Player" : "Enemy";
            TargetLayerMask = LayerMask.GetMask(target);
        }

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
            if (Physics.Raycast(AttackRay, out RaycastHit hitData, Stats.meleeDistance, TargetLayerMask)) // Within distance?
                HitOther(hitData.transform.GetComponent<Entity>());
            Stopping = false;
            Animator.ResetTrigger("Attack");
        }

        protected void HitOther(Entity entity)
        {
            Vector2 knockback = GetTargetLookDirection() * Stats.basicKnockbackStrength;
            ApplyKnockback(-knockback); // apply knockback to self
            entity.TakeHit(Stats.meleeDamage, knockback);
        }
    }
}