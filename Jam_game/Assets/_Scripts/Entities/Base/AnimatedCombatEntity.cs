using Abilities.Base;

namespace Entities.Base
{
    public class AnimatedCombatEntity : CombatEntity
    {
        protected override void StartAbilityUse(Ability ability)
        {
            if (!ability.usageAnimation.Enabled) return;
            Animator.SetTrigger(ability.usageAnimation.Value.name);
            
            if (!ability.usageAnimation.Value.isDirectional) return;
            string directionString = AttackAnimationDirectionString(ability);
            Animator.SetBool(directionString, Animator.GetBool(directionString));
            
            base.StartAbilityUse(ability);
        }

        protected override void FinishAbilityUse(Ability ability)
        {
            base.FinishAbilityUse(ability);
            
            if (!ability.usageAnimation.Enabled) return;
            Animator.ResetTrigger(ability.usageAnimation.Value.name);
        }

        protected static string AttackAnimationDirectionString(Ability attack) => attack.usageAnimation.Value.name + "Direction";
    }
}