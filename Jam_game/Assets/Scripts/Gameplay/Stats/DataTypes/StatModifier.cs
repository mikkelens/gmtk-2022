using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Gameplay.Stats.DataTypes
{
    [Serializable]
    public class StatModifier
    {
        [SerializeField] private StatTypeBase typeTarget;
        [SerializeField] private float value = 0;
        [SerializeField] private ModificationTypes modificationType = ModificationTypes.MultiplyAdd;
        [SerializeField] private Optional<float> timer;
        
        // accessors
        public List<StatType> TypeTargets
        {
            get
            {
                if (typeTarget is StatType statType) return new List<StatType> {statType};
                if (typeTarget is StatTypeCollection statTypeCollection) return statTypeCollection.statTypes;
                return null;
            }
        }
        public float Value => value;
        public ModificationTypes ModificationType => modificationType;
        public Optional<float> Timer => timer;
        
        public int Order => (int)modificationType;
    }
}