using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace WebApiDemo.IdentityUserExtras
{
    public class IdentityUserExtraAppService: WebApiDemoAppService
    {
        private readonly IRepository<IdentityUser, Guid> _identityUserRepository;
        private readonly IIdentityUserExtraRepository _identityUserExtraRepository;
        private readonly IdentityUserManager _identityUserManager;

        public IdentityUserExtraAppService(
            IRepository<IdentityUser, Guid> identityUserRepository,
            IIdentityUserExtraRepository identityUserExtraRepository,
            IdentityUserManager identityUserManager)
        {
            _identityUserRepository = identityUserRepository;
            _identityUserExtraRepository = identityUserExtraRepository;
            _identityUserManager = identityUserManager;
        }

        public async Task<Guid?> GetParentIdAsync(string userName)
        {
            try
            {
                var user = await _identityUserRepository.GetAsync(x=>x.UserName==userName);
                var parentId = user.GetProperty<Guid?>(IdentityUserExtraPropertyConsts.ParentId);
                return parentId;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }
        }
        public async Task<List<IdentityUserDto>> GetChildUsersAsync(string userName)
        {
            try
            {
                var user = await _identityUserExtraRepository.GetAsync(x=>x.UserName==userName);
                var chidlUsers = await _identityUserExtraRepository.FindChildUsers(user.Id);
                if (chidlUsers == null || chidlUsers.Count == 0)
                {
                    return null;
                }
                return ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(chidlUsers);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }
        }

        public async Task<IdentityUserDto> UpdateParentIdAsync(string userName,Guid parentId)
        {
            try
            {
                var user = await _identityUserRepository.GetAsync(x=>x.UserName==userName);
                var pid = user.GetProperty<Guid?>(IdentityUserExtraPropertyConsts.ParentId);
                user.SetProperty(IdentityUserExtraPropertyConsts.ParentId, parentId);
                return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }
        }

        public async Task<IdentityUserDto> AddChildUserAsync(string userName,string childUserSuffix)
        {
            var validationResults = new List<ValidationResult>();
            try
            {
                var childUserName = userName + childUserSuffix;
                if((await _identityUserRepository.CountAsync(x => x.UserName == childUserName)) > 0)
                {
                    return ObjectMapper.Map<IdentityUser, IdentityUserDto>(await _identityUserRepository.GetAsync(x=>x.UserName==childUserName));
                }
                var user = await _identityUserRepository.GetAsync(x=>x.UserName == userName);
                var childUserEmail = childUserName + "@test.com";
                var childUser = new IdentityUser(GuidGenerator.Create(),childUserName,childUserEmail);
                var parentId = user.Id;
                childUser.SetProperty(IdentityUserExtraPropertyConsts.ParentId, parentId);
                childUser.CreationTime = Clock.Now;
                childUser.Name = childUserName;
                childUser.Surname = childUserName;

                var identityResult = await _identityUserManager.CreateAsync(childUser, "Test_123456");
                if (!identityResult.Succeeded)
                {
                    foreach(var err in identityResult.Errors)
                    {
                        validationResults.Add(new ValidationResult(err.Description+"，错误码："+err.Code));
                    }
                    throw new AbpValidationException(validationResults);
                }
                return ObjectMapper.Map<IdentityUser, IdentityUserDto>(childUser);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }
        }


    }
}
