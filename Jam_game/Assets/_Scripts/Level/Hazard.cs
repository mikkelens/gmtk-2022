using Abilities;
using Abilities.Data;
using UnityEngine;

namespace Level
{
    // static hazard. think minecraft cactus
    [RequireComponent(typeof(Collider))]
    public class Hazard : MonoBehaviour
    {
        [SerializeField] private ImpactData impactEffect;

        public ImpactData Impact => impactEffect;
    }
}