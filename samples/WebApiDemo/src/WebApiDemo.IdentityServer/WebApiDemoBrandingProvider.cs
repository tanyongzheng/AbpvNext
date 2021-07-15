using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace WebApiDemo
{
    [Dependency(ReplaceServices = true)]
    public class WebApiDemoBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "WebApiDemo";
    }
}
