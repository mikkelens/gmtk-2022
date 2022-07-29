using System.Collections;
using Abilities;
using Abilities.Data;
using Abilities.Weapons;
using Entities.Base;
using Entities.Players;
using Management;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Entities.Enemies
{
    [Tooltip("Enemy: Acts like a hostile minecraft mob, will search for player and attack.")]
    public class Enemy : AnimatedCombatEntity
    {
        protected Player Player;
        
        [Header("Enemy Specific")]
        [FoldoutGroup("Quirks")]
        [SerializeField] protected bool rotationAffectsMoveDirection; // todo: this should only apply to an extent
        [FoldoutGroup("Quirks")]
        [SerializeField] protected float minAttackAttemptDistance; // todo: should be weapon specific
        [FoldoutGroup("Quirks")]
        [SerializeField] protected Optional<Transform> customTargetingOrigin; // todo: should be weapon specific
        
        [Header("Enemy Specific")]
        [FoldoutGroup("Stats")]
        [SerializeField] protected Optional<ImpactData> collisionImpact;

        protected override bool Headless => false;
        protected Ray TargetRay
        {
            get
            {
                Transform originTransform = customTargetingOrigin.Enabled ? customTargetingOrigin.Value : transform;
                return new Ray(originTransform.position, transform.forward);
            }
        }
        protected override bool WantsToAttack => Physics.Raycast(TargetRay, minAttackAttemptDistance, targetLayerMask);
        protected override bool CanMove => base.CanMove && !stunned;

        protected override void Start()
        {
            base.Start();
            Player = Player.Instance;
        }

        protected override Vector2 GetMoveDirection()
        {
            return rotationAffectsMoveDirection ? Transform.forward.WorldToPlane() : GetTargetMoveDirection();
        }
        protected override Vector2 GetTargetMoveDirection()
        {
            // walk towards player
            Vector2 pos = Transform.position.WorldToPlane();
            Vector2 playerPos = Player.transform.position.WorldToPlane();
            return (playerPos - pos).normalized;
        }
        
        protected override void UseWeapon(MeleeWeapon weapon) // On enemies, attacks are slow animations
        {
            StartCoroutine(MeleeRoutine(weapon));
        }
        
        private IEnumerator MeleeRoutine(MeleeWeapon weapon) // Think dark soulds attack with long chargeup
        {
            if (!weapon.activationDelay.Enabled) yield break;
            Stopping = true;
            Animator.SetBool("Walking", false);
            yield return new WaitForSeconds(weapon.activationDelay.Value);
            TryHitWithWeapon(weapon);
            Animator.SetBool("Walking", true);
            Stopping = false;
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (!collisionImpact.Enabled) return;
            
            Entity entity = collision.gameObject.GetComponent<Entity>();
            if (entity == null) return;
        
            Player player = entity as Player; // filter contact to only be player
            if (player == null) return;

            Vector2 collisionDirection = -collision.impulse.WorldToPlane().normalized;
            player.RegisterImpact(collisionImpact.Value, collisionDirection);
        }

        public override void KillThis()
        {
            base.KillThis();
            GameManager.Instance.IncreaseKillcount();
        }
    }
}