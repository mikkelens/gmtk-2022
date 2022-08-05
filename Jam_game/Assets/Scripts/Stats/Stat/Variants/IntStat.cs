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
        protected override bool Compare(int a, int b) => a == b;
        
        private List<Modifier<int>> _modifiers = new List<Modifier<int>>();
        protected override List<Modifier<int>> Modifiers => _modifiers;
        protected override int ModifiedValue()
        {
            int modifiedValue = baseValue;
            float sumMultiplierAdd = 0;
            for (int i = 0; i < Modifiers.Count; i++)
            {
                Modifier<int> modifier = Modifiers[i];
                if (modifier.Type == ModificationTypes.Replace)
                {
                    modifiedValue = modifier.Value;
                }
                else if (modifier.Type == ModificationTypes.Add)
                {
                    modifiedValue += Mathf.CeilToInt(modifier.Value);
                }
                else if (modifier.Type == ModificationTypes.AddMultiply)
                {
                    sumMultiplierAdd += modifier.Value;
                    if (i + 1 >= Modifiers.Count || Modifiers[i + 1].Type != ModificationTypes.AddMultiply)
                    {   // relying on our sorted list...
                        modifiedValue = Mathf.CeilToInt(modifiedValue * sumMultiplierAdd);
                        sumMultiplierAdd = 0;
                    }
                }
                else if (modifier.Type == ModificationTypes.TrueMultiply)
                {
                    modifiedValue = Mathf.CeilToInt(modifiedValue * modifier.Value);
                }
            }
            return modifiedValue;
        }

        public static implicit operator IntStat(int value) => new IntStat(value);
        public static implicit operator int(IntStat stat) => stat.CurrentValue; // can write "myStat" instead of "myStat.CurrentValue" 
    }
}
