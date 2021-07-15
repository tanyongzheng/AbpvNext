using AbpvNext;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace WebApiDemo
{
    [DependsOn(
        typeof(WebApiDemoDomainSharedModule),
        typeof(AbpvNextApplicationContractsModule)
    )]
    public class WebApiDemoApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            WebApiDemoDtoExtensions.Configure();
        }
    }
}
