using System;

namespace Gameplay.CustomStatsSystem.Stats
{
    [Serializable]
    public class Stat
    {
        public float baseValue;

        private bool _isDirty = true;
        private float _lastBaseValue;
        private float _value;
        public float Value
        {
            get
            {   // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (!_isDirty && baseValue == _lastBaseValue) return _value;
                _isDirty = false;
                return _value = CalculateFinalValue();
            }
        }

        public void AddModifier()
        {
            
        }
        
        private float CalculateFinalValue()
        {
            throw new NotImplementedException();
        }
    }
}