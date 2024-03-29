﻿using System.Collections;
using Abilities;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Entities.Base
{
    [Tooltip("Combat Entity: This can hit other entites.")]
    public abstract class CombatEntity : MovingEntity
    {
        [FoldoutGroup(QuirkCategory)]
        [SerializeField] protected LayerMask targetLayerMask;

        public LayerMask TargetLayerMask => targetLayerMask;
        protected Ability ChosenAbility { get; private set; }

        // todo: maybe make animation handled in child class?
        protected override void AliveUpdate()
        {
            base.AliveUpdate();
            
            ChosenAbility = SelectAbility();
            if (ChosenAbility == null) return;
            StartAbilityUse();
        }

        protected abstract Ability SelectAbility();
        
        // START
        protected virtual void StartAbilityUse() // overridden to add animation
        {
            StartCoroutine(UseAbility());
        }

        private IEnumerator UseAbility() // Think dark soulds attack with long chargeup
        {
            stopping = true;
            Animator.SetBool("Walking", false);
            yield return StartCoroutine(ChosenAbility.TriggerAbility(this));
            Animator.SetBool("Walking", true);
            stopping = false;
            FinishAbilityUse();
        }

        // END
        protected abstract void FinishAbilityUse();
        
    #if UNITY_EDITOR
        private void OnDrawGizmos() // showing area check box
        {
            if (ChosenAbility != null) return;
            Transform ??= transform;
            Gizmos.color = Color.red;
            Gizmos.matrix = Matrix4x4.TRS(Transform.position, Transform.rotation, Vector3.one);
            if (ChosenAbility is MeleeAttack melee)
            {
                if (melee.hitMethod == MeleeHitMethods.Area)
                {
                    if (!melee.physicsBox.Enabled) return;
                    Vector2 pos = Transform.position.WorldToPlane() + melee.Point;
                    Gizmos.DrawWireCube(pos.PlaneToWorld(), melee.physicsBox.Value.PlaneToWorldBox());
                }
                else if (melee.hitMethod == MeleeHitMethods.Raycast)
                {
                    float distance = melee.maxDistance.Enabled ? melee.maxDistance.Value : 99f;
                    Vector3 pos = melee.Point.PlaneToWorld() + Vector3.up * 0.5f;
                    Vector2 direction = Vector3.forward.WorldToPlane();
                    Gizmos.DrawRay(pos, direction.PlaneToWorld() * distance);
                }
            }
            else if (ChosenAbility is RangedAttack ranged)
            {
                Vector2 spawnPos = ranged.Point;
                float distance = ranged.projectileData.move.moveSpeed;
                Gizmos.DrawLine(spawnPos.PlaneToWorldOffset(), Transform.TransformPoint(Vector3.forward * distance));
                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(spawnPos.PlaneToWorldOffset(), 0.2f);
            }
        }
    #endif    
    }
}