using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using WebApiDemo.EntityFrameworkCore;

namespace WebApiDemo.IdentityUserExtras
{
    public class IdentityUserExtraRepository : EfCoreRepository<WebApiDemoDbContext, IdentityUser, Guid>, IIdentityUserExtraRepository
    {

        public IdentityUserExtraRepository(IDbContextProvider<WebApiDemoDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 获取字用户
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<List<IdentityUser>> FindChildUsers(Guid parentId)
        {
            var dbContext = await GetDbContextAsync();
            return await dbContext.Set<IdentityUser>()
                .Where(u => EF.Property<Guid?>(u, IdentityUserExtraPropertyConsts.ParentId) == parentId)
                .ToListAsync();
        }


    }
}
