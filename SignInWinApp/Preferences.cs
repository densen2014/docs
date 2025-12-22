using System.Text.Json;

namespace SignInWinApp;

public static class Preferences
{
    private static readonly string FilePath =
        Path.Combine(AppContext.BaseDirectory, "preferences.json");

    private static readonly Dictionary<string, object?> Settings = Load();

    private static Dictionary<string, object?> Load()
    {
        if (!File.Exists(FilePath))
            return new Dictionary<string, object?>();
        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<Dictionary<string, object?>>(json)
               ?? new Dictionary<string, object?>();
    }

    private static void Save()
    {
        var json = JsonSerializer.Serialize(Settings);
        File.WriteAllText(FilePath, json);
    }

    public static void Set<T>(string key, T value)
    {
        Settings[key] = value;
        Save();
    }

    public static T Get<T>(string key, T defaultValue)
    {
        if (Settings.TryGetValue(key, out var val) && val is not null)
        {
            if (val is JsonElement elem)
                return elem.Deserialize<T>() ?? defaultValue;
            if (val is T tVal)
                return tVal;
            try
            {
                return (T)Convert.ChangeType(val, typeof(T));
            }
            catch
            {
                // ignore
            }
        }
        return defaultValue;
    }
}
