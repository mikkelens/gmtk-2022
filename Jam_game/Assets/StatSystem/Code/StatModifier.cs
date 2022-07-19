using System;

namespace StatSystem.Code
{
    [Serializable]
    public class StatModifier
    {
        public readonly float Value;
        public readonly StatModType Type;
        public readonly int Order;
        public readonly object Source;
        
        public StatModifier(float value, StatModType type, int order, object source)
        {
            Value = value;
            Type = type;
            Order = order;
            Source = source;
        }
        // constructor overrides
        public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { } // enables a default order using type enum, leaves source unspecified
        public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { } // specifies order but doesnt specificy source
        public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { } // specifies source but doesnt specify order
    }
}