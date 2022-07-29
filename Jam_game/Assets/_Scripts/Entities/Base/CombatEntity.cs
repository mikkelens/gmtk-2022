using System.Collections;
using System.Collections.Generic;
using Abilities.Base;
using Abilities.Spells;
using JetBrains.Annotations;
using Management;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Entities.Base
{
    [Tooltip("Combat Entity: This can hit other entites.")]
    public class CombatEntity : MovingEntity
    {
        [FoldoutGroup(QuirkCategory)]
        [SerializeField] protected LayerMask targetLayerMask;
        [FoldoutGroup(QuirkCategory)]
        [ShowIf("Headless")] // this should only be visible to designer if this entity is a dummy
        [SerializeField] protected bool autoUseAbilities;
        [UsedImplicitly] protected virtual bool Headless => true;
     
        [FoldoutGroup(StatCategory)]
        [SerializeField] protected Optional<Ability> defaultAbility; // class instance with stats in it

        private bool _usingAbility;
        private Ability _lastAbility;
        private Ability _activeAbility;
        public virtual Ability ActiveAbility
        {
            get => _activeAbility == null ? defaultAbility.Enabled ? defaultAbility.Value : null : _activeAbility;
            set => _activeAbility = value; // will be set in some sort of weapon selection menu
        }
        private float _lastAbilityTime;

        // todo: maybe make animation handled in child class?
        protected virtual bool WantsToUseAbility => autoUseAbilities; // will only use "autoAttacks" field if WantsToAttack is not overridden
        private float CooldownAfterDelay => (float)(_lastAbility.activationDelay.Enabled ?_lastAbility.activationDelay.Value : 0f)
                                            + (_lastAbility.cooldown.Enabled ? _lastAbility.cooldown.Value : 0f);
        private bool CanUseAbility => !_usingAbility && (_lastAbility == null || _lastAbilityTime.TimeSince() >= CooldownAfterDelay);
        protected override void EntityUpdate()
        {
            base.EntityUpdate();

            // See if thing in attack box
            if (ActiveAbility != null && WantsToUseAbility && CanUseAbility)
            {
                StartAbilityUse(ActiveAbility);
            }
        } 
        
        // START
        protected virtual void StartAbilityUse(Ability ability) // overridden to add animation
        {
            _usingAbility = true;
            _lastAbility = ability;
            _lastAbilityTime = Time.time;
            if (ability.activationDelay.Enabled)
            {
                StartCoroutine(DelayedAbility(ability));
            }
            else
            {
                ImmediateAbility(ability);
            }
        }
        
        private void ImmediateAbility(Ability ability)
        {
            UseAbility(ability);
        }

        private IEnumerator DelayedAbility(Ability ability) // Think dark soulds attack with long chargeup
        {
            if (!ability.activationDelay.Enabled) yield break;
            Stopping = true;
            Animator.SetBool("Walking", false);
            yield return new WaitForSeconds(ability.activationDelay.Value);
            Animator.SetBool("Walking", true);
            Stopping = false;
            UseAbility(ability);
        }

        private void UseAbility(Ability ability)
        {
            ability.TriggerAbility(this);
            if (ability is Summon summon)
            {
                StartCoroutine(SummonRoutine(summon));
            }
            else
            {
                FinishAbilityUse(ability);
            }
        }

        private IEnumerator SummonRoutine(Summon summon)
        {
            List<EntityData> entitiesToSummon = new List<EntityData>(summon.summonEntities);
            List<Entity> summonedEntities = new List<Entity>();
            while (summonedEntities.Count <= summon.maxSimultaneousSummons.Value && entitiesToSummon.Count > 0)
            {
                if (summon.summonDelayTime.Enabled) yield return new WaitForSeconds(summon.summonDelayTime.Value);
                EntityData data = entitiesToSummon.SelectEntityAsset();
                Vector2 pos = summon.RandomPos;
                Entity entity = Instantiate(data.prefab, pos.PlaneToWorld(), Quaternion.identity, SpawnOrigin.SpawningParent);
                summonedEntities.Add(entity);
                if (summon.summonBehaviour == Summon.SummonBehaviours.SummonEachOnce) entitiesToSummon.Remove(data);
            }
            FinishAbilityUse(summon);
        }

        // END
        protected virtual void FinishAbilityUse(Ability ability)
        {
            _usingAbility = false;
        }
    }
}