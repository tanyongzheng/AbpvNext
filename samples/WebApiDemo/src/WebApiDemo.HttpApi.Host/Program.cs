using System;
using AbpvNext;
using AbpvNext.MultiTenancy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace WebApiDemo
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var logPath = "Logs/log.txt";
            // var logOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}";
#if DEBUG
            logPath = AppContext.BaseDirectory + logPath;
#endif
            MultiTenancyConsts.IsEnabled = true;
            AbpvNextDomainStaticData.IdentityServerDataSeedApiResourceName = "WebApiDemo";
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File(logPath))
#if DEBUG
                .WriteTo.Async(c => c.Console())
#endif
                .CreateLogger();

            try
            {
                Log.Information("Starting WebApiDemo.HttpApi.Host.");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseAutofac()
                .UseSerilog();
    }
}
