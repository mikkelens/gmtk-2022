using System;
using UnityEngine;

namespace Tools
{
    [Serializable]
    public struct Optional<T>
    {
        [SerializeField] private bool enabled;
        [SerializeField] private T value;

        public bool Enabled => enabled;
        public T Value => value;

        public Optional(T initialValue)
        {
            enabled = false; // opt-in, disabled by default
            value = initialValue;
        }
        public Optional(T initialValue, bool initialEnabled)
        {
            enabled = initialEnabled;
            value = initialValue;
        }
    }

}