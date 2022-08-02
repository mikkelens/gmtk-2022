using System.Collections;
using Abilities;
using Abilities.Attacks;
using JetBrains.Annotations;
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
        [FoldoutGroup(QuirkCategory)]
        [ShowIf("Headless")] // this should only be visible to designer if this entity is a dummy
        [SerializeField] protected bool autoUseAbilities;
        [UsedImplicitly] protected virtual bool Headless => true;
     
        [FoldoutGroup(StatCategory)]
        [SerializeField] protected Optional<Kit> defaultKit; // class instance with stats in it

        private Kit _activeKit;
        protected Kit ActiveKit => _activeKit ?? (defaultKit.Enabled ? defaultKit.Value : null);

        private Ability _activeAbility;
        protected virtual Ability ActiveAbility { get; set; }

        // todo: maybe make animation handled in child class?
        protected override void EntityUpdate()
        {
            base.EntityUpdate();

            // See if thing in attack box
            if (ActiveAbility == null) return;
            ActiveAbility = GetAbilityToUse();
            if (ActiveAbility == null || !ActiveAbility.CanUse) return;
            StartAbilityUse();
        }

        public abstract Ability GetAbilityToUse();
        
        // START
        protected virtual void StartAbilityUse() // overridden to add animation
        {
            StartCoroutine(UseAbility());
        }

        private IEnumerator UseAbility() // Think dark soulds attack with long chargeup
        {
            Stopping = true;
            Animator.SetBool("Walking", false);
            yield return StartCoroutine(ActiveAbility.TriggerAbility(this));
            Animator.SetBool("Walking", true);
            Stopping = false;
            FinishAbilityUse();
        }

        // END
        protected virtual void FinishAbilityUse()
        {
            
        }
        
    #if UNITY_EDITOR
        private void OnDrawGizmos() // showing area check box
        {
            if (ActiveAbility != null) return;
            Gizmos.color = Color.red;
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
            if (ActiveAbility is MeleeAttack melee)
            {
                if (melee.hitMethod == MeleeHitMethods.Area)
                {
                    if (!melee.physicsBox.Enabled) return;
                    Vector2 pos = transform.position.WorldToPlane() + melee.Point;
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
        }
    #endif    
    }
}