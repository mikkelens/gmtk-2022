using System;
using UnityEngine;

namespace Gameplay.Stats.Stat.Modifier
{
    [Serializable]
    [Tooltip("'Add' adds the number to the current value, 'MultiplyAdd' adds the amount *times* the original value, 'MultiplyExponential' multiplies on top of current value.")]
    public enum ModificationTypes
    {
        Add = 100,
        AddMultiply = 200,
        TrueMultiply = 300,
    }
}