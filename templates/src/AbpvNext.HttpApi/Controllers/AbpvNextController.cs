using AbpvNext.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AbpvNext.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class AbpvNextController : AbpController
    {
        protected AbpvNextController()
        {
            LocalizationResource = typeof(AbpvNextResource);
        }
    }
}