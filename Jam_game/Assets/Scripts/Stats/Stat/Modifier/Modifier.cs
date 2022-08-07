using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Stats.Type;
using Tools;
using UnityEngine;

namespace Stats.Stat.Modifier
{
    [Serializable]
    public class Modifier<T>
    {
        public Modifier(T setValue) => modificationValue = setValue;

        [SerializeField] public T modificationValue;
        [SerializeField] public ModificationTypes type = ModificationTypes.AddMultiply;
        [SerializeField] public Optional<float> usageDelay;
        [SerializeField] public Optional<float> resetAfterTime;

        [Tooltip("If disabled, modification does not repeatedly add")]
        [SerializeField] public Optional<float> repeatDelay;
        
        // private for clarity. we need to use the target method below
        [SerializeField] private List<StatTypeGeneric> modificationTargets;

        public int Order => (int)type;
        
        public List<StatType> GetAllTargets()
        {
            List<StatType> statTypes = new List<StatType>();
            foreach (StatTypeGeneric baseType in modificationTargets)
            {
                if (baseType is StatType single)
                {
                    statTypes.Add(single);
                }
                if (baseType is StatTypeCollection collection)
                {
                    collection.statTypes.ForEach(statType => statTypes.Add(statType));
                }
            }
            return statTypes;
        }
    }
}