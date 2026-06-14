using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SignInMauiApp;
using System.Net;

namespace KestrelWebHost;

public class WebHostProgram
{
    public static Task WebHostMain(WebHostParameters webHostParameters)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("apisettings.json", optional: true, reloadOnChange: true) // 确保 AddJsonFile 方法可用
            .Build();

        var webHost = new WebHostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<IConfiguration>(configuration); 
            })
            .UseKestrel(options =>
            {
#if DEBUG && IOS
                if (DeviceInfo.Current.DeviceType== DeviceType.Virtual)
                {
                    options.Listen(webHostParameters.ServerIpEndpoint);
                }
                else
                {
                    options.Listen(new IPEndPoint(IPAddress.Any, 5001));
                }
#elif MACCATALYST
                options.Listen(webHostParameters.ServerIpEndpoint);
#else
                options.Listen(new IPEndPoint(IPAddress.Any, 5001));
#endif
            })
            .UseContentRoot(AppDomain.CurrentDomain.BaseDirectory)
            .UseStartup<Startup>()
            .Build();

        #if ANDROID  
        App.Host = webHost;
        return webHost.RunPatchedAsync();
#else
        return webHost.RunAsync();
#endif
    }
}
