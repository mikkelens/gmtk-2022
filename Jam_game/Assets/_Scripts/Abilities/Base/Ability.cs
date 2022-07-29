using Abilities.Data;
using Abilities.Weapons;
using Entities.Base;
using Sirenix.OdinInspector;
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
        public Optional<FloatStat> usageSelfKnockback = (FloatStat)5f;
		public Optional<Vector2> originOffset;
		
        public Optional<AnimationData> usageAnimation;
		public Optional<Texture2D> customCursor;

		protected CombatEntity SourceEntity { get; private set; } // todo: use this to report damage numbers?

		[field: SerializeField]
		[field: ReadOnly]
		public AbilityMetrics Metrics { get; } = new AbilityMetrics();

		public void UseAbility(CombatEntity source)
		{
			SourceEntity = source;
			Use();
		}

		protected abstract void Use();
		
		protected void ImpactEntity(ImpactData impact, Entity entity, Vector2 direction)
		{
			Metrics.AddData(entity.RegisterImpact(impact, direction));
		}
	
	#if UNITY_EDITOR
		private const string MenuPath = "Convert To/";
        private const string Melee = nameof(MeleeWeapon);
        [ContextMenu(MenuPath + Melee, true)]
        public bool IsNotMelee(MenuCommand menuCommand) => menuCommand.context is not MeleeWeapon;
        [ContextMenu(MenuPath + Melee)]
        public void ConvertToMelee(MenuCommand menuCommand) => (menuCommand.context as ScriptableObject).ConvertToType(typeof(MeleeWeapon));
        
        private const string Proj = nameof(ProjectileWeapon);
        [ContextMenu(MenuPath + Proj, true)]
        public bool IsNotProj(MenuCommand menuCommand) => menuCommand.context is not ProjectileWeapon;
        [ContextMenu(MenuPath + Proj)]
        public void ConvertToProj(MenuCommand menuCommand) => (menuCommand.context as ScriptableObject).ConvertToType(typeof(ProjectileWeapon));
    #endif	
	}
}
