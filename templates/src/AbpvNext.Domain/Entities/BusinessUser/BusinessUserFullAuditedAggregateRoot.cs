using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Users;

namespace AbpvNext.Entities.BusinessUser
{
    /// <summary>
    /// 业务用户聚合根抽象类
    /// Id类型为Guid,和IdentityUser一致
    /// 一个业务用户对应一个身份用户，一个身份用户对应多个业务用户
    /// </summary>
    [Serializable]
    public abstract class BusinessUserFullAuditedAggregateRoot : FullAuditedAggregateRoot<Guid>, IMustHaveIdentityUser, IMultiTenant, IUser
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        public virtual Guid? TenantId { get; protected set; }

        /// <summary>
        /// 用户名
        /// 和IdentityUser同步
        /// </summary>
        public virtual string UserName { get; protected set; }

        /// <summary>
        /// 邮箱
        /// 和IdentityUser同步
        /// </summary>
        public virtual string Email { get; protected set; }


        /// <summary>
        /// 名字
        /// 和IdentityUser同步
        /// </summary>
        public virtual string Name { get; protected set; }

        /// <summary>
        /// 姓
        /// 和IdentityUser同步
        /// </summary>
        public virtual string Surname { get; protected set; }

        /// <summary>
        /// 是否确认邮箱
        /// 和IdentityUser同步
        /// </summary>
        public virtual bool EmailConfirmed { get; protected set; }

        /// <summary>
        /// 电话号码
        /// 和IdentityUser同步
        /// </summary>
        public virtual string PhoneNumber { get; protected set; }

        /// <summary>
        /// 是否确认电话号码
        /// 和IdentityUser同步
        /// </summary>
        public virtual bool PhoneNumberConfirmed { get; protected set; }

        /// <summary>
        /// 身份用户Id
        /// </summary>
        public virtual Guid IdentityUserId { get; protected set; }

        /// <summary>
        /// 身份用户
        /// </summary>
        public virtual IdentityUser IdentityUser { get; protected set;}

    }
}
