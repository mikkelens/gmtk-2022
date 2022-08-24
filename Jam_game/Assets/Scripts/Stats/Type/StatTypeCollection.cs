using System.Collections.Generic;
using UnityEngine;

namespace Stats.Type
{
    [CreateAssetMenu(fileName = "New StatType Collection", menuName = MenuPath + "StatType Collection")]
    public class StatTypeCollection : StatTypeGeneric
    {
        public List<StatType> statTypes = new();
    }

    // [CreateAssetMenu(fileName = "New Stat Type Collection", menuName = "Stats/Stat Type Collection")]
    // public class FloatStatTypeCollection : StatTypeCollection<FloatStatType>
    // {
    //     [SerializeField] private List<FloatStatType> statTypes = new List<FloatStatType>();
    //     public override List<FloatStatType> StatTypes => statTypes;
    // }
    //
    // [CreateAssetMenu(fileName = "New Stat Type Collection", menuName = "Stats/Stat Type Collection")]
    // public class BoolStatTypeCollection : StatTypeCollection<BoolStatType>
    // {
    //     [SerializeField] private List<BoolStatType> statTypes = new List<BoolStatType>();
    //     public override List<BoolStatType> StatTypes => statTypes;
    // }
}
