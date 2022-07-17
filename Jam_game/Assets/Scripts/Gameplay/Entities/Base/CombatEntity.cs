using System.Collections;
using Gameplay.Entities.Enemies;
using Gameplay.Entities.StatsAssets;
using Gameplay.Upgrades;
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
        CombatStats _combatStats;
        protected CombatStats CStats
        {
            get
            {
                if (_combatStats == null)
                {
                    _combatStats = Stats as CombatStats;
                }
                return _combatStats;
            }
        }

        protected int TargetLayerMask;
        protected float LastAttackTime;
        
        protected virtual bool WantsToAttack => Stats.autoAttacks;
        protected bool CanAttack => LastAttackTime.TimeSince() >= _stats.meleeCooldown;
        protected Ray AttackRay => new Ray(Transform.position, Transform.forward);

        private void Awake()
        {
            _stats = startingStats;
            
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
            if (Physics.Raycast(AttackRay, out RaycastHit hitData, _stats.meleeDistance, TargetLayerMask)) // Within distance?
                HitOther(hitData.transform.GetComponent<Entity>());
            Stopping = false;
            Animator.ResetTrigger("Attack");
        }

        protected void HitOther(Entity entity)
        {
            Vector2 knockback = GetTargetLookDirection() * _stats.basicKnockbackStrength;
            ApplyKnockback(-knockback); // apply knockback to self
            entity.TakeHit(_stats.meleeDamage, knockback);
        }
    }
}