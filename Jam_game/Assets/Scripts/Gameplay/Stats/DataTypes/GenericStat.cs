using System;
using System.Collections.Generic;

namespace Gameplay.Stats.DataTypes
{
    // can be float or int
    [Serializable]
    public class GenericStat
    {
        public StatType type;
        
        protected bool IsDirty = true;
        
        protected readonly List<StatModifier> ModifierList = new List<StatModifier>();
        public IReadOnlyCollection<StatModifier> Modifiers => ModifierList.AsReadOnly(); // in case we ever need it

        public void AddModifier(StatModifier statModifier)
        {
            IsDirty = true;
            ModifierList.Add(statModifier);
            ModifierList.Sort(CompareModifyOrder);
        }
        public bool RemoveModifier(StatModifier statModifier)
        {
            bool removed = ModifierList.Remove(statModifier);
            if (removed) IsDirty = true;
            return removed;
        }

        private static int CompareModifyOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order) return -1;
            if (a.Order > b.Order) return 1;
            return 0; // orders are equal
        }

        protected virtual float CalculateFinalValue(float startingValue)
        {
            float finalValue = startingValue;
            float sumPercentAdd = 0;
            for (int i = 0; i < ModifierList.Count; i++)
            {
                StatModifier statModifier = ModifierList[i];
                if (statModifier.modificationType == ModificationTypes.Flat)
                {
                    finalValue += statModifier.value;
                }
                else if (statModifier.modificationType == ModificationTypes.PercentAdd)
                {
                    sumPercentAdd += statModifier.value;
                    // relying on our sorted list...
                    if (i + 1 >= ModifierList.Count || ModifierList[i + 1].modificationType != ModificationTypes.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (statModifier.modificationType == ModificationTypes.PercentMultiply)
                {
                    finalValue *= 1 + statModifier.value;
                }
            }
            return (float)Math.Round(finalValue, 4);
        }
    }
}