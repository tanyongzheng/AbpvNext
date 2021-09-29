using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace WebApiDemo.IdentityUserExtras
{
    public interface IIdentityUserExtraRepository: IRepository<IdentityUser, Guid>
    {

        /// <summary>
        /// 获取字用户
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        Task<List<IdentityUser>> FindChildUsers(Guid parentId);
    }
}
