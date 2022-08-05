using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stats.Stat.Modifier
{
	[Serializable]
	public class ModifierCollection
	{
		public List<Modifier<bool>> bools = new List<Modifier<bool>>();
		public List<Modifier<int>> ints = new List<Modifier<int>>();
		public List<Modifier<float>> floats = new List<Modifier<float>>();
		public List<Modifier<Color>> colors = new List<Modifier<Color>>();
	}
}