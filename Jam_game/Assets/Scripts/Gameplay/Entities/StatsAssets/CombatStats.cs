using System;
using UnityEngine;

namespace Gameplay.Entities.StatsAssets
{
    [Serializable]
    [CreateAssetMenu(fileName = "New stats asset", menuName = "Stats/CombatStats")]
    public class CombatStats : Stats
    {
        public float basicKnockbackStrength = 10f;
        public float meleeKnockbackBonus = 2f;
        public int meleeDamage = 1;
        public float meleeDistance = 2.5f;
        public float meleeCooldown = 1.25f;
        public bool autoAttacks = true; // should be false on player
    }
}