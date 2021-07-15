using System;
using System.Collections.Generic;
using System.Text;
using AbpvNext.Localization;
using Volo.Abp.Application.Services;

namespace AbpvNext
{
    /* Inherit your application services from this class.
     */
    public abstract class AbpvNextAppService : ApplicationService
    {
        protected AbpvNextAppService()
        {
            LocalizationResource = typeof(AbpvNextResource);
        }
    }
}
