using System;
using System.Collections.Generic;
using Stats.Stat.Modifier;
using UnityEngine;

namespace Stats.Stat.Variants
{
    // [CreateAssetMenu(fileName = "New Int Stat", menuName = "Stat System/Int Stat")]
    [Serializable]
    public class IntStat : Stat<int>
    {
        public IntStat(int value) : base(value) { }
        protected override bool Compare(int a, int b) => a == b;
        
        private List<Modifier<int>> _modifiers = new List<Modifier<int>>();
        protected override List<Modifier<int>> Modifiers => _modifiers;
        protected override int ModifiedValue()
        {
            
        }

        public static implicit operator IntStat(int value) => new IntStat(value);
        public static implicit operator int(IntStat stat) => stat.CurrentValue; // can write "myStat" instead of "myStat.CurrentValue" 
    }
}
