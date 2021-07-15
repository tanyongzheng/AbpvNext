using System;
using System.Collections.Generic;
using System.Text;
using WebApiDemo.Localization;
using Volo.Abp.Application.Services;

namespace WebApiDemo
{
    /* Inherit your application services from this class.
     */
    public abstract class WebApiDemoAppService : ApplicationService
    {
        protected WebApiDemoAppService()
        {
            LocalizationResource = typeof(WebApiDemoResource);
        }
    }
}
