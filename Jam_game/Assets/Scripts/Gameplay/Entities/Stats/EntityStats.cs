using System;
using UnityEngine;

namespace Gameplay.Entities.Stats
{
    // [CreateAssetMenu(fileName = "NewStatsAsset", menuName = "Stats/StatsAsset")]
    // public class EntityStats : ScriptableObject, ICloneable
    // {
    //     public object Clone()
    //     {
    //         return MemberwiseClone();
    //     }
    //     
    //     [Header("Base Entity stats")]
    //     public bool godMode = false;
    //     public int maxHealth = 1; // should be higher for player
    //     public int health = 1;
    //     
    //     [Header("Movable Entity stats")]
    //     public float maxSpeed = 5f;
    //     public float walkAccelSpeed = 65f;
    //     public float maxStopBonus = 3f;
    //     public float maxTurnSpeed = 10f; // in angles per second
    //     public AnimationCurve turnSpeedCurve = new AnimationCurve(); // changes the turn speed dynamically
    //     public bool turningAffectsMoveDirection = false;
    //     public bool freezingAffectsRotation = false;
    //     public bool stoppingAffectsRotation = true;
    //
    //     [Header("Combat Entity stats")]
    //     public Attack mainMeleeAttack;
    //
    //     [Header("Enemy Entity stats")]
    //     public bool autoAttacks = true; // should be false on player
    //     public int collisionDamage = 1;
    //     public float collisionKnockback = 1f;
    //
    //     [Header("Player Entity stats")]
    //     public Attack altMeleeAttack;
    //     public float aimTurnSpeedBonus = 2f;
    //     
    //     [Header("Enemy Entity stats")]
    //     [Tooltip("Relative to other enemies of the same wave.")]
    //     public float relativeSpawnChance = 1f;
    //     public float attackChargeTime = 0.75f; // could be automated using animation time info maybe actually but i dont wanna do it
    //     public float minAttackAttemptDistance = 2.5f;
    //     public float stunDuration = 0.5f; // applied when taking damage
    //     
    //     [Header("Reptile Entity Stats")]
    //     public float headTurnAngle = 1f; // in degrees
    //     
    //     // todo: add more stats
    // }
}