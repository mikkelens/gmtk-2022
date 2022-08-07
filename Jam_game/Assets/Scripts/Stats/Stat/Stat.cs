using System;
using System.Collections;
using System.Collections.Generic;
using Stats.Stat.Modifier;
using Stats.Type;
using Tools;
using UnityEngine;

namespace Stats.Stat
{
    // can be of different types. Implemented variations: Color, Float, Int, Bool
    [Serializable]
    public abstract class Stat<T> where T : IEquatable<T>
    {
        // editable
        [SerializeField] public T baseValue;
        [SerializeField] public StatType associatedType;
        
        protected Stat(T value) => baseValue = value;

        // serialized to supply drawer
        [SerializeField] private T lastValue;
        
        private T _lastBaseValue;
        protected bool IsDirty = true;
        public T CurrentValue
        {
            get
            {
                if (!IsDirty && _lastBaseValue.Equals(baseValue)) return lastValue;
                IsDirty = false;
                _lastBaseValue = baseValue;
                if (Modifiers == null) return lastValue = baseValue;
                return lastValue = ModifiedValue();
            }
        }

        protected List<Modifier<T>> Modifiers { get; } = new List<Modifier<T>>();

        protected abstract T ModifiedValue();

        private static int CompareModifyOrder(Modifier<T> a, Modifier<T> b)
        {
            if (a.Order < b.Order) return -1;
            if (a.Order > b.Order) return 1;
            return 0; // orders are equal
        }
        
        public IEnumerator AddModifier(Modifier<T> modifier, MonoBehaviour manager)
        {
            if (modifier.usageDelay.Enabled)
                yield return new WaitForSeconds(modifier.usageDelay.Value);
            float usageTime = Time.time;

            do
            {
                IsDirty = true;
                Modifiers.Add(modifier);
                Modifiers.Sort(CompareModifyOrder);
            } while (modifier.resetAfterTime.Enabled);

            if (modifier.resetAfterTime.Enabled && usageTime.TimeSince() > modifier.resetAfterTime.Value)
            {
                RemoveModifier(modifier);
            }
        }
        public bool RemoveModifier(Modifier<T> modifierToRemove)
        {
            bool removed = Modifiers.RemoveAll(eachModifier => eachModifier.Equals(modifierToRemove)) > 0;
            if (removed) IsDirty = true;
            return removed;
        }
    }
}
