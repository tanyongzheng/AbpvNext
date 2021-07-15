using AbpvNext;
using WebApiDemo.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace WebApiDemo.DbMigrator
{
    [DependsOn(
        typeof(AbpvNextModule),
        typeof(WebApiDemoEntityFrameworkCoreModule),
        typeof(WebApiDemoApplicationContractsModule)
        )]
    public class WebApiDemoDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
