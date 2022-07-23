using System;
using UnityEngine;

namespace Gameplay.Stats.DataTypes
{
    // [CreateAssetMenu(fileName = "New Float Stat", menuName = "Stat System/Float Stat")]
    [Serializable]
    public class FloatStat : GenericStat
    {
        [SerializeField] public float baseValue;
        
        private float _lastBaseValue;
        [HideInInspector]
        public float value;
        public float CurrentValue
        {
            get
            {   // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (!IsDirty && baseValue == _lastBaseValue) return value;
                _lastBaseValue = baseValue;
                IsDirty = false;
                return value = CalculateFinalValue(baseValue);
            }
        }
        
        public static implicit operator float(FloatStat stat) => stat.CurrentValue; // can write "myStat" instead of "myStat.CurrentValue" 
    }
}