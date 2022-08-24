using System;
using System.Collections.Generic;
using Stats.Stat.Modifier;
using UnityEngine;

namespace Stats.Stat.Variants
{
    // [CreateAssetMenu(fileName = "New Int Stat", menuName = "Stat System/Int Stat")]
    [Serializable]
    public class IntStat : Stat<int>
    {
        public IntStat(int value) : base(value) { }

        protected override int ModifiedValue()
        {
            int modifiedValue = baseValue;
            float sumMultiplierAdd = 0;
            for (int i = 0; i < Modifiers.Count; i++)
            {
                Modifier<int> modifier = Modifiers[i];
                if (modifier.type == ModificationTypes.Replace)
                {
                    modifiedValue = modifier.modificationValue;
                }
                else if (modifier.type == ModificationTypes.Add)
                {
                    modifiedValue += Mathf.CeilToInt(modifier.modificationValue);
                }
                else if (modifier.type == ModificationTypes.AddMultiply)
                {
                    sumMultiplierAdd += modifier.modificationValue;
                    if (i + 1 >= Modifiers.Count || Modifiers[i + 1].type != ModificationTypes.AddMultiply)
                    {   // relying on our sorted list...
                        modifiedValue = Mathf.CeilToInt(modifiedValue * sumMultiplierAdd);
                        sumMultiplierAdd = 0;
                    }
                }
                else if (modifier.type == ModificationTypes.TrueMultiply)
                {
                    modifiedValue = Mathf.CeilToInt(modifiedValue * modifier.modificationValue);
                }
            }
            return modifiedValue;
        }

        public static implicit operator IntStat(int value) => new(value);
        public static implicit operator int(IntStat stat) => stat.CurrentValue; // can write "myStat" instead of "myStat.CurrentValue" 
    }
}
