using Tools;
using UnityEngine;

namespace Gameplay.Entities
{
    [CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrades/Upgrade")]
    public class Upgrade : ScriptableObject
    {
        // base
        public Optional<int> maxHealthBuff = new Optional<int>(1);

        // combat
        public Optional<float> basicKnockbackStrengthBuff;
        public Optional<float> meleeKnockbackBonusBuff;
        public Optional<float> meleeDistanceBuff;
        public Optional<float> meleeCooldownBuff;
        public Optional<bool> autoAttacksBuff; // should be false on player
        public Optional<int> meleeDamageBuff;

        // player
        public Optional<int> alternateMeleeDamageBuff = new Optional<int>(1);

        public void UpgradeStats(Stats stats)
        {
            // base
            
            
            // combat
            if (basicKnockbackStrengthBuff.Enabled)
                stats.basicKnockbackStrength += basicKnockbackStrengthBuff.Value;
            if (meleeDamageBuff.Enabled)
                stats.meleeDamage += meleeDamageBuff.Value;

            // player
        }
    }
}