using AbpvNext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp.Modularity;


namespace ConsoleApp
{
    [DependsOn(
        typeof(AbpvNextModule))]
    public class ConsoleAppModule : AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostEnvironment = context.Services.GetSingletonInstance<IHostEnvironment>();
            
            context.Services.AddHostedService<ConsoleAppHostedService>();
        }

    }
}