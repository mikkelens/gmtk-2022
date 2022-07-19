using System.Collections.Generic;
using UnityEngine;

namespace StatSystem.Code.TestUsage.Entity
{
    public class PickupUpgrade : MonoBehaviour
    {
        [SerializeField] private List<StatModifier> modifiers = new List<StatModifier>();

        public void PickedUpByCharacter(Character character)
        {
               
        }
    }
}