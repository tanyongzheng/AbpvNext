using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace AbpvNext.Data
{
    /* This is used if database provider does't define
     * IAbpvNextDbSchemaMigrator implementation.
     */
    public class NullAbpvNextDbSchemaMigrator : IAbpvNextDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}