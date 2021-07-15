using Volo.Abp.Settings;

namespace WebApiDemo.Settings
{
    public class WebApiDemoSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(WebApiDemoSettings.MySetting1));
        }
    }
}
