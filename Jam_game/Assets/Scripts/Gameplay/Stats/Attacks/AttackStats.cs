using System;
using Gameplay.Stats.DataTypes;
using Tools;
using UnityEngine;

namespace Gameplay.Stats.Attacks
{
    [Serializable]
    public class AttackStats : StatCollection
    {
        [Tooltip("This *has* to match the name of the animation. alternatives: 'Charge', 'MeleeAlternate'")]
        public string animationName = "Melee";
        [Tooltip("Should be true for attacks that can be directional")]
        public bool hasDirectionalAnimation = false;

        public Optional<float> testFloat;
        public HitStats hit;
        public FloatStat maxDistance;
        public FloatStat cooldown;
        public FloatStat selfKnockbackStrength;
    }
}