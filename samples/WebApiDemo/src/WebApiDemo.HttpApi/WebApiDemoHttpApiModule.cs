using AbpvNext;
using Localization.Resources.AbpUi;
using WebApiDemo.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace WebApiDemo
{
    [DependsOn(
        typeof(WebApiDemoApplicationContractsModule),
        typeof(AbpvNextHttpApiModule)
        )]
    public class WebApiDemoHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureLocalization();
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<WebApiDemoResource>()
                    .AddBaseTypes(
                        typeof(AbpUiResource)
                    );
            });
        }
    }
}
