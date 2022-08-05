using Abilities.Data;
using UnityEngine;

namespace Components
{
    // static hazard. think minecraft cactus
    [RequireComponent(typeof(Collider))]
    public class HazardComponent : MonoBehaviour
    {
        [SerializeField] private ImpactData impactEffect;

        public ImpactData Impact => impactEffect;
    }
}