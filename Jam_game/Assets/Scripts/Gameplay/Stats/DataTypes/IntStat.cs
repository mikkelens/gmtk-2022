using System;
using UnityEngine;

namespace Gameplay.Stats.DataTypes
{
    // [CreateAssetMenu(fileName = "New Int Stat", menuName = "Stat System/Int Stat")]
    [Serializable]
    public class IntStat : Stat
    {
        [SerializeField] private int baseValue;

        private int _lastBaseValue;
        [HideInInspector]
        public int value;
        public int CurrentValue
        {
            get
            {   // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (!IsDirty && baseValue == _lastBaseValue) return value;
                _lastBaseValue = baseValue;
                IsDirty = false;
                return value = Mathf.RoundToInt(CalculateFinalValue(baseValue));
            }
        }
        
        public static implicit operator int(IntStat stat) => stat.CurrentValue; // can write "myStat" instead of "myStat.CurrentValue" 
    }
}