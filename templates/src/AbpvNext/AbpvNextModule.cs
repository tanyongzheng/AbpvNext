using System;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace AbpvNext
{
    [DependsOn(
        typeof(AbpAutofacModule)
    )]
    public class AbpvNextModule : AbpModule
    {

    }
}
