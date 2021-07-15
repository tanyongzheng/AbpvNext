using WebApiDemo.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace WebApiDemo.Permissions
{
    public class WebApiDemoPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(WebApiDemoPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(WebApiDemoPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<WebApiDemoResource>(name);
        }
    }
}
