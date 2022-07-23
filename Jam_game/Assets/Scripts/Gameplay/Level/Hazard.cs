using Tools;
using UnityEngine;

namespace Gameplay.Level
{
    // static hazard. think minecraft cactus
    [RequireComponent(typeof(Collider))]
    public class Hazard : MonoBehaviour
    {
        [SerializeField] private Optional<int> damage;
        [SerializeField] private Optional<float> knockback;
        
        public Optional<int> Damage => damage;
        public Optional<float> Knockback => knockback;
    }
}