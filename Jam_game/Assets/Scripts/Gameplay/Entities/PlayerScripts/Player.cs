using Gameplay.Entities.Base;
using Gameplay.Level;
using Tools;
using UnityEngine;

namespace Gameplay.Entities.PlayerScripts
{
    [Tooltip("Player: This is the player script. It also derives from entity scripts.")]
    public partial class Player : CombatEntity // main
    {
        public static Player Instance;

        protected override void Awake()
        {
            Instance = this;
            base.Awake();
        }
        
        // movement is decided by input set in "Player.Input.cs"

        private void OnCollisionEnter(Collision collision)
        {
            GameObject other = collision.gameObject;
            
            Hazard hazard = other.GetComponent<Hazard>();
            if (hazard != null)
            {
                Vector2 direction = collision.impulse.WorldToPlane().normalized;
                TakeHit(hazard.Damage, direction * hazard.Knockback);
            }
        }
    }
}
