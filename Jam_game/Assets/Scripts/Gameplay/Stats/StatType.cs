using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Stats
{
    [CreateAssetMenu(fileName = "New Stat Type", menuName = "Stats/Stat Type")]
    [TypeInfoBox(Infomsg)]
    public class StatType : ScriptableObject
    {
        private const string Infomsg = "A single stat type.\n" +
                                       "Make one of these Scriptable Object assets to define a new type of stat.\n" +
                                       "You can then use this as a way of linking upgrades and stats together.";
    }
}