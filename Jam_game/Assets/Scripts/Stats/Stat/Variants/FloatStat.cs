using System;
using System.Collections.Generic;
using Stats.Stat.Modifier;

namespace Stats.Stat.Variants
{
    [Serializable]
    public class FloatStat : Stat<float>
    {
        public FloatStat(float value) : base(value) { }
        
        protected override float ModifiedValue()
        {
            float modifierValue = baseValue;
            float sumPercentAdd = 0;
            for (int i = 0; i < Modifiers.Count; i++)
            {
                Modifier<float> modifier = Modifiers[i];
                if (modifier.type == ModificationTypes.Replace)
                {   // relying on our sorted list...
                    modifierValue = modifier.modificationValue;
                }
                else if (modifier.type == ModificationTypes.Add)
                {
                    modifierValue += modifier.modificationValue;
                }
                else if (modifier.type == ModificationTypes.AddMultiply)
                {
                    sumPercentAdd += modifier.modificationValue;
                    if (i + 1 >= Modifiers.Count || Modifiers[i + 1].type != ModificationTypes.AddMultiply)
                    {   // relying on our sorted list...
                        modifierValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (modifier.type == ModificationTypes.TrueMultiply)
                {
                    modifierValue *= 1 + modifier.modificationValue;
                }
            }
            return modifierValue;
        }
        
        public static implicit operator FloatStat(float value) => new(value);
        public static implicit operator float(FloatStat stat) => stat.CurrentValue; // can write "myStat" instead of "myStat.CurrentValue" 
    }
}