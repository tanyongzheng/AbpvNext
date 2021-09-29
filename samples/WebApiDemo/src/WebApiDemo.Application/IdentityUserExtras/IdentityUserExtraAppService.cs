using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace WebApiDemo.IdentityUserExtras
{
    public class IdentityUserExtraAppService: WebApiDemoAppService
    {
        private readonly IRepository<IdentityUser, Guid> _identityUserRepository;

        public IdentityUserExtraAppService(
            IRepository<IdentityUser, Guid> identityUserRepository)
        {
            _identityUserRepository = identityUserRepository;
        }



        public async Task<Guid?> GetParentIdAsync(string userName)
        {
            try
            {
                var user = await _identityUserRepository.GetAsync(x=>x.UserName==userName);
                var parentId = user.GetProperty<Guid?>("ParentId");
                return parentId;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }
        }

        public async Task<IdentityUserDto> UpdateParentIdAsync(string userName)
        {
            try
            {
                var user = await _identityUserRepository.GetAsync(x=>x.UserName==userName);
                var userId = user.GetProperty<Guid?>("ParentId");
                var parentId = GuidGenerator.Create();
                user.SetProperty("ParentId", parentId);
                return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }
        }
    }
}
