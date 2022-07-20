using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.CustomStatsSystem
{
    [CreateAssetMenu(fileName = "New Stat", menuName = "Stat System/Stat")]
    public class Stat : ScriptableObject
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
                _lastBaseValue = baseValue;
                _isDirty = false;
                return _value = CalculateFinalValue();
            }
        }
        private List<Upgrade> _upgrades = new List<Upgrade>();
        public IReadOnlyCollection<Upgrade> Upgrades => _upgrades.AsReadOnly();
        
        public void AddUpgrade(Upgrade upgrade)
        {
            _isDirty = true;
            _upgrades.Add(upgrade);
            _upgrades.Sort(CompareModifyOrder);
        }
        public bool RemoveUpgrade(Upgrade upgrade)
        {
            bool removed = _upgrades.Remove(upgrade);
            if (removed) _isDirty = true;
            return removed;
        }
        
        private float CalculateFinalValue()
        {
            float finalValue = baseValue;
            float sumPercentAdd = 0;
            for (int i = 0; i < _upgrades.Count; i++)
            {
                Upgrade upgrade = _upgrades[i];
                if (upgrade.Type == ModificationTypes.Flat)
                {
                    finalValue += upgrade.Value;
                }
                else if (upgrade.Type == ModificationTypes.PercentAdd)
                {
                    sumPercentAdd += upgrade.Value;
                    // relying on our sorted list...
                    if (i + 1 >= _upgrades.Count || _upgrades[i + 1].Type != ModificationTypes.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (upgrade.Type == ModificationTypes.PercentMultiply)
                {
                    finalValue *= 1 + upgrade.Value;
                }
            }
            return (float)Math.Round(finalValue, 4);
        }

        private static int CompareModifyOrder(Upgrade a, Upgrade b)
        {
            if (a.Order < b.Order) return -1;
            if (a.Order > b.Order) return 1;
            return 0; // orders are equal
        }
    }
}