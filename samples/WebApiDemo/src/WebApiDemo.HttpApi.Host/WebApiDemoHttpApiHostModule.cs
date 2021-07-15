using System.IO;
using AbpvNext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApiDemo.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace WebApiDemo
{
    [DependsOn(
        typeof(WebApiDemoHttpApiModule),
        typeof(AbpvNextHttpApiWebCoreModule),
        typeof(WebApiDemoApplicationModule),
        typeof(WebApiDemoEntityFrameworkCoreModule)
    )]
    public class WebApiDemoHttpApiHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            ConfigureConventionalControllers();
            ConfigureVirtualFileSystem(context);
        }


        private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<WebApiDemoDomainSharedModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}WebApiDemo.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<WebApiDemoDomainModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}WebApiDemo.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<WebApiDemoApplicationContractsModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}WebApiDemo.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<WebApiDemoApplicationModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}WebApiDemo.Application"));
                });
            }
        }

        private void ConfigureConventionalControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(WebApiDemoApplicationModule).Assembly);
            });
        }
        

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

        }
    }
}
