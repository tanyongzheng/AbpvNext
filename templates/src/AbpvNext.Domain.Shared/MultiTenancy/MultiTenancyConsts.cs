namespace AbpvNext.MultiTenancy
{
    public static class MultiTenancyConsts
    {
        /* Enable/disable multi-tenancy easily in a single point.
         * If you will never need to multi-tenancy, you can remove
         * related modules and code parts, including this file.
         */
        //public const bool IsEnabled = true;

        /// <summary>
        /// 是否启用多租户
        /// 改为静态变量，项目启动前配置，可放到Program类的最前面做配置
        /// </summary>
        public static bool IsEnabled = true;
    }
}
