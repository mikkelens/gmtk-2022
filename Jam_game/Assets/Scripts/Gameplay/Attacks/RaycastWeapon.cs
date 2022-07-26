using Gameplay.Entities.Base;
using Gameplay.Stats.Stat.Variants;
using Tools;
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
    }
}