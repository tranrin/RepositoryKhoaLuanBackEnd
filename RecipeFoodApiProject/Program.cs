
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeFoodApiProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            try
            {
              //  var context = services.GetRequiredService<DataContext>();
                // var userManager = services.GetRequiredService<UserManager<AppUser>>();
                // await context.Database.MigrateAsync();
                //await Seed.SeedData(context, userManager);
            }
            catch (Exception ex)
            {// thong bao ra file log
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "co gi do sai sai roi");
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //  webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(logBuilder => {
                    logBuilder.ClearProviders(); // removes all providers from LoggerFactory
                    logBuilder.AddConsole();
                    //logBuilder.AddTraceSource("Information, ActivityTracing"); // Add Trace listener provider
                    logBuilder.AddTraceSource("Error"); // Add Trace listener provider
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
