using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace WebApiDemo.EntityFrameworkCore
{
    public static class WebApiDemoDbContextModelCreatingExtensions
    {
        public static void ConfigureWebApiDemo(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(WebApiDemoConsts.DbTablePrefix + "YourEntities", WebApiDemoConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}