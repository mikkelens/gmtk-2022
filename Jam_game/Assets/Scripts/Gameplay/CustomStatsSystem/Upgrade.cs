using UnityEngine;

namespace Gameplay.CustomStatsSystem
{
    public class Upgrade
    {
        public float Value;
        public ModificationTypes Type;
        public Stat StatToUpgrade; // scriptable object
        
        public Upgrade(float value = 0, ModificationTypes type = ModificationTypes.Flat)
        {
            Value = value;
            Type = type;
        }

        public int Order => (int)Type;
    }
}