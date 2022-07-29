using Attacks;
using UnityEngine;

namespace Entities.Base
{
    public class AnimatedCombatEntity : CombatEntity
    {
        protected override void StartAttack(Weapon weapon)
        {
            base.StartAttack(weapon);
            
            if (!weapon.animation.Enabled) return;
            Animator.SetTrigger(weapon.animation.Value.name);
            
            if (!weapon.animation.Value.isDirectional) return;
            string directionString = AttackAnimationDirectionString(weapon);
            Animator.SetBool(directionString, Animator.GetBool(directionString));
        }

        protected override void EndAttack(Weapon weapon)
        {
            base.EndAttack(weapon);
            
            if (!weapon.animation.Enabled) return;
            Animator.ResetTrigger(weapon.animation.Value.name);
        }

        protected static string AttackAnimationDirectionString(Weapon attack) => attack.animation.Value.name + "Direction";

        protected override void ApplyKnockback(Vector2 force)
        {
            base.ApplyKnockback(force);
        }
    }
}