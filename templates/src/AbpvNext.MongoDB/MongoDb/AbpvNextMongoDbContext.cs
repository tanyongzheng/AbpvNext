using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace AbpvNext.MongoDB
{
    [ConnectionStringName("Default")]
    public class AbpvNextMongoDbContext : AbpMongoDbContext
    {

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);
        }
    }
}
