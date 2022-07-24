using System.Linq;
using Gameplay.Attacks;
using Gameplay.Entities.Enemies;
using Gameplay.Entities.Players;
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
        [SerializeField] private bool autoAttacks;
        [UsedImplicitly] private bool Headless => this as Player == null && this as Enemy == null;
     
        [FoldoutGroup(StatCategory)]
        [SerializeField, Required] private Weapon weapon; // class instance with stats in it
        
        protected AttackStats LastAttackStats;
        protected float LastAttackTime;

        private static string AttackAnimationDirectionString(AttackStats attack) => attack.animationName + "Direction";
        protected virtual bool WantsToAttack => autoAttacks; // will only use "autoAttacks" field if WantsToAttack is not overridden
        protected virtual bool CanAttack => LastAttackStats is not { cooldown: { Enabled: true } } || LastAttackTime.TimeSince() >= LastAttackStats.cooldown.Value;
        protected virtual Ray AttackRay => new Ray(Transform.position, Transform.forward);
        
        
        protected override void EntityUpdate()
        {
            base.EntityUpdate();

            // See if thing in attack box
            if (WantsToAttack && CanAttack)
            {
                StartAttack(GetAttack()); // first attack. should probably be picked somehow
            }
        }

        protected virtual AttackStats GetAttack()
        {
            return weapon.allAttacks.First();
        }

        protected virtual void StartAttack(AttackStats attack)
        {
            if (attack.hasDirectionalAnimation)
            {
                string directionString = AttackAnimationDirectionString(attack);
                Animator.SetBool(directionString, Animator.GetBool(directionString));
            }
            Animator.SetTrigger(attack.animationName);
            LastAttackStats = attack;
            LastAttackTime = Time.time;
            StartMelee(attack);
        }

        protected virtual void StartMelee(AttackStats attack)
        {
            TryHitWithAttack(attack);
        }
        
        protected void TryHitWithAttack(AttackStats attack)
        {
            // Raycast for hit
            float maxdistance = attack.maxDistance.Enabled ? attack.maxDistance.Value : float.MaxValue;
            if (Physics.Raycast(AttackRay, out RaycastHit hitData, maxdistance, targetLayerMask.value)) // Within distance?
                HitOther(hitData.transform.GetComponent<Entity>(), attack);
            EndAttack(attack);
        }

        protected void EndAttack(AttackStats attack)
        {
            Animator.ResetTrigger(attack.animationName);
        }

        private void HitOther(Entity entity, AttackStats attack)
        {
            Vector2 lookDirection = GetLookDirection();
            if (attack.hit.Enabled) entity.TakeHit(attack.hit.Value, lookDirection);
            if (attack.selfKnockbackStrength.Enabled) ApplyKnockback(-lookDirection * attack.selfKnockbackStrength.Value); // apply knockback to self
        }
    }
}