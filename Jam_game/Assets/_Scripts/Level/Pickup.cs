using Entities.Players;
using Management;
using Stats.Stat.Modifier;
using Tools;
using UnityEngine;

namespace Level
{
    [RequireComponent(typeof(Collider))]
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private Optional<ModifierCollection> modifiers;

        private void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponent<Player>();
            if (player == null) return;

            if (!modifiers.Enabled) return;
            player.ApplyModifierCollectionToObject(modifiers.Value);

            Debug.Log($"Player picked up pickup: {name}");
            // todo: add graphical effect on pickup
            Despawn();
        }

        private void Despawn()
        {
            Destroy(gameObject);
        }
    }
}