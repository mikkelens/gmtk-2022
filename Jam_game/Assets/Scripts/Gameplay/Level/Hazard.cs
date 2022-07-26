using Gameplay.Attacks;
using UnityEngine;

namespace Gameplay.Level
{
    // static hazard. think minecraft cactus
    [RequireComponent(typeof(Collider))]
    public class Hazard : MonoBehaviour
    {
        [SerializeField] private ImpactData impactEffect;

        public ImpactData Impact => impactEffect;
    }
}