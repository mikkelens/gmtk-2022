﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Abilities.Data;
using Entities.Base;
using Stats.Stat;
using Stats.Stat.Variants;
using Tools;
using UnityEditor;
using UnityEngine;

namespace Abilities
{
	public abstract class Ability : ExpandableScriptableObject, IStatCollection
	{
		public const string MenuPath = "Abilities/";
		
		[Header("Ability")]
        public BoolStat continuousUsage = true;

        // these should maybe be off for player melee attacks
        public Optional<FloatStat> activationDelay;
        public Optional<BoolStat> stopOnActivate;
        
		public Optional<FloatStat> cooldown = (FloatStat)1f;
		public Optional<Vector2> originOffset;
		
        public Optional<AnimationData> usageAnimation;
		public Optional<Texture2D> customCursor;
		
		protected CombatEntity SourceEntity { get; private set; } // todo: use this to report damage numbers?

		private float _lastUseTime;
		
		public virtual bool CanUse => _lastUseTime.TimeSince() >= cooldown.Value;

		public Vector2 Point
		{
			get
			{
				if (SourceEntity == null) return originOffset.Value;
				return SourceEntity.transform.position.WorldToPlane() + originOffset.Value;
			}
		}
		public Vector2 Direction => SourceEntity == null ? Vector2.up : SourceEntity.transform.forward.WorldToPlane();

		public IEnumerator TriggerAbility(CombatEntity source)
		{
			SourceEntity = source;
			_lastUseTime = Time.time;
			yield return SourceEntity.StartCoroutine(Use());
		}

		protected abstract IEnumerator Use();

	    protected void ImpactEntities(ImpactData impact, List<Entity> entities, Optional<int> maxHits)
	    {
		    if (maxHits.Enabled && maxHits.Value <= 1)
		    {
			    ImpactEntity(impact, entities.First());
		    }
		    else
		    {
			    for (int i = 0; i < entities.Count; i++)
			    {
				    if (maxHits.Enabled && i >= maxHits.Value) return;
				    Entity entity = entities[i];
				    ImpactEntity(impact, entity);
			    }
		    }
	    }
		protected void ImpactEntity(ImpactData impact, Entity entity, Vector2? direction = null)
		{
			if (direction == null) direction = Direction;
			AddDataToSource(entity.RegisterImpact(impact, direction.Value));
		}
	    public void AddDataToSource(ImpactResultData data)
	    {
		    SourceEntity.Metrics.AddData(data);
	    }
	
	#if UNITY_EDITOR
		private const string ConvertMenuPath = "Convert To/";
        [ContextMenu(ConvertMenuPath + "Melee Weapon", true)]
        public bool IsNotMelee(MenuCommand menuCommand) => menuCommand.context is not MeleeAttack;
        [ContextMenu(ConvertMenuPath + "Melee Weapon")]
        public void ConvertToMelee(MenuCommand menuCommand) => (menuCommand.context as ScriptableObject).ConvertToType(typeof(MeleeAttack));
        [ContextMenu(ConvertMenuPath + "Projectile Weapon", true)]
        public bool IsNotProj(MenuCommand menuCommand) => menuCommand.context is not RangedAttack;
        [ContextMenu(ConvertMenuPath + "Projectile Weapon")]
        public void ConvertToProj(MenuCommand menuCommand) => (menuCommand.context as ScriptableObject).ConvertToType(typeof(RangedAttack));
    #endif
	}
}
