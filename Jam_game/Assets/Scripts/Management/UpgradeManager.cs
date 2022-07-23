using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Gameplay.Entities.PlayerScripts;
using Gameplay.Stats;
using Gameplay.Stats.DataTypes;
using UnityEngine;

namespace Management
{
    public class UpgradeManager : MonoBehaviour
    {
        public static UpgradeManager Instance;
        private void Awake() => Instance = this;

        private Player _player;
        
        private void Start()
        {
            _player = Player.Instance;
        }

        public void ApplyUpgradesToPlayer(List<StatModifier> upgrades)
        {
            // Get all stats on player
            List<GenericStat> allStats = StatSystem.FindAllStatsOnObject(_player);

            // upgrade appropriate stats
            foreach (StatModifier upgrade in upgrades)
            {
                foreach (GenericStat stat in allStats.Where(stat => stat.type == upgrade.targetStatType))
                {
                    stat.AddModifier(upgrade);
                }
            }
        }

    }
}