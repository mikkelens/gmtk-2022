using Abilities;

namespace Entities.Base
{
    public abstract class AnimatedCombatEntity : CombatEntity
    {
        protected override void StartAbilityUse()
        {
            if (!ChosenAbility.usageAnimation.Enabled) return;
            Animator.SetTrigger(ChosenAbility.usageAnimation.Value.name);
            
            if (!ChosenAbility.usageAnimation.Value.isDirectional) return;
            string directionString = AttackAnimationDirectionString(ChosenAbility);
            Animator.SetBool(directionString, Animator.GetBool(directionString));
            
            base.StartAbilityUse();
        }

        protected override void FinishAbilityUse()
        {
            base.FinishAbilityUse();
            
            if (!ChosenAbility.usageAnimation.Enabled) return;
            Animator.ResetTrigger(ChosenAbility.usageAnimation.Value.name);
        }

        private static string AttackAnimationDirectionString(Ability attack) => attack.usageAnimation.Value.name + "Direction";
    }
}