using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Stats
{
    [CreateAssetMenu(fileName = "New Stat Type", menuName = "Stats/Stat Type")]
    [TypeInfoBox("Make one of these Scriptable Object assets to define a new type of stat.\n" +
             "You can then use this as a way of linking upgrades and stats together.")]
    public class StatType : ScriptableObject
    {
        
    }
}