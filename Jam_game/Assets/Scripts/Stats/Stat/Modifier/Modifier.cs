using System;
using System.Collections.Generic;
using Stats.Type;
using Tools;
using UnityEngine;

namespace Stats.Stat.Modifier
{
    [Serializable]
    public class Modifier<T>
    {
        public Modifier(T setValue)
        {
            modificationValue = setValue;
        }

        [SerializeField] private ModificationTypes type = ModificationTypes.AddMultiply;
        [SerializeField] private Optional<float> timer;

        [SerializeField] private T modificationValue;
        [SerializeField] private List<StatTypeGeneric> modificationTargets;
        public T Value => modificationValue;

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
        
        public ModificationTypes Type => type;
        public Optional<float> Timer => timer;

        public int Order => (int)type;
    }
}