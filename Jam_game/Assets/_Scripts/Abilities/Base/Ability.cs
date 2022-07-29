using Abilities.Data;
using Abilities.Weapons;
using Entities.Base;
using Management;
using Stats.Stat;
using Stats.Stat.Variants;
using Tools;
using UnityEditor;
using UnityEngine;

namespace Abilities.Base
{
	public abstract class Ability : ExpandableScriptableObject, IStatCollection
	{
        public Optional<LayerMask> targetMask;
		
		public Optional<FloatStat> activationDelay; // should be off for player probably?
		public Optional<FloatStat> cooldown = (FloatStat)1f;
		public Optional<Vector2> originOffset;
		
        public Optional<AnimationData> usageAnimation;
		public Optional<Texture2D> customCursor;
		public Optional<ImpactData> selfImpact;

		public AbilityMetrics Metrics { get; } = new AbilityMetrics();
		protected CombatEntity SourceEntity { get; private set; } // todo: use this to report damage numbers?

		public Vector2 AttackPoint
		{
			get
			{
				if (SourceEntity == null) return originOffset.Value;
				return SourceEntity.transform.position.WorldToPlane() + originOffset.Value;
			}
		}
		public Vector2 AttackDirection
		{
			get
			{
				if (SourceEntity == null) return Vector2.up;
				return SourceEntity.transform.forward.WorldToPlane();
			}
		}

		public void TriggerAbility(CombatEntity source)
		{
			SourceEntity = source;
			Use();
		}

		protected virtual void Use()
		{
			if (selfImpact.Enabled)
				ImpactEntity(selfImpact.Value, SourceEntity, SourceEntity.transform.forward.WorldToPlane());
			if (selfImpact.Value.effects.Enabled) 
				SourceEntity.ApplyModifierCollectionToObject(selfImpact.Value.effects.Value);
		}
		
		protected void ImpactEntity(ImpactData impact, Entity entity, Vector2 direction)
		{
			Metrics.AddData(entity.RegisterImpact(impact, direction));
		}
	
	#if UNITY_EDITOR
		private const string MenuPath = "Convert To/";
        [ContextMenu(MenuPath + "Melee Weapon", true)]
        public bool IsNotMelee(MenuCommand menuCommand) => menuCommand.context is not MeleeWeapon;
        [ContextMenu(MenuPath + "Melee Weapon")]
        public void ConvertToMelee(MenuCommand menuCommand) => (menuCommand.context as ScriptableObject).ConvertToType(typeof(MeleeWeapon));
        [ContextMenu(MenuPath + "Projectile Weapon", true)]
        public bool IsNotProj(MenuCommand menuCommand) => menuCommand.context is not ProjectileWeapon;
        [ContextMenu(MenuPath + "Projectile Weapon")]
        public void ConvertToProj(MenuCommand menuCommand) => (menuCommand.context as ScriptableObject).ConvertToType(typeof(ProjectileWeapon));
    #endif	
	}
}
