using Entities.Base;
using Level;
using Management;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Entities.PlayerScripts
{
    [Tooltip("Player: This is the player script. It also derives from entity scripts.")]
    public partial class Player : AnimatedCombatEntity // main
    {
        public static Player Instance;

        private UIManager _uiManager;

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
            
            Vector2 direction = collision.impulse.WorldToPlane().normalized;
            RegisterImpact(hazard.Impact, direction); // hit itself/player
        }
    }
}
