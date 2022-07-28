using System.Reflection;
using Gameplay.Entities.Base;
using Gameplay.Stats.Stat;
using Gameplay.Stats.Stat.Variants;
using Tools;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Gameplay.Attacks
{
    public abstract class Weapon : ExpandableScriptableObject, IStatCollection
    {
        // todo: move hit logic into seperate ways of hiting, hitting happens on weapon child class (eg. RaycastWeapon)
        public Optional<ImpactData> impact;
        
        public Optional<AnimationData> animation;
        public Optional<FloatStat> chargeTime; // should be off for player probably?
        
        public Optional<FloatStat> cooldown;
        public Optional<FloatStat> selfKnockbackStrength;

        public bool TryHitEntity(CombatEntity source, Vector2 direction, LayerMask targetLayerMask)
        {
            Entity entity = CastForEntity(source, direction, targetLayerMask);
            HitEntity(entity, direction);
            return true;
        }
        private void HitEntity(Entity entity, Vector2 direction)
        {
            if (impact.Enabled) entity.TakeHit(impact.Value, direction);
        }
        // overriden in raycastweapon etc.
        protected abstract Entity CastForEntity(CombatEntity source, Vector2 direction, LayerMask targetLayerMask);


    #if UNITY_EDITOR
        [ContextMenu("Convert/To other weapon")]
        public void ConvertToOtherWeapon()
        {
            Weapon target = Selection.GetFiltered<Weapon>(SelectionMode.Assets)[0];
            string path = AssetDatabase.GetAssetPath(target).PathWithoutAsset();
            
            // create new prefab
            RaycastWeapon raycastWeapon = CreateInstance(typeof(RaycastWeapon)) as RaycastWeapon;
            Debug.Log($"Path: {path}");
            AssetDatabase.CreateAsset(raycastWeapon, path);
            
            // copy data over
            foreach (FieldInfo field in typeof(Weapon).GetFields())
            {
                field.SetValue(raycastWeapon, field.GetValue(target));
            }
        }
    #endif
    }
}