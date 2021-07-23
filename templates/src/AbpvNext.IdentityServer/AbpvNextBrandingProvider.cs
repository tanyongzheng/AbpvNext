using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace AbpvNext
{
    [Dependency(ReplaceServices = true)]
    public class AbpvNextBrandingProvider : DefaultBrandingProvider
    {
        //public override string AppName => "AbpvNext";
        public override string AppName => AbpvNextDomainStaticData.IdentityServerDataSeedApiResourceName;
    }
}
