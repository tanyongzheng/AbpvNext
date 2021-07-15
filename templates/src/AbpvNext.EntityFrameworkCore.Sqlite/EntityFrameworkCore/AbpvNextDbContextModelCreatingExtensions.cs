using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace AbpvNext.EntityFrameworkCore
{
    public static class AbpvNextDbContextModelCreatingExtensions
    {
        public static void ConfigureAbpvNext(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(AbpvNextConsts.DbTablePrefix + "YourEntities", AbpvNextConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}