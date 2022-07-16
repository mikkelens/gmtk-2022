using Gameplay.Entities.PlayerScripts;
using Tools;
using UnityEngine;

namespace Gameplay.Entities.Enemies
{
    public class Enemy : CombatEntity
    {
        protected Player Player;
        
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

        public override void OnEntityContact(Entity entity) // filter contact to only be player
        {
            Player player = entity as Player;
            if (player == null) return;
            HitOther(player);
        }
    }
}