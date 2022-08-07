using Entities.PlayerScripts;
using Game;
using Stats.Stat.Modifier;
using Tools;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(Collider))]
    public class PickupComponent : MonoBehaviour
    {
        [SerializeField] private Optional<Effect> modifiers;

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