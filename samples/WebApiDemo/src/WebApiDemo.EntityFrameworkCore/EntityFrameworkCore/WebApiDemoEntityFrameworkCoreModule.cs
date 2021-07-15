using AbpvNext;
using AbpvNext.Data;
using AbpvNext.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace WebApiDemo.EntityFrameworkCore
{
    [DependsOn(
        typeof(WebApiDemoDomainModule),
        typeof(AbpvNextEntityFrameworkCoreSqlServerModule)
        )]
    public class WebApiDemoEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            //AbpvNextDomainStaticData.EFCoreDbMigrationSrcDirectoryPath = "samples/WebApiDemo";
            WebApiDemoEfCoreEntityExtensionMappings.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<WebApiDemoDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also WebApiDemoMigrationsDbContextFactory for EF Core tooling. */
                options.UseSqlServer();
            });
            // 替换空的数据迁移
            context.Services.Replace(ServiceDescriptor.Scoped<IAbpvNextDbSchemaMigrator, EntityFrameworkCoreWebApiDemoDbSchemaMigrator>());
        }
    }
}
