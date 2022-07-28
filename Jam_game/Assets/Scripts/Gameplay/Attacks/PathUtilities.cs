namespace Gameplay.Attacks
{
	public static class PathUtilities
	{
		public static string PathWithoutAsset(this string path)
		{
			return path.Remove(path.LastIndexOf('/'));
		}
	}
}