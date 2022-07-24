using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Stats
{
    [CreateAssetMenu(fileName = "New Stat Type Collection", menuName = "Stats/Stat Type Collection")]
    [TypeInfoBox("A collection of multiple stat types.")]
    public class StatTypeCollection : ScriptableObject
    {
        public List<StatType> statTypes = new List<StatType>();
    }
}