using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Gameplay.Entities.OLD_Stats
{
    // Data object that is stored in Pickup components.
    [CreateAssetMenu(fileName = "New Upgrade Asset", menuName = "Stats/Upgrade Asset")]
    public class Upgrade : ScriptableObject
    {
        public string upgradeName = "Unnamed Upgrade";
        
        [FoldoutGroup("Health")]
        public Optional<int> maxHealthBuff = new Optional<int>(1);
        
        [FoldoutGroup("Attack")]
        public Optional<int> attackDamageBuff = new Optional<int>(1);
        [FoldoutGroup("Attack")]
        public Optional<float> attackDistanceBuff = new Optional<float>(1.2f);
        [FoldoutGroup("Attack")]
        public Optional<float> attackCooldownBuff = new Optional<float>(0.8f);
        [FoldoutGroup("Attack")]
        public Optional<float> attackKnockbackBuff = new Optional<float>(1.5f);
        
        [FoldoutGroup("Alternate Attack")]
        public Optional<int> alternateAttackDamageBuff = new Optional<int>(1);

        public Optional<float> collisionKnockbackStrengthBuff = new Optional<float>(2f);
        
        // public Optional<int> alternateMeleeDamageBuff = new Optional<int>(1);

        public void UpgradeStats(EntityStats stats)
        {
            if (maxHealthBuff.Enabled)
            {
                stats.health += maxHealthBuff.Value;
                stats.maxHealth += maxHealthBuff.Value;
            }
            if (attackDamageBuff.Enabled)
                stats.mainMeleeAttack.damage += attackDamageBuff.Value;
            if (attackDistanceBuff.Enabled)
                stats.mainMeleeAttack.maxDistance *= attackDistanceBuff.Value;
            if (attackCooldownBuff.Enabled)
                stats.mainMeleeAttack.cooldown *= attackCooldownBuff.Value;
            if (attackKnockbackBuff.Enabled)
                stats.mainMeleeAttack.targetKnockbackStrength *= attackKnockbackBuff.Value;
            if (collisionKnockbackStrengthBuff.Enabled)
                stats.collisionKnockback *= collisionKnockbackStrengthBuff.Value;
        }
    }
}