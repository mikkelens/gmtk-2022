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
		protected override bool ModifiedValue()
		{
			// if any of the modifiers have a true value, flip starting value
			bool containsATrue = Modifiers.Any(modifier => modifier.modificationValue);
			return baseValue ^ containsATrue;
		}
		
		public static implicit operator BoolStat(bool value) => new(value);
		public static implicit operator bool(BoolStat stat) => stat.CurrentValue;
	}
}