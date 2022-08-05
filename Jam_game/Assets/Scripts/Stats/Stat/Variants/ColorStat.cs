using System;
using System.Collections.Generic;
using Stats.Stat.Modifier;
using UnityEngine;

namespace Stats.Stat.Variants
{
	[Serializable]
	public class ColorStat : Stat<Color>
	{
		public ColorStat(Color value) : base(value) { }
		protected override bool Compare(Color a, Color b) => a == b;

		private List<Modifier<Color>> _modifiers = new List<Modifier<Color>>();
		protected override List<Modifier<Color>> Modifiers => _modifiers;
		protected override Color ModifiedValue()
		{
			Color modifiedValue = baseValue;
			Color sumPercentAdd = Color.clear;
			for (int i = 0; i < Modifiers.Count; i++)
			{
				Modifier<Color> modifier = Modifiers[i];
				if (modifier.Type == ModificationTypes.Replace)
				{	// relying on our sorted list...
					modifiedValue = modifier.Value;
				}
				else if (modifier.Type == ModificationTypes.Add)
				{
					modifiedValue += modifier.Value;
				}
				else if (modifier.Type == ModificationTypes.AddMultiply)
				{
					sumPercentAdd += modifier.Value;
					if (i + 1 >= Modifiers.Count || Modifiers[i + 1].Type != ModificationTypes.AddMultiply)
					{	// relying on our sorted list...
						modifiedValue *= Color.clear + sumPercentAdd;
						sumPercentAdd = Color.clear;
					}
				}
				else if (modifier.Type == ModificationTypes.TrueMultiply)
				{
					modifiedValue *= Color.clear + modifier.Value;
				}
			}
			return modifiedValue;
		}
	}
}