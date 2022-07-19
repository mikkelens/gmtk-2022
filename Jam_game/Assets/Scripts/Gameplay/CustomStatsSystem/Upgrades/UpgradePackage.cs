using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.CustomStatsSystem
{
    [CreateAssetMenu(fileName = "New Upgrade", menuName = "Stats/Upgrade")]
    public class UpgradePackage : ScriptableObject
    {
        [SerializeField] private List<Upgrade> upgrades = new List<Upgrade>();
        
        public void Apply(Player)
    }
}