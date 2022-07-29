#if UNITY_EDITOR
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abilities.Base;
using Abilities.Data;
using Entities.Base;
using Tools;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Abilities.Weapons
{
    public abstract class MeleeWeapon : Weapon
    {
        public Optional<ImpactData> impact;
        public HitMethods hitMethod; // todo: unify area and raycast
        [Serializable]
        public enum HitMethods
        {
            Raycast,
            Area
        }

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
            if (impact.Enabled) entity.RegisterImpact(impact.Value, direction);
        }
        // overriden in raycastweapon etc.
        protected abstract Collider[] CastForEntity(CombatEntity source, Vector2 direction, LayerMask targetLayerMask);

        
        
    #if UNITY_EDITOR
        private const string MenuPath = "Convert To/";
        private const string RayWeapon = nameof(RaycastMeleeWeapon);
        private const string AreaWeapon = nameof(AreaMeleeWeapon);
        
        [ContextMenu(MenuPath + RayWeapon, true)]
        public bool IsNotRay(MenuCommand menuCommand) => menuCommand.context is not RaycastMeleeWeapon;
        [ContextMenu(MenuPath + RayWeapon)]
        public void ConverToRaycast(MenuCommand menuCommand) => ConvertObjectToType(menuCommand.context as MeleeWeapon, typeof(RaycastMeleeWeapon));

        [ContextMenu(MenuPath + AreaWeapon, true)]
        public bool IsNotArea(MenuCommand menuCommand) => menuCommand.context is not AreaMeleeWeapon;
        [ContextMenu(MenuPath + AreaWeapon)]
        public void ConverToArea(MenuCommand menuCommand) => ConvertObjectToType(menuCommand.context as MeleeWeapon, typeof(AreaMeleeWeapon));
    #endif
    }
}