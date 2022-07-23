using System;
using Gameplay.StatSystem;

namespace Gameplay.Entities
{
    [Serializable]
    public class Attack
    {
        // This *has* to match the name of the animation. alternatives: "Charge", "MeleeAlternate"
        public string animationName = "Melee";
        public bool hasDirectionalAnimation = false; // should be true for attacks that can be directional
        
        public IntStat damage;
        public FloatStat maxDistance;
        public FloatStat cooldown;
        public FloatStat targetKnockbackStrength;
        public FloatStat selfKnockbackStrength;
        
    }
}