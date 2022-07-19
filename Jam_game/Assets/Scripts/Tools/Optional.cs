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
        
        public Optional(T initialValue, bool initialEnabled = false)
        {
            enabled = initialEnabled;
            value = initialValue;
        }
    }

}