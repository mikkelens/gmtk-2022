using System.Collections.Generic;
using Gameplay.Stats.DataTypes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Attacks
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Stats/Weapon")]
    [TypeInfoBox("'Weapons' encompass all types of attacks. They can have multiple attacks using one weapon.")]
    public class Weapon : StatCollection
    {
        public List<AttackStats> allAttacks = new List<AttackStats>();
    }
}