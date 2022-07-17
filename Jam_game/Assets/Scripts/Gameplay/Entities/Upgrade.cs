using Tools;
using UnityEngine;

namespace Gameplay.Entities
{
    [CreateAssetMenu(fileName = "New Upgrade Asset", menuName = "Stats/Upgrade Asset")]
    public class Upgrade : ScriptableObject
    {
        // base
        public Optional<int> maxHealthBuff = new Optional<int>(1);

        // combat
        public Optional<int> meleeDamageBuff = new Optional<int>(1);
        public Optional<float> meleeDistanceBuff = new Optional<float>(1.2f);
        public Optional<float> meleeCooldownBuff = new Optional<float>(0.8f);
        public Optional<float> meleeKnockbackBuff = new Optional<float>(1.5f);
        public Optional<float> collisionKnockbackStrengthBuff = new Optional<float>(2f);

        // player
        public Optional<int> alternateMeleeDamageBuff = new Optional<int>(1);

        public void UpgradeStats(EntityStats stats)
        {
            if (meleeDamageBuff.Enabled)
                stats.mainMeleeAttack.damage += meleeDamageBuff.Value;
            if (meleeDistanceBuff.Enabled)
                stats.mainMeleeAttack.maxDistance *= meleeDistanceBuff.Value;
            if (meleeCooldownBuff.Enabled)
                stats.mainMeleeAttack.cooldown *= meleeCooldownBuff.Value;
            if (meleeKnockbackBuff.Enabled)
                stats.mainMeleeAttack.targetKnockbackStrength *= meleeKnockbackBuff.Value;
            if (collisionKnockbackStrengthBuff.Enabled)
                stats.collisionKnockback *= collisionKnockbackStrengthBuff.Value;
        }
    }
}