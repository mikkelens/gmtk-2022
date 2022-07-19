using System;

namespace Gameplay.Entities.Stats
{
    [Serializable]
    public class Attack
    {
        public string animationName = "Melee";
        public int damage = 1;
        public float maxDistance = 2.5f;
        public float cooldown = 1.25f;
        public float targetKnockbackStrength = 10f;
        public float selfKnockbackStrength = 5f;
        
        public bool hasDirectionalAnimation = false;
    }
}