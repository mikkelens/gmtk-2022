using Abilities;

namespace Entities.Base
{
    public abstract class AnimatedCombatEntity : CombatEntity
    {
        protected override void StartAbilityUse()
        {
            if (!ActiveAbility.usageAnimation.Enabled) return;
            Animator.SetTrigger(ActiveAbility.usageAnimation.Value.name);
            
            if (!ActiveAbility.usageAnimation.Value.isDirectional) return;
            string directionString = AttackAnimationDirectionString(ActiveAbility);
            Animator.SetBool(directionString, Animator.GetBool(directionString));
            
            base.StartAbilityUse();
        }

        protected override void FinishAbilityUse()
        {
            base.FinishAbilityUse();
            
            if (!ActiveAbility.usageAnimation.Enabled) return;
            Animator.ResetTrigger(ActiveAbility.usageAnimation.Value.name);
        }

        private static string AttackAnimationDirectionString(Ability attack) => attack.usageAnimation.Value.name + "Direction";
    }
}