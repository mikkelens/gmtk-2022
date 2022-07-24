using System.Collections.Generic;
using Gameplay.Stats;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Gameplay.Attacks
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Stats/Weapon")]
    [TypeInfoBox("'Weapons' encompass all types of attacks. They can have multiple attacks using one weapon.")]
    public class Weapon : ExpandableScriptableObject, IStatCollection
    {
        public List<AttackStats> allAttacks = new List<AttackStats>();
    }
}