using System;
using Gameplay.Stats;
using Gameplay.Stats.DataTypes;
using UnityEngine;

namespace Gameplay.Attacks
{
    public class AttackStats : StatCollection
    {
        [Tooltip("This *has* to match the name of the animation. alternatives: 'Charge', 'MeleeAlternate'")]
        public string animationName = "Melee";
        [Tooltip("Should be true for attacks that can be directional")]
        public bool hasDirectionalAnimation = false;
        
        public HitStats hit;
        public FloatStat maxDistance;
        public FloatStat cooldown;
        public FloatStat selfKnockbackStrength;
    }
}