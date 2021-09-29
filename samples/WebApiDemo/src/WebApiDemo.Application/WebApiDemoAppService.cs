using System;
using System.Collections.Generic;
using System.Text;
using WebApiDemo.Localization;
using Volo.Abp.Application.Services;
using AbpvNext;

namespace WebApiDemo
{
    /* Inherit your application services from this class.
     */
    public abstract class WebApiDemoAppService : AbpvNextAppService
    {
        protected WebApiDemoAppService()
        {
            LocalizationResource = typeof(WebApiDemoResource);
        }
    }
}
