using System.Reflection;
using Gameplay.Entities.Base;
using Gameplay.Stats.Stat.Variants;
using Tools;
using UnityEditor;
using UnityEngine;

namespace Gameplay.Attacks
{
    // Default weapon
    [CreateAssetMenu(fileName = "New RaycastWeapon", menuName = "Stats/RaycastWeapon")]
    public class RaycastWeapon : Weapon
    {
        public Optional<Transform> attackPoint;
        public Optional<FloatStat> maxDistance;

        // Will be different for weapons that are not raycast weapons
        protected override Entity CastForEntity(CombatEntity source, Vector2 direction, LayerMask targetLayerMask)
        {
            Vector2 origin = attackPoint.Enabled
                ? attackPoint.Value.position.WorldToPlane()
                : source.transform.position.WorldToPlane();
            
            Ray ray = new Ray(origin.PlaneToWorld(), direction.PlaneToWorld());
            float maxdistance = maxDistance.Enabled ? maxDistance.Value : float.MaxValue;
            if (Physics.Raycast(ray, out RaycastHit hitData, maxdistance, targetLayerMask.value))
            {
                return hitData.transform.GetComponent<Entity>();
            }
            return null;
        }

        [MenuItem("Assets/Convert/To other weapon (WIP)")]
        public static void ConvertToOtherWeapon(MenuCommand menuCommand)
        {
            
        }

        public static RaycastWeapon ConvertToRaycast(Weapon weapon) // todo: make generic/handle all objects
        {
            RaycastWeapon raycastWeapon = CreateInstance(typeof(RaycastWeapon)) as RaycastWeapon;
            foreach (FieldInfo field in typeof(Weapon).GetFields())
            {
                field.SetValue(raycastWeapon, field.GetValue(weapon));
            }
            return raycastWeapon;
        }
    }
}