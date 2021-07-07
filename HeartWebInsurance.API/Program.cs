using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using NLog.Web;
using NLog;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Presentation
{
    public class Program
    {
        public static IConfiguration Configuration
         => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            try
            {
                logger.Info("Starting up...");
                var host = CreateHostBuilder(args).Build();
                using (var scope = host.Services.CreateScope())
                    scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

                host.Run();
                logger.Info("Shutting down...");
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Api host terminated unexpectedly");
                LogManager.Flush();
            }
            finally { LogManager.Shutdown(); }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseStartup<Startup>().ConfigureLogging(
                         logging =>
                         {
                             logging.ClearProviders();
                         }
                         ).UseNLog();
                 });
    }
}