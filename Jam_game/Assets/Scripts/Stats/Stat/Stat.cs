using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Stats.Stat.Modifier;
using Stats.Type;
using UnityEngine;

namespace Stats.Stat
{
    // can be of different types. Implemented variations: Color, Float, Int, Bool
    [Serializable]
    public class Stat<T> where T
    {
        // editable
        [SerializeField] protected T baseValue;
        [SerializeField] protected StatType type;
        
        protected Stat(T value) => baseValue = value;

        // serialized to supply drawer
        [SerializeField] private T lastValue;
        
        private T _lastBaseValue;
        protected bool IsDirty = true;
        public T CurrentValue
        {
            get
            {
                if (!IsDirty && Compare(_lastBaseValue, baseValue)) return lastValue;
                IsDirty = false;
                _lastBaseValue = baseValue;
                if (Modifiers == null) return lastValue = baseValue;
                return lastValue = ModifiedValue(baseValue);
            }
        }

        protected List<Modifier<T>> Modifiers { get; }

        protected T ModifiedValue<int>(T startingValue) where T : IEquatable<int>
        {

        }
        
        protected bool Compare<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) == 0;
        }
        private static int CompareModifyOrder(Modifier<T> a, Modifier<T> b)
        {
            if (a.Order < b.Order) return -1;
            if (a.Order > b.Order) return 1;
            return 0; // orders are equal
        }
        
        public void AddModifier(Modifier<T> modifier)
        {
            IsDirty = true;
            Modifiers.Add(modifier);
            Modifiers.Sort(CompareModifyOrder);
        }
        public bool RemoveModifier(Modifier<T> modifier)
        {
            bool removed = Modifiers.Remove(modifier);
            if (removed) IsDirty = true;
            return removed;
        }
        public IEnumerator TimedModifier(Modifier<T> modifier)
        {
            AddModifier(modifier);
            yield return new WaitForSeconds(modifier.Timer.Value);
            RemoveModifier(modifier);
        }
        
    }

    public static class StatGetters
    {
        public static int ModifiedValue(this Stat<int> stat)
        {
            
        }
    }
}
