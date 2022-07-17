using Gameplay.Entities.StatsAssets;
using Tools;
using UnityEngine;

namespace Gameplay.Upgrades
{
    [CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrades/Upgrade")]
    public class Upgrade : ScriptableObject
    {
        // base
        
        // combat

        // player
        public Optional<float> basicKnockbackStrengthBuff;
        public Optional<float> meleeKnockbackBonusBuff;
        public Optional<float> meleeDistanceBuff;
        public Optional<float> meleeCooldownBuff;
        public Optional<bool> autoAttacksBuff; // should be false on player
        public Optional<int> meleeDamageBuff;

        public void UpgradeStats(Stats stats)
        {
            // base
            
            
            // combat
            if (stats is not CombatStats combatStats) return;
            if (meleeDamageBuff.Enabled)
                combatStats.meleeDamage += meleeDamageBuff.Value;

            // player
            if (stats is not PlayerStats playerStats) return;
        }
    }
}