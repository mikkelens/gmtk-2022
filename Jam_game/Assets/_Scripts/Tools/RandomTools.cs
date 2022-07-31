namespace Tools
{
	public static class RandomTools
	{
		private static System.Random _random;
		private static System.Random MyRandom => _random ??= new System.Random();
		public static double NextDouble() => MyRandom.NextDouble(); // 0.0 <= x < 1.0
		public static float NextFloat() => (float)NextDouble();
		public static bool NextBool() => NextDouble() < 0.0f;
		public static int NextIntSign() => NextBool() ? 1 : -1;
	}
}