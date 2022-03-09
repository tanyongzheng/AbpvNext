using System;
using Volo.Abp.Identity;
using JetBrains.Annotations;

namespace AbpvNext.Entities.BusinessUser;

/// <summary>
/// Standard interface for an entity that MUST have a IdentityUser.
/// </summary>
internal interface IMustHaveIdentityUser
{

    /// <summary>
    /// 身份用户Id
    /// </summary>
    [NotNull]
    Guid IdentityUserId { get;  }

    /// <summary>
    /// 身份用户
    /// </summary>
    IdentityUser IdentityUser { get;  }
}
