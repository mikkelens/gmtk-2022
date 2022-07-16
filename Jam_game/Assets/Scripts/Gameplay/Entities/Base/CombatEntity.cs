using System;
using System.Collections;
using Gameplay.Entities.Enemies;
using Tools;
using UnityEngine;

namespace Gameplay.Entities.Base
{
    [Tooltip("Combat Entity: This can hit other entites.")]
    public class CombatEntity : MovableEntity
    {
        [SerializeField] protected int damage = 1;
        [SerializeField] protected float knockbackStrength = 1.5f;
        [SerializeField] protected float meleeDistance = 2.5f;
        [SerializeField] protected float meleeAttackDelay = 0.75f; // could be automated using animation time info maybe actually but i dont wanna do it
        [SerializeField] protected float meleeCooldown = 1.25f;
        [SerializeField] protected bool autoAttacks = true; // should be false on player

        protected int targetLayerMask;
        protected float LastAttackTime;

        protected virtual bool WantsToAttack => autoAttacks;
        protected virtual bool CanAttack => LastAttackTime.TimeSince() >= meleeCooldown;

        private void Awake()
        {
            string target = GetType() == typeof(Enemy) ? "Player" : "Enemy";
            targetLayerMask = LayerMask.GetMask(target);
        }

        public override void Update()
        {
            base.Update();
            
            // See thing in attack box
            if (WantsToAttack && CanAttack)
            {
                LastAttackTime = Time.time;
                StartAttack();
            }
        }

        public virtual void StartAttack()
        {
            StartCoroutine(MeleeAttack());
        }

        private IEnumerator MeleeAttack()
        {
            Animator.SetBool("Walking", false);
            Stopping = true;
            yield return new WaitForSeconds(meleeAttackDelay);
            TryMelee();
            Animator.SetBool("Walking", true);
        }

        protected void TryMelee()
        {
            // Raycast for hit
            // Within distance?
            Ray ray = new Ray(Transform.position, Transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hitData, meleeDistance, targetLayerMask))
            {
                HitOther(hitData.transform.GetComponent<Entity>());
            }
            Stopping = false;
        }

        public virtual void OnCollisionEnter(Collision collision)
        {
            GameObject other = collision.gameObject;
            
            Entity entity = other.GetComponent<Entity>();
            if (entity != null) ContactWith(entity);
        }
        
        public virtual void ContactWith(Entity entity)
        {
            HitOther(entity);
        }
        public virtual void HitOther(Entity entity)
        {
            Vector2 knockbackDirection = GetTargetLookDirection() * knockbackStrength;
            entity.TakeHit(damage, knockbackDirection);
        }
    }
}