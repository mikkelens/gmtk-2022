using System.Collections.Generic;
using System.Linq;
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
        protected override Collider[] CastForEntity(CombatEntity source, Vector2 direction, LayerMask targetLayerMask)
        {
            Vector3 origin = attackPoint.Enabled
                ? attackPoint.Value.position
                : source.transform.position;
            Ray ray = new Ray(origin, direction.PlaneToWorld());
            float maxdistance = maxDistance.Enabled ? maxDistance.Value : float.MaxValue;
            return Physics.RaycastAll(ray, maxdistance, ~targetLayerMask.value).Select(hit => hit.collider).ToArray();
        }

    }
}