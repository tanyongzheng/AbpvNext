using AbpvNext;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace WebApiDemo
{
    [DependsOn(
        typeof(WebApiDemoApplicationContractsModule),
        typeof(AbpvNextHttpApiClientModule)
    )]
    public class WebApiDemoHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(WebApiDemoApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
