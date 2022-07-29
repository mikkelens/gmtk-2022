using System.Linq;
using Entities.Base;
using Stats.Stat.Variants;
using Tools;
using UnityEngine;

namespace Abilities.Weapons
{
    // Default weapon
    [CreateAssetMenu(fileName = "New RaycastWeapon", menuName = "Stats/RaycastWeapon")]
    public class RaycastMeleeWeapon : MeleeWeapon
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

        public override void UseAbility()
        {
            throw new System.NotImplementedException();
        }
    }
}