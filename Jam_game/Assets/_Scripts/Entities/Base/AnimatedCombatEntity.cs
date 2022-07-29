using Abilities;
using Abilities.Weapons;
using UnityEngine;

namespace Entities.Base
{
    public class AnimatedCombatEntity : CombatEntity
    {
        protected override void StartAttack(MeleeWeapon weapon)
        {
            base.StartAttack(weapon);
            
            if (!weapon.usageAnimation.Enabled) return;
            Animator.SetTrigger(weapon.usageAnimation.Value.name);
            
            if (!weapon.usageAnimation.Value.isDirectional) return;
            string directionString = AttackAnimationDirectionString(weapon);
            Animator.SetBool(directionString, Animator.GetBool(directionString));
        }

        protected override void EndAttack(MeleeWeapon weapon)
        {
            base.EndAttack(weapon);
            
            if (!weapon.usageAnimation.Enabled) return;
            Animator.ResetTrigger(weapon.usageAnimation.Value.name);
        }

        protected static string AttackAnimationDirectionString(MeleeWeapon attack) => attack.usageAnimation.Value.name + "Direction";

        protected override void ApplyKnockback(Vector2 force)
        {
            base.ApplyKnockback(force);
        }
    }
}