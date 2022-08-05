using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
	public static class ListUtils
	{
		public static T RandomItem<T>(this List<T> list)
		{
			float percent = Random.value;
			int length = list.Count - 1;
			int chosenIndex = Mathf.RoundToInt(percent * length);
			return list[chosenIndex];
		}
	}
}