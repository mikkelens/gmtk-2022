using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Stats.Attacks
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Stats/Weapon")]
    [TypeInfoBox("'Weapons' encompass all types of attacks. They can have multiple.")]
    public class Weapon : ScriptableObject
    {
        public List<AttackStats> allAttacks = new List<AttackStats>();
    }
}