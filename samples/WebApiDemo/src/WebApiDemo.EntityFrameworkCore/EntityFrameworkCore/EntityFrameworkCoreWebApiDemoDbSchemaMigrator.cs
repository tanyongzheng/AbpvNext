using System;
using System.Threading.Tasks;
using AbpvNext.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace WebApiDemo.EntityFrameworkCore
{
    public class EntityFrameworkCoreWebApiDemoDbSchemaMigrator
        : IAbpvNextDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreWebApiDemoDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the WebApiDemoDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<WebApiDemoDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
