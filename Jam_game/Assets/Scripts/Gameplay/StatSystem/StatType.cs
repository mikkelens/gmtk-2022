using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.StatSystem
{
    [CreateAssetMenu(fileName = "New Stat Type", menuName = "Stat System/Stat Type")]
    [InfoBox("Make one of these Scriptable Object assets to define a new type of stat.\n" +
             "You can then use this as a way of linking upgrades and stats together.", InfoMessageType.None)]
    public class StatType : ScriptableObject
    {
        
    }
}