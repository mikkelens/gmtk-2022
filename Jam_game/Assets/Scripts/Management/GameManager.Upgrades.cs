using System.Collections.Generic;
using System.Linq;
using Gameplay.Stats;
using Gameplay.Stats.DataTypes;

namespace Management
{
    public partial class GameManager
    {
        public void ApplyUpgradesToPlayer(List<StatModifier> upgrades)
        {
            // Get all stats on player
            IEnumerable<GenericStat> allStats = StatSystem.FindAllStatsOnObject(_player);

            // upgrade appropriate stats
            foreach (GenericStat stat in allStats.Where(stat => stat.type != null))
            {
                foreach (StatModifier upgrade in upgrades)
                {
                    if (upgrade.targetStatType != null)
                    {
                        if (upgrade.targetStatType == stat.type) stat.AddModifier(upgrade);
                    }
                    else if (upgrade.targetStatTypeCollection != null)
                    {
                        StatTypeCollection upgradeTypes = upgrade.targetStatTypeCollection;
                        if (upgradeTypes.statTypes == null) continue;
                        if (upgradeTypes.statTypes.Contains(stat.type)) stat.AddModifier(upgrade);
                    }
                }
            }
        }
    }
}