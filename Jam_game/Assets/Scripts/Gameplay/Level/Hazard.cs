using Gameplay.Entities;
using Gameplay.Stats.Attacks;
using Tools;
using UnityEngine;

namespace Gameplay.Level
{
    // static hazard. think minecraft cactus
    [RequireComponent(typeof(Collider))]
    public class Hazard : MonoBehaviour
    {
        [SerializeField] private HitStats hitEffect;

        public HitStats Hit => hitEffect;
    }
}