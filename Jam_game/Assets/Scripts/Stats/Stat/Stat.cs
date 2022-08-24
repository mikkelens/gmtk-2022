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
        protected bool isDirty = true;
        public T CurrentValue
        {
            get
            {
                if (!isDirty && _lastBaseValue.Equals(baseValue)) return lastValue;
                isDirty = false;
                _lastBaseValue = baseValue;
                if (Modifiers == null) return lastValue = baseValue;
                return lastValue = ModifiedValue();
            }
        }

        protected List<Modifier<T>> Modifiers { get; } = new();

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

            do // do at least once
            {
                isDirty = true;
                Modifiers.Add(modifier);
                Modifiers.Sort(CompareModifyOrder);

                if (modifier.resetAfterTime.Enabled && usageTime.TimeSince() >= modifier.resetAfterTime.Value)
                {
                    RemoveModifier(modifier); // we dont need to sort because of how removing works
                    yield break;
                }
                // repeat if we should repeat
            } while (modifier.repeatEachTime.Enabled && usageTime.TimeSince() >= modifier.repeatEachTime.Value);

        }

        public bool RemoveModifier(Modifier<T> modifierToRemove)
        {
            bool removed = Modifiers.RemoveAll(eachModifier => eachModifier.Equals(modifierToRemove)) > 0;
            if (removed) isDirty = true;
            return removed;
        }
    }
}