using Gameplay.Entities;
using Gameplay.Entities.PlayerScripts;
using Gameplay.Entities.Stats;
using UnityEngine;

namespace Gameplay.Level
{
    [RequireComponent(typeof(Collider))]
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private Upgrade data;

        private void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponent<Player>();
            if (player == null) return;
            
            data.UpgradeStats(player.stats);
            Debug.Log("Pickup: " + data.upgradeName);
            // todo: add graphics?
            Despawn();
        }
        
        

        private void Despawn()
        {
            Destroy(gameObject);
        }
    }
}