using System;
using UnityEngine;

namespace Gameplay.Stats.DataTypes
{
    [Serializable]
    [Tooltip("'Add' adds the number to the current value, 'MultiplyAdd' adds the amount *times* the original value, 'MultiplyExponential' multiplies on top of current value.")]
    public enum ModificationTypes
    {
        Add = 100,
        MultiplyAdd = 200,
        MultiplyExponential = 300,
    }
}