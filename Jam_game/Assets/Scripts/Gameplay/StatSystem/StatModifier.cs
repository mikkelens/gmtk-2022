using System;

namespace Gameplay.StatSystem
{
    [Serializable]
    public class StatModifier
    {
        public float value = 0;
        public ModificationTypes modificationType = ModificationTypes.Flat;
        public StatType targetStatType; // scriptable object

        public int Order => (int)modificationType;
    }
}