using Gameplay.Entities.Base;
using Gameplay.Level;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Gameplay.Entities.PlayerScripts
{
    [Tooltip("Player: This is the player script. It also derives from entity scripts.")]
    public partial class Player : CombatEntity // main
    {
        public static Player Instance;

        [FoldoutGroup(QuirkCategory)]
        [Header("Player Specific")]
        [SerializeField] private float aimTurnSpeedBonus;

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
            if (hazard == null) return;
            
            HitStats hazardHit = new HitStats(hazard.Damage, hazard.Knockback);
            Vector2 direction = collision.impulse.WorldToPlane().normalized;
            TakeHit(hazardHit, direction); // hit itself/player
        }
    }
}
