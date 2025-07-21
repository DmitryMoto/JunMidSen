namespace TestJunMidSen.ThirdTask.Utils
{
    public static class GetPrefixedPath
    {
        public static string Get(string originalPath, string prefix)
        {
            string directory = Path.GetDirectoryName(originalPath)!;
            string name = Path.GetFileNameWithoutExtension(originalPath);
            string extension = Path.GetExtension(originalPath);
            string newName = $"{prefix}{name}{extension}";
            return Path.Combine(directory, newName);
        }
    }
}
