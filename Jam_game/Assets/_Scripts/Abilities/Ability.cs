using System.Collections;
using Abilities.Attacks;
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
		protected const string MenuPath = "Abilities/";
		
        public Optional<LayerMask> targetMask;
        
        // use delay, should be off for player melee attacks
        public Optional<FloatStat> activationDelay;
        public Optional<BoolStat> stopOnActivate;
        
		public Optional<FloatStat> cooldown = (FloatStat)1f;
		public Optional<Vector2> originOffset;
		
        public Optional<AnimationData> usageAnimation;
		public Optional<Texture2D> customCursor;
		
		protected CombatEntity SourceEntity { get; private set; } // todo: use this to report damage numbers?
		protected int HitMask => targetMask.Enabled ? targetMask.Value.value : Physics.AllLayers;

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

		protected void ImpactEntity(ImpactData impact, Entity entity)
		{
			AddDataToSource(entity.RegisterImpact(impact, Direction));
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
        public bool IsNotProj(MenuCommand menuCommand) => menuCommand.context is not ProjectileAttack;
        [ContextMenu(ConvertMenuPath + "Projectile Weapon")]
        public void ConvertToProj(MenuCommand menuCommand) => (menuCommand.context as ScriptableObject).ConvertToType(typeof(ProjectileAttack));
    #endif
	}
}
