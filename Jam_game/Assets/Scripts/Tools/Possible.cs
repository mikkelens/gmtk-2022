using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tools
{
	[Serializable]
	public struct Possible<T>
	{
		[SerializeField] private float chance;
		[SerializeField] private T value;
		
		public float Chance
		{
			get => chance;
			set => chance = value;
		}
		public T Value
		{
			get => value;
			set => this.value = value;
		}

		public Possible(T initialValue = default, float initialChance = 1.0f)
		{
			chance = initialChance;
			value = initialValue;
		}
		// construction from value to optional value implcitly
		public static implicit operator Possible<T>(T value) => new Possible<T>(value);
		// alternative way of getting value
		public static explicit operator T(Possible<T> optional) => optional.Value;
	}

	public static class PossibleHelpers
	{
		public static Possible<T> SelectPossibleRelative<T>(this List<Possible<T>> possibilities) where T : class
		{
			if (possibilities.Count == 0) return null;
						
			// count up chances as a range, then generate a number within the range.
			float chancesSum = possibilities.Sum(possible => possible.Chance);
			float scaledRandom = RandomTools.NextFloat() * chancesSum;
			float last = 0;
			foreach (Possible<T> possible in possibilities)
			{ 
				 last += possible.Chance;
				 if (last >= scaledRandom) return possible; // Item with lowest number but above generated number will be chosen.
			}
			return possibilities.First();
		}
	}
}
