public static class FileSystem
{
    public static string CacheDirectory
    {
        get
        {
            string cacheDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Fichaje", "Cache");
            if (!Directory.Exists(cacheDir))
                Directory.CreateDirectory(cacheDir);
            return cacheDir;
        }
    }
}
