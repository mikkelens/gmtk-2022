using Gameplay.Entities.Player;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Gameplay.Entities.Enemies
{
    public class Enemy : CombatEntity
    {
        protected PlayerController Player;
        
        private Vector2 _velocity;

        public override void Start()
        {
            base.Start();
            Player = Manager.player;
        }

        public override Vector2 GetTargetMoveDirection()
        {
            // walk towards player
            Vector2 pos = Transform.position.WorldToPlane();
            Vector2 playerPos = Player.transform.position.WorldToPlane();
            return (playerPos - pos).normalized;
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player == null) return;
            
            player.TakeHit(damage, Vector2.zero);
        }
    }
}