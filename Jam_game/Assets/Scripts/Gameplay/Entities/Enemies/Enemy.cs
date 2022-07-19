using System.Collections;
using Gameplay.Entities.Base;
using Gameplay.Entities.PlayerScripts;
using Gameplay.Entities.Stats;
using Gameplay.Events;
using Tools;
using UnityEngine;

namespace Gameplay.Entities.Enemies
{
    [Tooltip("Enemy: Acts like a hostile minecraft mob, will search for player and attack.")]
    public class Enemy : CombatEntity
    {
        protected CombatEvent SpawnOrigin;
        protected Player Player;
        
        // protected ;
        private float _lastStunTime;

        protected override bool WantsToAttack => Physics.Raycast(AttackRay, stats.minAttackAttemptDistance, targetLayerMask);
        protected override bool CanMove => base.CanMove && !IsStunned;
        protected virtual bool IsStunned => _lastStunTime.TimeSince() <= stats.stunDuration;
        public void SetSpawnOrigin(CombatEvent origin)
        {
            SpawnOrigin = origin;
        }
        
        protected override void Start()
        {
            base.Start();
            Player = Player.Instance;
        }

        protected override Vector2 GetMoveDirection()
        {
            if (stats.turningAffectsMoveDirection)
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
        
        protected override void StartMelee(Attack attack) // On enemies, attacks are slow animations
        {
            StartCoroutine(MeleeRoutine(attack));
        }
        
        private IEnumerator MeleeRoutine(Attack attack) // Think dark soulds attack with long chargeup
        {
            Stopping = true;
            Animator.SetBool("Walking", false);
            yield return new WaitForSeconds(stats.attackChargeTime);
            TryHitWithAttack(attack);
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
            player.TakeHit(stats.collisionDamage, -collision.impulse.WorldToPlane() * stats.collisionKnockback);
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
            SpawnOrigin.DespawnEnemy(this);
        }
    }
}