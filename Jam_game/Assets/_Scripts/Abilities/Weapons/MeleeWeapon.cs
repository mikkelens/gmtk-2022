using System.Collections.Generic;
using System.Linq;
using Abilities.Base;
using Abilities.Data;
using Entities.Base;
using Sirenix.OdinInspector;
using Stats.Stat.Variants;
using Tools;
#if UNITY_EDITOR
#endif
using UnityEngine;

namespace Abilities.Weapons
{
    [CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Abilities/Melee Weapon")]
    public class MeleeWeapon : Weapon
    {
        public Optional<ImpactData> impact;
        public MeleeHitMethods hitMethod;
        [ShowIf("@hitMethod == MeleeHitMethods.Area")]
        public Optional<Vector2> physicsBox;
        [ShowIf("@hitMethod == MeleeHitMethods.Raycast")]
        public Optional<FloatStat> maxDistance;

        protected override void Use()
        {
            if (TryHitEntity(SourceEntity, targetMask.Value))
                base.Use();
        }
        
        private bool TryHitEntity(CombatEntity source, LayerMask targetLayerMask)
        {
            Collider[] colliders;
            if (hitMethod == MeleeHitMethods.Raycast)
                colliders = RaycastCheck(source, targetLayerMask);
            else
                colliders = AreaCheck(source, targetLayerMask);
            if (colliders.Length == 0) return false;
            
            List<Entity> entities = colliders.Select(collider => collider.GetComponent<Entity>()).Where(entity => entity != null).ToList();
            if (entities.Count == 0) return false;
            Debug.Log($"Weapon {name} hit something.");

            ImpactEntities(impact.Value, entities, AttackDirection);
            return true;
        }

    #region Check methods
        private Collider[] AreaCheck(CombatEntity source, LayerMask targetLayerMask)
        {
            Vector2 center = originOffset.Enabled ? originOffset.Value : source.transform.position.WorldToPlane();
            return Physics.OverlapBox(center.PlaneToWorld(), Vector3.one, Quaternion.identity, ~targetLayerMask.value);
        }
        private Collider[] RaycastCheck(CombatEntity source, LayerMask targetLayerMask)
        {
            Ray ray = new Ray(AttackPoint.PlaneToWorld(), AttackDirection.PlaneToWorld());
            float maxdistance = maxDistance.Enabled ? maxDistance.Value : float.MaxValue;
            return Physics.RaycastAll(ray, maxdistance, ~targetLayerMask.value).Select(hit => hit.collider).ToArray();
        }
    #endregion
    }
}