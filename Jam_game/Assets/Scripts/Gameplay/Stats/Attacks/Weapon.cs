using Gameplay.Stats.DataTypes;
using Tools;
using UnityEngine;

namespace Gameplay.Stats.Attacks
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Stats/Weapon")]
    public class Weapon : ExpandableScriptableObject, IStatCollection
    {
        public Optional<HitStats> attackHit;
        public Optional<FloatStat> maxDistance;
        public Optional<FloatStat> cooldown;
        public Optional<FloatStat> selfKnockbackStrength;
        
        [Tooltip("This *has* to match the name of the animation. alternatives: 'Charge', 'MeleeAlternate'")]
        public string animationName = "Melee";
        [Tooltip("Should be true for attacks that can be directional")]
        public bool hasDirectionalAnimation = false;
    }
}