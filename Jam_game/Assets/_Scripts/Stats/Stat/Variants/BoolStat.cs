using System;
using System.Collections.Generic;
using System.Linq;
using Stats.Stat.Modifier;

namespace Stats.Stat.Variants
{
	[Serializable]
	public class BoolStat : Stat<bool>
	{
		public BoolStat(bool value) : base(value) { }
		protected override bool Compare(bool a, bool b) => a == b;
		
		private List<Modifier<bool>> _modifiers = new List<Modifier<bool>>();
		protected override List<Modifier<bool>> Modifiers => _modifiers;
		protected override bool ModifiedValue()
		{
			// if any of the modifiers have a true value, flip starting value
			bool containsATrue = Modifiers.Any(modifier => modifier.Value);
			return baseValue ^ containsATrue;
		}
		
		public static implicit operator BoolStat(bool value) => new BoolStat(value);
		public static implicit operator bool(BoolStat stat) => stat.CurrentValue;
	}
}