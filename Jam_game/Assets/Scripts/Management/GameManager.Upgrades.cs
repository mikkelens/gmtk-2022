using System.Collections.Generic;
using System.Linq;
using Gameplay.Entities.Players;
using Gameplay.Input;
using Gameplay.Stats;
using Gameplay.Stats.DataTypes;
using UnityEngine;

namespace Management
{
    public partial class GameManager
    {
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