using System;
using UnityEngine;

namespace Gameplay.Entities
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Upgrade", menuName = "Stats/BaseStats")]
    public class Stats : ScriptableObject
    {
        [Header("Base Entity stats")]
        public bool godMode = false;
        public int maxHealth = 1; // should be higher for player
        
        [Header("Movable Entity stats")]
        public float maxSpeed = 5f;
        public float walkAccelSpeed = 65f;
        public float stopBonus = 3f;
        public float maxTurnSpeed; // in angles per second
        public AnimationCurve turnSpeedCurve = new AnimationCurve(); // changes the turn speed dynamically
        public bool freezeAffectsRotation = false;
        public bool attackStopsRotation = true;

        [Header("Combat Entity stats")]
        public bool autoAttacks = true; // should be false on player
        public int basicDamage = 1;
        public int meleeDamage = 1;
        public float meleeDistance = 2.5f;
        public float meleeCooldown = 1.25f;
        public float basicKnockbackStrength = 10f;
        public float meleeKnockbackBonus = 2f;
        
        [Header("Player Entity stats")]
        public int alternateMeleeDamage = 2;
        public float aimTurnSpeedBonus = 2f;
        
        [Header("Enemy Entity stats")]
        [Tooltip("Relative to other enemies of the same wave.")]
        public float relativeSpawnChance = 1f;
        public float meleeAttackDelay = 0.75f; // could be automated using animation time info maybe actually but i dont wanna do it
        public float stunDuration = 0.5f; // applied when taking damage

        
        // todo: add more stats
    }
}