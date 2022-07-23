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
            get => value;
            set => this.value = value;
        }

        public Optional(T initialValue = default, bool initialEnabled = false) // false is default setting for optional variables
        {
            enabled = initialEnabled;
            value = initialValue;
        }

        // from value to optional value implcitly
        public static implicit operator Optional<T>(T value) => new Optional<T>(value, true);
    }

}