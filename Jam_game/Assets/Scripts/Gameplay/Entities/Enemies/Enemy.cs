using System.Collections;
using Gameplay.Entities.Base;
using Gameplay.Entities.Players;
using Gameplay.Stats.Attacks;
using Gameplay.Stats.DataTypes;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Gameplay.Entities.Enemies
{
    [Tooltip("Enemy: Acts like a hostile minecraft mob, will search for player and attack.")]
    public class Enemy : CombatEntity
    {
        protected Player Player;
        
        [Header("Enemy Specific")]
        [FoldoutGroup("Quirks")]
        [SerializeField] protected bool turningAffectsMoveDirection;
        [FoldoutGroup("Quirks")]
        [SerializeField] protected float minAttackAttemptDistance;
        [FoldoutGroup("Quirks")]
        [SerializeField] protected float attackChargeTime;
        
        [Header("Enemy Specific")]
        [FoldoutGroup("Stats")]
        [SerializeField] protected Optional<FloatStat> stunDuration;
        [FoldoutGroup("Stats")]
        [SerializeField] protected HitStats collisionHit;

        private float _lastStunTime;
        protected override bool WantsToAttack => Physics.Raycast(AttackRay, minAttackAttemptDistance, targetLayerMask);
        protected override bool CanMove => base.CanMove && !IsStunned;
        protected virtual bool IsStunned => _lastStunTime.TimeSince() <= stunDuration.Value;

        protected override void Start()
        {
            base.Start();
            Player = Player.Instance;
        }

        protected override Vector2 GetMoveDirection()
        {
            if (turningAffectsMoveDirection)
                return Transform.forward.WorldToPlane();
            return GetTargetMoveDirection();
        }
        protected override Vector2 GetTargetMoveDirection()
        {
            // walk towards player
            Vector2 pos = Transform.position.WorldToPlane();
            Vector2 playerPos = Player.transform.position.WorldToPlane();
            return (playerPos - pos).normalized;
        }
        
        protected override void StartUseWeapon(Weapon weapon) // On enemies, attacks are slow animations
        {
            StartCoroutine(MeleeRoutine(weapon));
        }
        
        private IEnumerator MeleeRoutine(Weapon weapon) // Think dark soulds attack with long chargeup
        {
            Stopping = true;
            Animator.SetBool("Walking", false);
            yield return new WaitForSeconds(attackChargeTime);
            TryHitWithWeapon(weapon);
            Animator.SetBool("Walking", true);
            Stopping = false;
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            GameObject other = collision.gameObject;
            
            Entity entity = other.GetComponent<Entity>();
            if (entity == null) return;
        
            Player player = entity as Player; // filter contact to only be player
            if (player == null) return;

            Vector2 collisionDirection = -collision.impulse.WorldToPlane().normalized;
            player.TakeHit(collisionHit, collisionDirection);
        }

        protected override void ApplyKnockback(Vector2 force)
        {
            base.ApplyKnockback(force);
            ApplyEnemyStun();
        }
        private void ApplyEnemyStun()
        {
            _lastStunTime = Time.time;
        }
        public override void KillThis()
        {
            base.KillThis();
            Manager.IncreaseKillcount();
        }
    }
}