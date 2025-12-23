using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace KestrelWebHost;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void Configure(IApplicationBuilder app)
    {
        app.Run(WebApp.OnHttpRequest);
    }
}
