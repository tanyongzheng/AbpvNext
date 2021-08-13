namespace AbpvNext
{
    /// <summary>
    /// 
    /// </summary>
    public static class AbpvNextDomainStaticData
    {
        /// <summary>
        /// Id4数据种子Api资源名
        /// 对应appsettings.json配置文件的IdentityServer配置
        /// 项目启动前配置，可放到Program类的最前面做配置
        /// </summary>
        public static string IdentityServerDataSeedApiResourceName = "AbpvNext";

        /// <summary>
        /// 解决方案下的EF Core数据迁移项目所在的源码目录
        /// 默认src
        /// </summary>
        public static string EFCoreDbMigrationSrcDirectoryPath = "src";

        /// <summary>
        /// EF Core数据迁移项目名
        /// 默认为.EntityFrameworkCore结尾的项目
        /// </summary>
        public static string EFCoreDbMigrationProjectName = "src";
    }
}