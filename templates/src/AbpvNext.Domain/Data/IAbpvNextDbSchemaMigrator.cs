using System.Threading.Tasks;

namespace AbpvNext.Data
{
    public interface IAbpvNextDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
