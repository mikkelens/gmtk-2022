using System;
using System.Collections.Generic;

namespace StatSystem.Code
{
    [Serializable]
    public class CharacterStat
    {
        public float BaseValue;
        public virtual float Value
        {
            get
            {   // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (!IsDirty || BaseValue != LastBaseValue) return InternalValue;
                LastBaseValue = BaseValue;
                IsDirty = false;
                return InternalValue = CalculateFinalValue();
            }
        }
        protected bool IsDirty = true;
        protected float InternalValue;
        protected float LastBaseValue = float.MinValue;
        
        protected readonly List<StatModifier> InternalStatModifiers;
        public readonly IReadOnlyCollection<StatModifier> StatModifiers;

        public CharacterStat()
        {
            InternalStatModifiers = new List<StatModifier>();
            StatModifiers = InternalStatModifiers.AsReadOnly();
        }
        public CharacterStat(float baseValue) : this()
        {
            BaseValue = baseValue;
            InternalStatModifiers = new List<StatModifier>();
            StatModifiers = InternalStatModifiers.AsReadOnly();
        }

        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order) return -1;
            if (a.Order > b.Order) return 1;
            return 0; // orders are equal
        }
        
        public virtual void AddModifier(StatModifier mod)
        {
            IsDirty = true;
            InternalStatModifiers.Add(mod);
            // sorting is needed for CalculateFinalValue() algorithm to work with percent additive modifiers
            InternalStatModifiers.Sort(CompareModifierOrder);
        }
        public virtual bool RemoveModifier(StatModifier mod)
        {
            if (InternalStatModifiers.Remove(mod))
            {
                return IsDirty = true;
            }
            return false;
        }
        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;
            for (int i = InternalStatModifiers.Count - 1; i >= 0; i--) // from top to bottom because of list index stacking
            {
                if (InternalStatModifiers[i].Source == source)
                {
                    didRemove = true;
                    IsDirty = true;
                    InternalStatModifiers.RemoveAt(i);
                }
            }
            return didRemove;
        }

        protected virtual float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;
            for (int i = 0; i < InternalStatModifiers.Count; i++)
            {
                StatModifier mod = InternalStatModifiers[i];
                if (mod.Type == StatModType.Flat)
                {
                    finalValue += mod.Value;
                }
                else if (mod.Type == StatModType.PercentAdditive)
                {
                    sumPercentAdd += mod.Value; // add additive percent modifiers together
                    if (i + 1 >= InternalStatModifiers.Count || InternalStatModifiers[i + 1].Type != StatModType.PercentAdditive)
                    {
                        finalValue *= 1 + sumPercentAdd; // apply all additive percent modifiers at once
                        sumPercentAdd = 0; // reset percent additive value
                    }
                }
                else if (mod.Type == StatModType.PercentMultiplicative)
                {
                    finalValue *= 1 + mod.Value;
                }
            }
            return (float)Math.Round(finalValue, 4); // 12.0001f != 12f
        }
    }
}
