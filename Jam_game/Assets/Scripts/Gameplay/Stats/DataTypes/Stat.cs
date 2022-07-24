using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Stats.DataTypes
{
    // can be float or int
    [Serializable]
    public abstract class Stat
    {
        public StatType type;
        
        protected bool IsDirty = true;

        private readonly List<StatModifier> _modifierList = new List<StatModifier>();
        public IReadOnlyCollection<StatModifier> Modifiers => _modifierList.AsReadOnly(); // in case we ever need it

        protected float CalculateFinalValue(float startingValue)
        {
            float finalValue = startingValue;
            float sumPercentAdd = 0;
            for (int i = 0; i < _modifierList.Count; i++)
            {
                StatModifier statModifier = _modifierList[i];
                if (statModifier.ModificationType == ModificationTypes.Add)
                {
                    finalValue += statModifier.Value;
                }
                else if (statModifier.ModificationType == ModificationTypes.MultiplyAdd)
                {
                    sumPercentAdd += statModifier.Value;
                    if (i + 1 >= _modifierList.Count || _modifierList[i + 1].ModificationType != ModificationTypes.MultiplyAdd)
                    {   // relying on our sorted list...
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (statModifier.ModificationType == ModificationTypes.MultiplyExponential)
                {
                    finalValue *= 1 + statModifier.Value;
                }
            }
            return (float)Math.Round(finalValue, 4);
        }
        private static int CompareModifyOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order) return -1;
            if (a.Order > b.Order) return 1;
            return 0; // orders are equal
        }
        
        public void AddModifier(StatModifier modifier)
        {
            IsDirty = true;
            _modifierList.Add(modifier);
            _modifierList.Sort(CompareModifyOrder);
        }
        public bool RemoveModifier(StatModifier modifier)
        {
            bool removed = _modifierList.Remove(modifier);
            if (removed) IsDirty = true;
            return removed;
        }
        public IEnumerator TimedModifier(StatModifier modifier)
        {
            AddModifier(modifier);
            yield return new WaitForSeconds(modifier.Timer.Value);
            RemoveModifier(modifier);
        }
    }
}
