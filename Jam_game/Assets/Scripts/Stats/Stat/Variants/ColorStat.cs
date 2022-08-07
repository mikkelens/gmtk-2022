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
		protected override Color ModifiedValue()
		{
			Color modifiedValue = baseValue;
			Color sumPercentAdd = Color.clear;
			for (int i = 0; i < Modifiers.Count; i++)
			{
				Modifier<Color> modifier = Modifiers[i];
				if (modifier.type == ModificationTypes.Replace)
				{	// relying on our sorted list...
					modifiedValue = modifier.modificationValue;
				}
				else if (modifier.type == ModificationTypes.Add)
				{
					modifiedValue += modifier.modificationValue;
				}
				else if (modifier.type == ModificationTypes.AddMultiply)
				{
					sumPercentAdd += modifier.modificationValue;
					if (i + 1 >= Modifiers.Count || Modifiers[i + 1].type != ModificationTypes.AddMultiply)
					{	// relying on our sorted list...
						modifiedValue *= Color.clear + sumPercentAdd;
						sumPercentAdd = Color.clear;
					}
				}
				else if (modifier.type == ModificationTypes.TrueMultiply)
				{
					modifiedValue *= Color.clear + modifier.modificationValue;
				}
			}
			return modifiedValue;
		}
	}
}