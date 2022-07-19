using System;
using UnityEngine;

namespace Gameplay.CustomStatsSystem
{
    [Serializable]
    public class Upgrade
    {
        public float value;
        public UpgradeTypes type;
        
        public Upgrade(float value = 0, UpgradeTypes type = UpgradeTypes.Flat)
        {
            this.value = value;
            this.type = type;
        }
    }
}