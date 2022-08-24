using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Stats.Stat.Modifier
{
	[CreateAssetMenu(fileName = "New Effect", menuName = "Stats/Effect")]
	[Serializable]
	public class Effect : ExpandableScriptableObject
	{
		public List<Modifier<bool>> bools = new();
		public List<Modifier<int>> ints = new();
		public List<Modifier<float>> floats = new();
		public List<Modifier<Color>> colors = new();
	}
}