using System.Collections.Generic;
using Entities.Players;
using Gameplay.Stats.Stat.Modifier;
using Management;
using Tools;
using UnityEngine;

namespace Level
{
    [RequireComponent(typeof(Collider))]
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private Optional<List<Modifier<float>>> floatModifiers = new List<Modifier<float>>();
        [SerializeField] private Optional<List<Modifier<int>>> intModifiers = new List<Modifier<int>>();
        [SerializeField] private Optional<List<Modifier<bool>>> boolModifiers = new List<Modifier<bool>>();

        private void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponent<Player>();
            if (player == null) return;

            if (floatModifiers.Enabled)
            {
                player.FindAllStatsOnObject<float>().ApplyModifiers(floatModifiers.Value);
            }
            if (intModifiers.Enabled)
            {
                player.FindAllStatsOnObject<int>().ApplyModifiers(intModifiers.Value);
            }
            if (boolModifiers.Enabled)
            {
                player.FindAllStatsOnObject<bool>().ApplyModifiers(boolModifiers.Value);
            }
            // todo: add graphical effect on pickup
            Despawn();
        }

        private void Despawn()
        {
            Destroy(gameObject);
        }
    }
}