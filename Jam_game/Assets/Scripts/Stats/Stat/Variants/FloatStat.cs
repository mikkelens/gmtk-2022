using System;
using System.Collections.Generic;
using Stats.Stat.Modifier;

namespace Stats.Stat.Variants
{
    [Serializable]
    public class FloatStat : Stat<float>
    {
        public FloatStat(float value) : base(value) { }
        protected override bool Compare(float a, float b) => Math.Abs(a - b) < 0.000000001;
        
        private List<Modifier<float>> _modifiers = new List<Modifier<float>>();
        protected override List<Modifier<float>> Modifiers => _modifiers;
        protected override float ModifiedValue()
        {
            float modifierValue = baseValue;
            float sumPercentAdd = 0;
            for (int i = 0; i < Modifiers.Count; i++)
            {
                Modifier<float> modifier = Modifiers[i];
                if (modifier.Type == ModificationTypes.Replace)
                {   // relying on our sorted list...
                    modifierValue = modifier.Value;
                }
                else if (modifier.Type == ModificationTypes.Add)
                {
                    modifierValue += modifier.Value;
                }
                else if (modifier.Type == ModificationTypes.AddMultiply)
                {
                    sumPercentAdd += modifier.Value;
                    if (i + 1 >= Modifiers.Count || Modifiers[i + 1].Type != ModificationTypes.AddMultiply)
                    {   // relying on our sorted list...
                        modifierValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (modifier.Type == ModificationTypes.TrueMultiply)
                {
                    modifierValue *= 1 + modifier.Value;
                }
            }
            return modifierValue;
        }
        
        public static implicit operator FloatStat(float value) => new FloatStat(value);
        public static implicit operator float(FloatStat stat) => stat.CurrentValue; // can write "myStat" instead of "myStat.CurrentValue" 
    }
}