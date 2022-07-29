#if UNITY_EDITOR
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Entities.Base;
using Stats.Stat;
using Stats.Stat.Variants;
using Tools;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Attacks
{
    public abstract class Weapon : ExpandableScriptableObject, IStatCollection
    {
        // todo: move hit logic into seperate ways of hiting, hitting happens on weapon child class (eg. RaycastWeapon)
        public Optional<ImpactData> impact;
        
        public Optional<AnimationData> animation;
        public Optional<FloatStat> chargeTime; // should be off for player probably?

        public Optional<IntStat> maxEntitiesHit = (IntStat)1;
        public Optional<FloatStat> cooldown = (FloatStat)1f;
        public Optional<FloatStat> selfKnockbackStrength = (FloatStat)5f;

        public bool TryHitEntity(CombatEntity source, Vector2 direction, LayerMask targetLayerMask)
        {
            Collider[] colliders = CastForEntity(source, direction, targetLayerMask);
            if (colliders.Length == 0) return false;
            
            List<Entity> entities = colliders.Select(collider => collider.GetComponent<Entity>()).Where(entity => entity != null).ToList();
            if (entities.Count == 0) return false;
            Debug.Log($"Weapon {name} hit something.");

            if (maxEntitiesHit.Value <= 1)
                HitEntity(entities.First(), direction);
            else
                entities.ForEach(entity => HitEntity(entity, direction));
            return true;
        }
        private void HitEntity(Entity entity, Vector2 direction)
        {
            if (impact.Enabled) entity.TakeHit(impact.Value, direction);
        }
        // overriden in raycastweapon etc.
        protected abstract Collider[] CastForEntity(CombatEntity source, Vector2 direction, LayerMask targetLayerMask);


    #if UNITY_EDITOR
        private const string MenuPath = "Convert To/";
        private const string RayWeapon = nameof(RaycastWeapon);
        private const string AreaWeapon = nameof(AreaBoxWeapon);
        
        [ContextMenu(MenuPath + RayWeapon, true)]
        public bool IsNotRay(MenuCommand menuCommand) => menuCommand.context is not RaycastWeapon;
        [ContextMenu(MenuPath + RayWeapon)]
        public void ConverToRaycast(MenuCommand menuCommand) => ConvertObjectToType(menuCommand.context as Weapon, typeof(RaycastWeapon));

        [ContextMenu(MenuPath + AreaWeapon, true)]
        public bool IsNotArea(MenuCommand menuCommand) => menuCommand.context is not AreaBoxWeapon;
        [ContextMenu(MenuPath + AreaWeapon)]
        public void ConverToArea(MenuCommand menuCommand) => ConvertObjectToType(menuCommand.context as Weapon, typeof(AreaBoxWeapon));

        private static void ConvertObjectToType(Object target, Type type)
        {
            if (target == null)
            {
                Debug.LogWarning("Target weapon was not found?");
                return;
            }
            string path = AssetDatabase.GetAssetPath(target);
            
            // create new weapon
            Weapon newWeapon = CreateInstance(type) as Weapon;
            foreach (FieldInfo field in typeof(Weapon).GetFields())
            {
                field.SetValue(newWeapon, field.GetValue(target)); // copy data over
            }
            
            int undoGroup = Undo.GetCurrentGroup();
            
            Undo.DestroyObjectImmediate(target); // replace with new
            
            AssetDatabase.CreateAsset(newWeapon, path);
            Undo.RegisterCreatedObjectUndo(newWeapon, $"Created {newWeapon!.GetType().Name}");
            
            AssetDatabase.SaveAssets(); // save changes
            AssetDatabase.Refresh();
            Undo.CollapseUndoOperations(undoGroup);
            
            Selection.objects = new Object[] { newWeapon }; // select object again

            Debug.Log($"Converted {path.PathWithoutDirectory()} to {newWeapon!.GetType().Name}"); // success message
        }
    #endif
    }
}