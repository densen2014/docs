using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection; // 添加此 using 指令
using System.Net;

namespace KestrelWebHost;

public class WebHostProgram
{
    public static Task WebHostMain(WebHostParameters webHostParameters)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("apisettings.json", optional: true, reloadOnChange: true)
            .Build();

        var webHost = new WebHostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<IConfiguration>(configuration); 
            })
            .UseKestrel(options =>
            {
                options.Listen(new IPEndPoint(IPAddress.Any, webHostParameters?.ServerIpEndpoint?.Port ?? 5001));
                //options.Listen(webHostParameters.ServerIpEndpoint);
            })
            .UseContentRoot(AppDomain.CurrentDomain.BaseDirectory)
            .UseStartup<Startup>()
            .Build();

        return webHost.RunAsync();
    }
}
