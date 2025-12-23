using JovenApi;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Runtime.Caching;
using System.Text;
using System.Text.Json;
#nullable enable

namespace KestrelWebHost;

public partial class WebApp
{
    private static async Task WriteResponse(HttpResponse response, int statusCode, string content)
    {
        response.StatusCode = statusCode;
        var contentBytes = Encoding.UTF8.GetBytes(content);
        response.ContentLength = contentBytes.Length;
        await response.Body.WriteAsync(contentBytes, 0, contentBytes.Length);
    }
    private static async Task WriteResponse(HttpResponse response, int statusCode, ResponeAPI content)
    {
        response.ContentType = "application/json";
        var json = JsonSerializer.Serialize(content, options);
        response.StatusCode = statusCode;
        var contentBytes = Encoding.UTF8.GetBytes(json);
        response.ContentLength = contentBytes.Length;
        await response.Body.WriteAsync(contentBytes, 0, contentBytes.Length);
    }
    public static ObjectCache Cache = MemoryCache.Default;

    public static CacheItemPolicy CacheItemPolicy20s => new CacheItemPolicy
    {
        AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(20.0),
    };
    public static CacheItemPolicy CacheItemPolicy => new CacheItemPolicy
    {
        AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(60.0),
    };
    public static CacheItemPolicy CacheItemPolicy5 => new CacheItemPolicy
    {
        AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5),
    };

    private static JsonSerializerOptions options = new JsonSerializerOptions
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // 禁用 Unicode 转义
    };
    private static JsonSerializerOptions optionsIn = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private static IConfiguration? configuration;
 
   
    public static void Debug(string logs, bool warning = false)
    {
#if DEBUG
        Console.WriteLine($"{DateTime.Now:HH:mm:ss} {(warning ? "(警告) " : "")}{logs}");
#endif
    }
    public static void Logs(string logs, bool savetofile = true, bool warning = false, bool debug = true)
    {
        Console.WriteLine($"{DateTime.Now:HH:mm:ss} {(warning ? "(警告) " : "")}{logs}");
        try
        {
            if (savetofile)
            {
                File.AppendAllText(Logfile(), $"\r\n{DateTime.Now:yyyy-MM-dd HH:mm:ss} {logs}");
            }
        }
        catch (Exception)
        {
        }
    }
    private static string Logfile()
    {
        var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var parentDirectory = Directory.GetParent(currentDirectory)!.FullName;
        var filename = Path.Combine(parentDirectory, $"log_{DateTime.Now:yyyyMM}.log");
        return filename;
    }

    private static IFreeSql? fsql { get; set; }
    private static bool isDatabaseAvailable { get; set; }

}
