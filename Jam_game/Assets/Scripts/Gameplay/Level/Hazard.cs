using UnityEngine;

namespace Gameplay.Level
{
    // static hazard. think minecraft cactus
    [RequireComponent(typeof(Collider))]
    public class Hazard : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float knockback;
        
        public int Damage => damage;
        public float Knockback => knockback;
    }
}