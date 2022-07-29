using System;
using System.Collections.Generic;
using Gameplay.Stats.Stat.Modifier;
using UnityEngine;

namespace Gameplay.Stats.Stat.Variants
{
    // [CreateAssetMenu(fileName = "New Float Stat", menuName = "Stat System/Float Stat")]
    [Serializable]
    public class FloatStat : Stat<float>
    {
        public FloatStat(float value) : base(value) { }
        protected override bool Compare(float a, float b) => Math.Abs(a - b) < 0.000000001;
        
        private List<Modifier<float>> _modifiers = new List<Modifier<float>>();
        protected override List<Modifier<float>> Modifiers => _modifiers;
        protected override float ModifiedValue(float startingValue)
        {
            float finalValue = 0;
            float sumPercentAdd = 0;
            for (int i = 0; i < Modifiers.Count; i++)
            {
                Modifier<float> modifier = Modifiers[i];
                if (modifier.ModificationType == ModificationTypes.Add)
                {
                    finalValue += modifier.Value;
                }
                else if (modifier.ModificationType == ModificationTypes.AddMultiply)
                {
                    sumPercentAdd += modifier.Value;
                    if (i + 1 >= Modifiers.Count || Modifiers[i + 1].ModificationType != ModificationTypes.AddMultiply)
                    {   // relying on our sorted list...
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (modifier.ModificationType == ModificationTypes.TrueMultiply)
                {
                    finalValue *= 1 + modifier.Value;
                }
            }
            return (float)Math.Round(finalValue);
        }
        
        public static implicit operator FloatStat(float value) => new FloatStat(value);
        public static implicit operator float(FloatStat stat) => stat.CurrentValue; // can write "myStat" instead of "myStat.CurrentValue" 
    }
}