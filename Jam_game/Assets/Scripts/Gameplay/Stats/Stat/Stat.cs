using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Stats.Stat.Modifier;
using Gameplay.Stats.Type;
using UnityEngine;

namespace Gameplay.Stats.Stat
{
    // can be float or int
    [Serializable]
    public abstract class Stat<T>
    {
        [SerializeField] public T baseValue;
        public T value;
        
        protected Stat(T value) => baseValue = value;
        
        private T _lastBaseValue;
        private T _lastValue;
        
        protected bool IsDirty = true;

        public StatType type;
        public T CurrentValue
        {
            get
            {
                if (!IsDirty && Compare(_lastBaseValue, baseValue)) return _lastValue;
                IsDirty = false;
                _lastBaseValue = baseValue;
                if (Modifiers == null) return _lastValue = baseValue;
                return _lastValue = ModifiedValue(baseValue);
            }
        }
        protected abstract List<Modifier<T>> Modifiers { get; }
        protected abstract T ModifiedValue(T startingValue);

        protected abstract bool Compare(T a, T b);
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
}
