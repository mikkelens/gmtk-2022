using System.Collections;
using Gameplay.Entities.Base;
using Gameplay.Entities.PlayerScripts;
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

        protected override bool WantsToAttack => Physics.Raycast(AttackRay, myStats.meleeDistance, targetLayerMask);
        protected override bool CanMove => base.CanMove && !IsStunned;
        protected virtual bool IsStunned => _lastStunTime.TimeSince() <= myStats.stunDuration;
        public void SetSpawnOrigin(CombatEvent origin)
        {
            SpawnOrigin = origin;
        }
        
        protected override void Start()
        {
            base.Start();
            Player = Player.Instance;
        }

        protected override Vector2 GetTargetMoveDirection()
        {
            // walk towards player
            Vector2 pos = Transform.position.WorldToPlane();
            Vector2 playerPos = Player.transform.position.WorldToPlane();
            return (playerPos - pos).normalized;
        }
        
        protected override void StartMelee() // On enemies, attacks are slow animations
        {
            StartCoroutine(MeleeRoutine());
        }
        
        private IEnumerator MeleeRoutine() // Think dark soulds attack with long chargeup
        {
            Animator.SetBool("Walking", false);
            Stopping = true;
            yield return new WaitForSeconds(myStats.meleeAttackDelay);
            TryMelee();
            Animator.SetBool("Walking", true);
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            GameObject other = collision.gameObject;
            
            Entity entity = other.GetComponent<Entity>();
            if (entity != null) ContactWith(entity);
        }

        private void ContactWith(Entity entity) // filter contact to only be player
        {
            Player player = entity as Player;
            if (player != null) HitOther(player);
        }

        protected override void ApplyKnockback(Vector2 force)
        {
            base.ApplyKnockback(force);
            ApplyStun();
        }

        private void ApplyStun()
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