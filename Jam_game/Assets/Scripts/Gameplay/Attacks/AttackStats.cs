using System;
using Gameplay.Stats;
using Gameplay.Stats.DataTypes;
using Tools;
using UnityEngine;

namespace Gameplay.Attacks
{
    [Serializable]
    public class AttackStats : IStatCollection
    {
        [Tooltip("This *has* to match the name of the animation. alternatives: 'Charge', 'MeleeAlternate'")]
        public string animationName = "Melee";
        [Tooltip("Should be true for attacks that can be directional")]
        public bool hasDirectionalAnimation = false;
        
        public Optional<HitStats> hit;
        public Optional<FloatStat> maxDistance;
        public Optional<FloatStat> cooldown;
        public Optional<FloatStat> selfKnockbackStrength;
    }
}