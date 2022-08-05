using System;
using UnityEngine;

namespace Tools
{
    [Serializable]
    public struct Optional<T>
    {
        [SerializeField] private bool enabled;
        [SerializeField] private T value;

        public bool Enabled
        {
            get => enabled;
            set => enabled = value;
        }
        public T Value
        {
            get
            {
            #if UNITY_EDITOR // these are not triggered by property drawers because the drawers use serialized fields, not properties
                if (!enabled) Debug.LogWarning($"Tried to get value from optional variable '{nameof(T)}' while it was disabled.");
            #endif
                return value;
            }
            set
            {
            #if UNITY_EDITOR
                if (!enabled) Debug.LogWarning($"Tried to set value on optional variable '{nameof(T)}' while it was disabled.");
            #endif
                this.value = value;
            }
        }

        public Optional(T initialValue = default, bool initialEnabled = false) // false is default setting for optional variables
        {
            enabled = initialEnabled;
            value = initialValue;
        }

        // construction from value to optional value implcitly
        public static implicit operator Optional<T>(T value) => new Optional<T>(value, true);
        // alternative way of getting value
        public static explicit operator T(Optional<T> optional) => optional.Value;
    }

}