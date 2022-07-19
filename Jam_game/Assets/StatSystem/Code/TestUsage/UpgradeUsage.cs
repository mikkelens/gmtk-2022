using System.Collections.Generic;
using UnityEngine;

namespace StatSystem.Code.TestUsage
{
    public class Character
    {
        public CharacterStat Strength;
        public CharacterStat Health;
        public CharacterStat Mana;
    }

    public class StrengthItem
    {
        public void EquipItem(Character c)
        {
            List<StatModifier> _strengthMods = new List<StatModifier>
            {
                new StatModifier(10, StatModType.Flat, this), // base increase
                new StatModifier(0.25f, StatModType.PercentAdditive, this), // 25% increase
                new StatModifier(0.3f, StatModType.PercentAdditive, this), // 30% increase (55% increase)
                new StatModifier(0.1f, StatModType.PercentMultiplicative, this), // additional 10% increase on top (65% increase)
            };
            _strengthMods.ForEach(strengthMod => c.Strength.AddModifier(strengthMod));
        }

        public void UnequipItem(Character c)
        {
            c.Strength.RemoveAllModifiersFromSource(this);
        }
    }
}