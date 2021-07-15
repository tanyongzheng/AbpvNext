using WebApiDemo.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace WebApiDemo.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class WebApiDemoController : AbpController
    {
        protected WebApiDemoController()
        {
            LocalizationResource = typeof(WebApiDemoResource);
        }
    }
}