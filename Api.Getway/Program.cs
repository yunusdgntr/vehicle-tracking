using Api.Gateway;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace getwayAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
              .ConfigureServices(services =>
              {
                  services.AddOcelot();
              })
                .ConfigureAppConfiguration((hosting, config) =>
                {
                    config.AddJsonFile("ocelot.json", false, true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().Configure(async app => {
                        app.UseMiddleware<RequestInspector>();

                        await app.UseOcelot();
                    
                    }); ;
                });
    }
}
