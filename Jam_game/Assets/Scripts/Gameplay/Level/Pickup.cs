using Gameplay.Entities;
using Gameplay.Entities.PlayerScripts;
using UnityEngine;

namespace Gameplay.Level
{
    [RequireComponent(typeof(Collider))]
    public class Pickup : MonoBehaviour
    {
        [SerializeField] Upgrade upgradeData;

        private void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponent<Player>();
            if (player == null) return;
            
            upgradeData.UpgradeStats(player.stats);
            // todo: add graphics?
            Despawn();
        }
        
        

        private void Despawn()
        {
            Destroy(gameObject);
        }
    }
}