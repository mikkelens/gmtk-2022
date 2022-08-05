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
    // can be float or int
    [Serializable]
    public abstract class Stat<T>
    {
        [SerializeField] protected T baseValue;
        // [HideInInspector] public T oldValue; // only for custom drawer. not sure how to hide this
        
        protected Stat(T value) => baseValue = value;
        
        public StatType type;
        
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
                return lastValue = ModifiedValue();
            }
        }
        protected abstract List<Modifier<T>> Modifiers { get; }
        protected abstract T ModifiedValue();

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
