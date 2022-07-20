using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.CustomStatsSystem
{
    [CreateAssetMenu(fileName = "New Upgrade Package", menuName = "Stat System/Upgrade Package")]
    public class UpgradePackage : ScriptableObject
    {
        public List<Upgrade> Upgrades = new List<Upgrade>();
    }
}