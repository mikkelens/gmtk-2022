using System.Collections.Generic;
using Gameplay.Entities.Players;
using Gameplay.Stats;
using Management;
using UnityEngine;

namespace Gameplay.Level
{
    [RequireComponent(typeof(Collider))]
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private List<StatModifier> modifiers = new List<StatModifier>();

        private void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponent<Player>();
            if (player == null) return;
            
            ApplyUpgrades();
            // todo: add graphical effect on pickup
            Despawn();
        }

        private void ApplyUpgrades()
        {
            UpgradeManager.Instance.ApplyUpgradesToPlayer(modifiers);
        }

        private void Despawn()
        {
            Destroy(gameObject);
        }
    }
}