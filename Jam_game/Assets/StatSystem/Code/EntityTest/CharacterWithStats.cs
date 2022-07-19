using System;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem.Code.TestUsage.Entity
{
    [Serializable]
    public class PlayerStats
    {
        public CharacterStat health;
        public CharacterStat damage;
        public CharacterStat armor;
        public CharacterStat speed;
        public CharacterStat knockback;
    }
    
    public class CharacterWithStats : MonoBehaviour
    {
        [SerializeField] private PlayerStats stats;
    }
}