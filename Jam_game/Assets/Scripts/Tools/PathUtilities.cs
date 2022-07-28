namespace Tools
{
	public static class PathUtilities
	{
		public static string PathWithoutFile(this string path)
		{
			return path.Remove(path.LastIndexOf('/') + 1);
		}

		public static string PathWithoutDirectory(this string path)
		{
			return path[(path.LastIndexOf('/') + 1)..];
		}
	}
}