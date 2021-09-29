using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;
using WebApiDemo.Users;

namespace WebApiDemo.EntityFrameworkCore
{
    public static class WebApiDemoEfCoreEntityExtensionMappings
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            WebApiDemoGlobalFeatureConfigurator.Configure();
            WebApiDemoModuleExtensionConfigurator.Configure();

            OneTimeRunner.Run(() =>
            {
                /* You can configure extra properties for the
                 * entities defined in the modules used by your application.
                 *
                 * This class can be used to map these extra properties to table fields in the database.
                 *
                 * USE THIS CLASS ONLY TO CONFIGURE EF CORE RELATED MAPPING.
                 * USE WebApiDemoModuleExtensionConfigurator CLASS (in the Domain.Shared project)
                 * FOR A HIGH LEVEL API TO DEFINE EXTRA PROPERTIES TO ENTITIES OF THE USED MODULES
                 *
                 * Example: Map a property to a table field:

                     ObjectExtensionManager.Instance
                         .MapEfCoreProperty<IdentityUser, string>(
                             "MyProperty",
                             (entityBuilder, propertyBuilder) =>
                             {
                                 propertyBuilder.HasMaxLength(128);
                             }
                         );

                 * See the documentation for more:
                 * https://docs.abp.io/en/abp/latest/Customizing-Application-Modules-Extending-Entities
                 */

                #region Abp框架自带实体拓展字段映射配置
                // EFCore数据库拓展字段
                ObjectExtensionManager.Instance
                    .MapEfCoreProperty<IdentityUser, Guid?>(
                        IdentityUserExtraPropertyConsts.ParentId,
                        (entityBuilder, propertyBuilder) =>
                        {
                            // propertyBuilder.
                            entityBuilder.HasIndex(IdentityUserExtraPropertyConsts.ParentId);
                        }
                    );

                // 身份用户添加拓展字段（前端显示在extraProperties对象中）
                ObjectExtensionManager.Instance.Modules()
                .ConfigureIdentity(identity =>
                {
                    identity.ConfigureUser(user =>
                    {
                        user.AddOrUpdateProperty<string>( //property type: string
                            IdentityUserExtraPropertyConsts.ParentId, //property name
                            property =>
                            {
                            //validation rules
                            // property.Attributes.Add(new RequiredAttribute());
                            // property.Attributes.Add(new StringLengthAttribute(64));
                        }
                        );
                    });
                });
                #endregion

            });
        }
    }
}
