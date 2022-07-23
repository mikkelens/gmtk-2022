using System.Collections.Generic;
using System.Linq;
using Gameplay.Entities.PlayerScripts;
using Gameplay.StatSystem;
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
            List<GenericStat> allStats = typeof(Player).GetFields().ToList().ConvertAll(fieldInfo => fieldInfo.GetValue(_player) as GenericStat);
            foreach (StatModifier upgrade in upgrades)
            {
                foreach (GenericStat stat in allStats)
                {
                    if (stat.type != upgrade.targetStatType) continue;
                    stat.AddModifier(upgrade);
                }
            }
        }
    }
}