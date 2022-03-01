# AbpvNext

#### 介绍
Abp vNext框架的基础封装<br/>
版权说明：<br/>
部分业务需求可能需要修改源代码，根据Abp开源协议[LGPL-3.0 License](https://github.com/abpframework/abp/blob/dev/LICENSE.md) 修改部分需要进行开源，此框架在abp基础上进行封装及修改，不涉及具体业务，可基于该框架简化开发。<br/>
开源协议介绍：https://www.runoob.com/w3cnote/open-source-license.html


#### 软件架构

##### 1.AbpvNext
 基础模块，依赖只Abp的Autofac模块，可用于控制台，WPF等项目

##### 2.AbpvNext.Domain.Shared
  领域共享模块
 1. 集成Abp Web模板的所有依赖
 2. 是否启用多租户常量（MultiTenancyConsts.IsEnabled）改为静态变量（项目启动前配置，可放到Program类的最前面做配置）

##### 3.AbpvNext.Domain
  领域模块
  1. 集成Abp Web模板的所有依赖
  2. Domain管理服务基类（DomainManagerBase）
  3. Domain带混合缓存管理服务类（DomainHybridCacheManager）
  4. 修改模板自带的IdentityServer数据种子类（IdentityServerDataSeedContributor）<br/>
      把Id4相关的一些客Client,ClientId等由固定的AbpvNext改为按配置生成。<br/>
      对应appsettings.json配置文件的IdentityServer配置 <br/>
      把AbpvNextDomainStaticData类下的IdentityServerDataSeedApiResourceName静态变量值"AbpvNext"改为对应项目的名字。
```js
 "IdentityServer": {
    "Clients": {
      "AbpvNext_Web": {
        "ClientId": "AbpvNext_Web",
        "ClientSecret": "1q2w3e*",
        "RootUrl": "https://localhost:44306"
      },
      "AbpvNext_App": {
        "ClientId": "AbpvNext_App",
        "ClientSecret": "1q2w3e*",
        "RootUrl": "http://localhost:4200"
      },
      "AbpvNext_BlazorServerTiered": {
        "ClientId": "AbpvNext_BlazorServerTiered",
        "ClientSecret": "1q2w3e*",
        "RootUrl": "https://localhost:44314"
      },
      "AbpvNext_Swagger": {
        "ClientId": "AbpvNext_Swagger",
        "ClientSecret": "1q2w3e*",
        "RootUrl": "https://localhost:44387"
      }
    }
  }
```

##### 4.AbpvNext.Application.Contracts
  应用契约模块
  1. 集成Abp Web模板的所有依赖

##### 5.AbpvNext.Application
  应用模块
  1. 集成Abp Web模板的所有依赖

##### 6.AbpvNext.EntityFrameworkCore.*
  EFCore关系数据库模块（自定义仓库层）
  1. 集成Abp Web模板的所有依赖
  2. 集成Dapper仓储

##### 7.AbpvNext.MongoDB
  MongoDB数据库模块（自定义仓库层）
  1. 集成Abp Web模板的所有依赖

##### 8.AbpvNext.HttpApi
  远程Http Api模块
  1. 集成Abp Web模板的所有依赖

##### 9.AbpvNext.HttpApi.Client
  Http客户端代理模块
  1. 集成Abp Web模板的所有依赖

##### 10.AbpvNext.HttpApi.WebCore
  WebApi模块
  1. 集成Abp Web模板的所有依赖
  2. 已做好基本配置（Swagger，多语言等）
  3. 项目中需要对实际的虚拟文件做配置
  4. 项目中需要配置混合缓存


#### 创建Web Api项目

##### 1. 创建一个空的解决方案ProjectName
   

##### 2. Domain.Shared项目

1. 创建一个类库（.NET Standard 2.0及以上版本） ProjectName.Domain.Shared
2. 引用AbpvNext.Domain.Shared
3. 创建Localization文件夹存本地化语言，创建ProjectName文件夹并存放多语言的json文件，创建ProjectNameResource本地化语言资源类
~~~CSharp
    [LocalizationResourceName("ProjectName")]
    public class ProjectNameResource
    {

    }
~~~

4. 创建全局功能配置静态类ProjectNameGlobalFeatureConfigurator，譬如配置某些模块功能的启用/禁用 （非必要）
~~~CSharp

    public static class ProjectNameGlobalFeatureConfigurator
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            OneTimeRunner.Run(() =>
            {
                /* You can configure (enable/disable) global features of the used modules here.
                 *
                 * YOU CAN SAFELY DELETE THIS CLASS AND REMOVE ITS USAGES IF YOU DON'T NEED TO IT!
                 *
                 * Please refer to the documentation to lear more about the Global Features System:
                 * https://docs.abp.io/en/abp/latest/Global-Features
                 */
            });
        }
    }
~~~

5. 创建全局模块拓展配置静态类ProjectNameModuleExtensionConfigurator，譬如配置已存在的领域实体模型的属性或给框架内的领域实体添加拓展字段等 （非必要）
~~~CSharp

    public static class ProjectNameModuleExtensionConfigurator
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            OneTimeRunner.Run(() =>
            {
                ConfigureExistingProperties();
                ConfigureExtraProperties();
            });
        }

        private static void ConfigureExistingProperties()
        {
            /* You can change max lengths for properties of the
             * entities defined in the modules used by your application.
             *
             * Example: Change user and role name max lengths

               IdentityUserConsts.MaxNameLength = 99;
               IdentityRoleConsts.MaxNameLength = 99;

             * Notice: It is not suggested to change property lengths
             * unless you really need it. Go with the standard values wherever possible.
             *
             * If you are using EF Core, you will need to run the add-migration command after your changes.
             */
        }

        private static void ConfigureExtraProperties()
        {
            /* You can configure extra properties for the
             * entities defined in the modules used by your application.
             *
             * This class can be used to define these extra properties
             * with a high level, easy to use API.
             *
             * Example: Add a new property to the user entity of the identity module

               ObjectExtensionManager.Instance.Modules()
                  .ConfigureIdentity(identity =>
                  {
                      identity.ConfigureUser(user =>
                      {
                          user.AddOrUpdateProperty<string>( //property type: string
                              "SocialSecurityNumber", //property name
                              property =>
                              {
                                  //validation rules
                                  property.Attributes.Add(new RequiredAttribute());
                                  property.Attributes.Add(new StringLengthAttribute(64) {MinimumLength = 4});

                                  //...other configurations for this property
                              }
                          );
                      });
                  });

             * See the documentation for more:
             * https://docs.abp.io/en/abp/latest/Module-Entity-Extensions
             */
        }
    }

~~~

6. 添加模块类ProjectNameDomainSharedModule
~~~CSharp

    [DependsOn(typeof(AbpvNextDomainSharedModule))]
    public class ProjectNameDomainSharedModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            ProjectNameGlobalFeatureConfigurator.Configure();
            ProjectNameModuleExtensionConfigurator.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<ProjectNameDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<ProjectNameResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/ProjectName");

                options.DefaultResourceType = typeof(ProjectNameResource);
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("ProjectName", typeof(ProjectNameResource));
            });
        }
    }
~~~

7. 领域模块的错误码静态类ProjectNameDomainErrorCodes （非必要）
~~~CSharp

    public static class ProjectNameDomainErrorCodes
    {
        /* You can add your business exception error codes here, as constants */
    }
~~~

##### 3. Domain项目

1. 创建一个类库（.NET 5.0及以上版本） ProjectName.Domain
2. 引用AbpvNext.Domain
3. 引用本地Domain.Shared项目
4. 按照Demo或者Apb官方模板添加领域项目的设置项Settings（非必要）
5. 创建领域模块常量类ProjectNameConsts，用于配置数据库表前缀，Schema等（非必要）
6. 创建领域模块类ProjectNameDomainModule

~~~CSharp
    [DependsOn(
        typeof(ProjectNameDomainSharedModule),
        typeof(AbpvNextDomainModule)
    )]
    public class ProjectNameDomainModule : AbpModule
    {

    }
~~~

##### 4. Application.Contracts项目

1. 创建一个类库（.NET Standard 2.0及以上版本） ProjectName.Application.Contracts
2. 引用AbpvNext.Application.Contracts
3. 引用本地Domain.Shared项目
4. 按照Demo或者Apb官方模板添加权限配置（Permissions权限定义类，权限提供类ProjectNamePermissionDefinitionProvider)
5. 添加Dto拓展类ProjectNameDtoExtensions（非必要）
6. 添加应用契约模块类ProjectNameApplicationContractsModule
~~~CSharp

    [DependsOn(
        typeof(ProjectNameDomainSharedModule),
        typeof(AbpvNextApplicationContractsModule)
    )]
    public class ProjectNameApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            ProjectNameDtoExtensions.Configure();
        }
    }
~~~

##### 5. Application项目

1. 创建一个类库（.NET 5.0及以上版本） ProjectName.Application
2. 引用AbpvNext.Application
3. 引用本地Domain项目
4. 引用本地Application.Contracts项目
5. 添加自动映射类ApplicationAutoMapperProfile

~~~CSharp

    public class ProjectNameApplicationAutoMapperProfile : Profile
    {
        public ProjectNameApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
        }
    }
~~~

6. 添加应用模块类ProjectNameApplicationModule,并添加自动映射配置
~~~CSharp

    [DependsOn(
        typeof(ProjectNameDomainModule),
        typeof(AbpvNextApplicationModule)
    )]
    public class ProjectNameApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<ProjectNameApplicationModule>();
            });
        }
    }
~~~

7. 添加应用服务基类ProjectNameAppService，并配置多国语言本地化，其他应用服务类可以集成该基类（非必要）
~~~CSharp

    public abstract class ProjectNameAppService : ApplicationService
    {
        protected ProjectNameAppService()
        {
            LocalizationResource = typeof(ProjectNameResource);
        }
    }
~~~

##### 6. EF Core项目

1. 创建一个类库（.NET 5.0及以上版本） ProjectName.EntityFrameworkCore
2. 引用AbpvNext.EntityFrameworkCore.SqlServer（不同数据引用对应的包）
3. 引用AbpvNext.Domain
4. 引用本地Domain项目
5. 添加数据库上下文类ProjectNameDbContext
~~~CSharp

    [ConnectionStringName("Default")]
    public class ProjectNameDbContext : AbpDbContext<ProjectNameDbContext>
    {
        public ProjectNameDbContext(DbContextOptions<ProjectNameDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

        }
    }
~~~

5. 添加EF实体拓展映射静态类ProjectNameEfCoreEntityExtensionMappings （非必要）
~~~CSharp

    public static class ProjectNameEfCoreEntityExtensionMappings
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            ProjectNameGlobalFeatureConfigurator.Configure();
            ProjectNameModuleExtensionConfigurator.Configure();

            OneTimeRunner.Run(() =>
            {
                /* You can configure extra properties for the
                 * entities defined in the modules used by your application.
                 *
                 * This class can be used to map these extra properties to table fields in the database.
                 *
                 * USE THIS CLASS ONLY TO CONFIGURE EF CORE RELATED MAPPING.
                 * USE ProjectNameModuleExtensionConfigurator CLASS (in the Domain.Shared project)
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
            });
        }
    }
~~~


6. 添加EF模块类ProjectNameEntityFrameworkCoreModule,并添加拓展映射配置，及数据库上下文配置
~~~CSharp
    [DependsOn(
        typeof(ProjectNameDomainModule),
        typeof(AbpvNextDomainModule),
        typeof(AbpvNextEntityFrameworkCoreSqlServerModule)
        )]
    public class ProjectNameEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            ProjectNameEfCoreEntityExtensionMappings.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ProjectNameDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also ProjectNameMigrationsDbContextFactory for EF Core tooling. */
                options.UseSqlServer();
            });

            // 替换空的数据迁移
            context.Services.Replace(ServiceDescriptor.Scoped<IAbpvNextDbSchemaMigrator, EntityFrameworkCoreProjectNameDbSchemaMigrator>());
        }
    }
~~~

7. 添加数据库上下文工厂DbContextFactory，用于控制台的Add-Migration， Update-Database数据迁移命令

~~~CSharp

    public class ProjectNameDbContextFactory : IDesignTimeDbContextFactory<ProjectNameDbContext>
    {
        public ProjectNameDbContext CreateDbContext(string[] args)
        {
            ProjectNameEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<ProjectNameDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new ProjectNameDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ProjectName.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
~~~

8. 添加数据迁移类,实现IAbpvNextDbSchemaMigrator接口

~~~CSharp

    public class EntityFrameworkCoreProjectNameDbSchemaMigrator
        : IAbpvNextDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreProjectNameDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the ProjectNameDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<ProjectNameDbContext>()
                .Database
                .MigrateAsync();
        }
    }
~~~

9. 项目引用EF Core工具
```xml
	<ItemGroup>
		<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="5.0.*">
			<IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
			<PrivateAssets>compile; contentFiles; build; buildMultitargeting; buildTransitive; analyzers; native</PrivateAssets>
		</PackageReference>
	</ItemGroup>
```
##### 7. HttpApi项目

1. 创建一个类库（.NET 5.0及以上版本） ProjectName.HttpApi
2. 引用AbpvNext.HttpApi
3. 引用本地Application.Contracts项目
4. 添加HttpApi模块类ProjectNameHttpApiModule，并配置多国语言本地化
~~~CSharp

    [DependsOn(
        typeof(ProjectNameApplicationContractsModule),
        typeof(AbpvNextHttpApiModule)
        )]
    public class ProjectNameHttpApiModule : AbpModule
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
                    .Get<ProjectNameResource>()
                    .AddBaseTypes(
                        typeof(AbpUiResource)
                    );
            });
        }
    }
~~~

##### 8. HttpApi.Client项目

1. 创建一个类库（.NET 5.0及以上版本） ProjectName.HttpApi
2. 引用AbpvNext.HttpApi.Client
3. 引用本地Application.Contracts项目
4. 添加HttpApi模块类ProjectNameHttpApiClientModule，并配置代理客户端
~~~CSharp

    [DependsOn(
        typeof(ProjectNameApplicationContractsModule),
        typeof(AbpvNextHttpApiClientModule)
    )]
    public class ProjectNameHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(ProjectNameApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
~~~

##### 9. HttpApi.Host项目

1. 创建一个WebApi项目（.NET 5.0及以上版本） ProjectName.HttpApi.Host
2. 引用AbpvNext.HttpApi.WebCore
3. 引用本地Application项目
4. 引用本地EntityFrameworkCore项目
5. 引用本地HttpApi项目
6. 添加Host模块类，并配置虚拟文件，动态API等
~~~CSharp

    [DependsOn(
        typeof(ProjectNameHttpApiModule),
        typeof(AbpvNextHttpApiWebCoreModule),
        typeof(ProjectNameApplicationModule),
        typeof(ProjectNameEntityFrameworkCoreModule)
    )]
    public class ProjectNameHttpApiHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            ConfigureConventionalControllers();
            ConfigureVirtualFileSystem(context);
        }


        private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<ProjectNameDomainSharedModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}ProjectName.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<ProjectNameDomainModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}ProjectName.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<ProjectNameApplicationContractsModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}ProjectName.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<ProjectNameApplicationModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}ProjectName.Application"));
                });
            }
        }

        private void ConfigureConventionalControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(ProjectNameApplicationModule).Assembly);
            });
        }
        

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

        }
    }
~~~

7. Startup类去掉其他配置，修改为

~~~CSharp

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<ProjectNameHttpApiHostModule>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.InitializeApplication();
        }
    }
~~~

8. Program修改为，并配置多租户及id4数据种子资源名

~~~CSharp

    public class Program
    {
        public static int Main(string[] args)
        {
            MultiTenancyConsts.IsEnabled = true;
            AbpvNextDomainStaticData.IdentityServerDataSeedApiResourceName = "ProjectName";
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
#if DEBUG
                .WriteTo.Async(c => c.Console())
#endif
                .CreateLogger();

            try
            {
                Log.Information("Starting ProjectName.HttpApi.Host.");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseAutofac()
                .UseSerilog();
    }
~~~

##### 10. DbMigrator项目

1. 创建一个控制台项目（.NET 5.0及以上版本） ProjectName.DbMigrator
2. 引用AbpvNext
3. 引用本地Application.Contracts项目
4. 引用本地EntityFrameworkCore项目
5. 添加数据迁移模块类，配置后台作业

~~~CSharp

    [DependsOn(
        typeof(AbpvNextModule),
        typeof(ProjectNameEntityFrameworkCoreModule),
        typeof(ProjectNameApplicationContractsModule)
        )]
    public class ProjectNameDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
~~~

6. 添加数据迁移主机服务类DbMigratorHostedService，并将获取的数据迁移服务改为封装的AbpvNextDbMigrationService

~~~CSharp
    public class DbMigratorHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IConfiguration _configuration;

        public DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime, IConfiguration configuration)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _configuration = configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var application = AbpApplicationFactory.Create<ProjectNameDbMigratorModule>(options =>
            {
                options.Services.ReplaceConfiguration(_configuration);
                options.UseAutofac();
                options.Services.AddLogging(c => c.AddSerilog());
            }))
            {
                application.Initialize();

                await application
                    .ServiceProvider
                    .GetRequiredService<AbpvNextDbMigrationService>()
                    .MigrateAsync();

                application.Shutdown();

                _hostApplicationLifetime.StopApplication();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }

~~~

7. 修改Program类，并配置多租户及id4数据种子资源名

~~~CSharp

    class Program
    {
        static async Task Main(string[] args)
        {
            MultiTenancyConsts.IsEnabled = true;
            AbpvNextDomainStaticData.IdentityServerDataSeedApiResourceName = "ProjectName";
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
#if DEBUG
                .MinimumLevel.Override("ProjectName", LogEventLevel.Debug)
#else
                .MinimumLevel.Override("ProjectName", LogEventLevel.Information)
#endif
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
                .WriteTo.Async(c => c.Console())
                .CreateLogger();

            await CreateHostBuilder(args).RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(build =>
                {
                    build.AddJsonFile("appsettings.secrets.json", optional: true);
                })
                .ConfigureLogging((context, logging) => logging.ClearProviders())
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<DbMigratorHostedService>();
                });
    }

~~~

##### 11. IdentityServer


#### 数据迁移使用

如不按照约定的项目结构则项目迁移会报错，譬如报错::“对象名 'AbpSettings' 无效。”

1. 项目要放到src目录下，或者设置数据迁移目录：AbpvNextDomainStaticData.EFCoreDbMigrationSrcDirectoryPath
2. 使用EFCore则项目名必须是以EntityFrameworkCore结尾或者使用变量配置自定义EFCore项目名

~~~CSharp
 AbpvNextDomainStaticData.EFCoreDbMigrationProjectName = "ProjectName.EntityFrameworkCore";
~~~
3. 升级Abp Cli版本和项目版本的Abp一致

4. 如果是4.4.0版本及以下升级过来的，或有和Abp自带实体共享表的，4.4.0及以后版本官方模板去掉了.EntityFrameworkCore.DbMigrations项目，
做数据迁移的时候需要删除掉EF Core的数据上下文中共享表的实体及相关配置。如4.4.0之前版本的模板中有AppUser。
  4.4.0及之后版本不支持共享表，需要添加字段只能使用表拓展字段，需要在EfCoreEntityExtensionMappings结尾的类中添加对应的拓展字段配置，可参考本项目的WebApiDemo


~~~CSharp

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

                #region 共享表拓展字段映射配置
                // EFCore数据库拓展字段
                ObjectExtensionManager.Instance
                    .MapEfCoreProperty<IdentityUser, Guid?>(
                        nameof(AppIdentityUser.ParentId),
                        (entityBuilder, propertyBuilder) =>
                        {
                            // propertyBuilder.
                            entityBuilder.HasIndex(nameof(AppIdentityUser.ParentId));
                        }
                    );

                // 身份用户添加拓展字段（前端显示在extraProperties对象中）
                ObjectExtensionManager.Instance.Modules()
                .ConfigureIdentity(identity =>
                {
                    identity.ConfigureUser(user =>
                    {
                        user.AddOrUpdateProperty<string>( //property type: string
                            "ParentId", //property name
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
~~~
前端使用拓展字段

~~~js
// 展示给前端
"extraProperties": {
        "ParentId": "576511c8-5d68-c607-a5ec-39ff3db15809"
      }
~~~

5. abp 4.4.0及以上版本更新说明

https://community.abp.io/articles/unifying-dbcontexts-for-ef-core-removing-the-ef-core-migrations-project-nsyhrtna

https://blog.abp.io/abp/ABP-Platform-4-4-RC-Has-Been-Released

https://github.com/abpframework/abp/releases/tag/4.4.0-rc.1


6. abp 5.1及以上版本PostgreSql数据库报错

> Cannot write DateTime with Kind=Local to PostgreSQL type 'timestamp with time zone', only UTC is supported. Note that it's not possible to mix DateTimes with different Kinds in an array/range. See the Npgsql.EnableLegacyTimestampBehavior AppContext switch to enable legacy behavior

或者数据迁移报错1：
> Failed executing DbCommand (105ms) [Parameters=[@__providerName_0='?'], CommandType='Text', CommandTimeout='30']
SELECT a."Id", a."Name", a."ProviderKey", a."ProviderName", a."Value"
FROM "AbpSettings" AS a
WHERE (a."ProviderName" = @__providerName_0) AND (a."ProviderKey" IS NULL)
[16:47:52 ERR] An exception occurred while iterating over the results of a query for context type 'Volo.Abp.SettingManagement.EntityFrameworkCore.SettingManagementDbContext'.
Npgsql.PostgresException (0x80004005): 42P01: 关系 "AbpSettings" 不存在

> POSITION: 77
   at Npgsql.Internal.NpgsqlConnector.<ReadMessage>g__ReadMessageLong|213_0(NpgsqlConnector connector, Boolean async, DataRowLoadingMode dataRowLoadingMode, Boolean readingNotifications, Boolean isReadingPrependedMessage)
   at Npgsql.NpgsqlDataReader.NextResult(Boolean async, Boolean isConsuming, CancellationToken cancellationToken)
   at Npgsql.NpgsqlCommand.ExecuteReader(CommandBehavior behavior, Boolean async, CancellationToken cancellationToken)
   at Npgsql.NpgsqlCommand.ExecuteReader(CommandBehavior behavior, Boolean async, CancellationToken cancellationToken)
   at Npgsql.NpgsqlCommand.ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReaderAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReaderAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.Query.Internal.SplitQueryingEnumerable`1.AsyncEnumerator.InitializeReaderAsync(AsyncEnumerator enumerator, CancellationToken cancellationToken)
   at Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.NpgsqlExecutionStrategy.ExecuteAsync[TState,TResult](TState state, Func`4 operation, Func`4 verifySucceeded, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.Query.Internal.SplitQueryingEnumerable`1.AsyncEnumerator.MoveNextAsync()
  Exception data:
    Severity: 错误
    SqlState: 42P01
    MessageText: 关系 "AbpSettings" 不存在
    Position: 77
    File: parse_relation.c
    Line: 1384
    Routine: parserOpenTable
Npgsql.PostgresException (0x80004005): 42P01: 关系 "AbpSettings" 不存在

> POSITION: 77
   at Npgsql.Internal.NpgsqlConnector.<ReadMessage>g__ReadMessageLong|213_0(NpgsqlConnector connector, Boolean async, DataRowLoadingMode dataRowLoadingMode, Boolean readingNotifications, Boolean isReadingPrependedMessage)
   at Npgsql.NpgsqlDataReader.NextResult(Boolean async, Boolean isConsuming, CancellationToken cancellationToken)
   at Npgsql.NpgsqlCommand.ExecuteReader(CommandBehavior behavior, Boolean async, CancellationToken cancellationToken)
   at Npgsql.NpgsqlCommand.ExecuteReader(CommandBehavior behavior, Boolean async, CancellationToken cancellationToken)
   at Npgsql.NpgsqlCommand.ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReaderAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReaderAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.Query.Internal.SplitQueryingEnumerable`1.AsyncEnumerator.InitializeReaderAsync(AsyncEnumerator enumerator, CancellationToken cancellationToken)
   at Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.NpgsqlExecutionStrategy.ExecuteAsync[TState,TResult](TState state, Func`4 operation, Func`4 verifySucceeded, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.Query.Internal.SplitQueryingEnumerable`1.AsyncEnumerator.MoveNextAsync()
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.ToListAsync[TSource](IQueryable`1 source, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.ToListAsync[TSource](IQueryable`1 source, CancellationToken cancellationToken)
   at Volo.Abp.SettingManagement.EntityFrameworkCore.EfCoreSettingRepository.GetListAsync(String providerName, String providerKey, CancellationToken cancellationToken)
  Exception data:
    Severity: 错误
    SqlState: 42P01
    MessageText: 关系 "AbpSettings" 不存在
    Position: 77
    File: parse_relation.c
    Line: 1384
    Routine: parserOpenTable

报错2：

>  Failed executing DbCommand (25ms) [Parameters=[@p15='?' (DbType = Guid), @p0='?', @p1='?', @p16='?', @p2='?' (DbType = DateTime), @p3='?' (DbType = Guid), @p4='?' (DbType = Guid), @p5='?' (DbType = DateTime), @p6='?', @p7='?', @p8='?' (DbType = Boolean), @p9='?', @p10='?' (DbType = Boolean), @p11='?' (DbType = DateTime), @p12='?' (DbType = Guid), @p13='?', @p14='?' (DbType = Boolean), @p65='?' (DbType = Guid), @p17='?' (DbType = Int32), @p18='?' (DbType = Int32), @p19='?' (DbType = Int32), @p20='?' (DbType = Boolean), @p21='?' (DbType = Boolean), @p22='?' (DbType = Boolean), @p23='?' (DbType = Boolean), @p24='?', @p25='?' (DbType = Boolean), @p26='?' (DbType = Boolean), @p27='?' (DbType = Int32), @p28='?' (DbType = Boolean), @p29='?', @p30='?', @p31='?', @p32='?', @p33='?', @p34='?', @p66='?', @p35='?' (DbType = Int32), @p36='?' (DbType = DateTime), @p37='?' (DbType = Guid), @p38='?' (DbType = Guid), @p39='?' (DbType = DateTime), @p40='?', @p41='?' (DbType = Int32), @p42='?' (DbType = Boolean), @p43='?' (DbType = Boolean), @p44='?', @p45='?' (DbType = Boolean), @p46='?', @p47='?' (DbType = Int32), @p48='?' (DbType = Boolean), @p49='?' (DbType = Boolean), @p50='?' (DbType = DateTime), @p51='?' (DbType = Guid), @p52='?', @p53='?', @p54='?', @p55='?' (DbType = Int32), @p56='?' (DbType = Int32), @p57='?' (DbType = Boolean), @p58='?' (DbType = Boolean), @p59='?' (DbType = Boolean), @p60='?' (DbType = Boolean), @p61='?' (DbType = Int32), @p62='?' (DbType = Boolean), @p63='?', @p64='?' (DbType = Int32), @p115='?' (DbType = Guid), @p67='?' (DbType = Int32), @p68='?' (DbType = Int32), @p69='?' (DbType = Int32), @p70='?' (DbType = Boolean), @p71='?' (DbType = Boolean), @p72='?' (DbType = Boolean), @p73='?' (DbType = Boolean), @p74='?', @p75='?' (DbType = Boolean), @p76='?' (DbType = Boolean), @p77='?' (DbType = Int32), @p78='?' (DbType = Boolean), @p79='?', @p80='?', @p81='?', @p82='?', @p83='?', @p84='?', @p116='?', @p85='?' (DbType = Int32), @p86='?' (DbType = DateTime), @p87='?' (DbType = Guid), @p88='?' (DbType = Guid), @p89='?' (DbType = DateTime), @p90='?', @p91='?' (DbType = Int32), @p92='?' (DbType = Boolean), @p93='?' (DbType = Boolean), @p94='?', @p95='?' (DbType = Boolean), @p96='?', @p97='?' (DbType = Int32), @p98='?' (DbType = Boolean), @p99='?' (DbType = Boolean), @p100='?' (DbType = DateTime), @p101='?' (DbType = Guid), @p102='?', @p103='?', @p104='?', @p105='?' (DbType = Int32), @p106='?' (DbType = Int32), @p107='?' (DbType = Boolean), @p108='?' (DbType = Boolean), @p109='?' (DbType = Boolean), @p110='?' (DbType = Boolean), @p111='?' (DbType = Int32), @p112='?' (DbType = Boolean), @p113='?', @p114='?' (DbType = Int32)], CommandType='Text', CommandTimeout='30']
UPDATE "IdentityServerApiResources" SET "AllowedAccessTokenSigningAlgorithms" = @p0, "ConcurrencyStamp" = @p1, "CreationTime" = @p2, "CreatorId" = @p3, "DeleterId" = @p4, "DeletionTime" = @p5, "Description" = @p6, "DisplayName" = @p7, "Enabled" = @p8, "ExtraProperties" = @p9, "IsDeleted" = @p10, "LastModificationTime" = @p11, "LastModifierId" = @p12, "Name" = @p13, "ShowInDiscoveryDocument" = @p14
WHERE "Id" = @p15 AND "ConcurrencyStamp" = @p16;
UPDATE "IdentityServerClients" SET "AbsoluteRefreshTokenLifetime" = @p17, "AccessTokenLifetime" = @p18, "AccessTokenType" = @p19, "AllowAccessTokensViaBrowser" = @p20, "AllowOfflineAccess" = @p21, "AllowPlainTextPkce" = @p22, "AllowRememberConsent" = @p23, "AllowedIdentityTokenSigningAlgorithms" = @p24, "AlwaysIncludeUserClaimsInIdToken" = @p25, "AlwaysSendClientClaims" = @p26, "AuthorizationCodeLifetime" = @p27, "BackChannelLogoutSessionRequired" = @p28, "BackChannelLogoutUri" = @p29, "ClientClaimsPrefix" = @p30, "ClientId" = @p31, "ClientName" = @p32, "ClientUri" = @p33, "ConcurrencyStamp" = @p34, "ConsentLifetime" = @p35, "CreationTime" = @p36, "CreatorId" = @p37, "DeleterId" = @p38, "DeletionTime" = @p39, "Description" = @p40, "DeviceCodeLifetime" = @p41, "EnableLocalLogin" = @p42, "Enabled" = @p43, "ExtraProperties" = @p44, "FrontChannelLogoutSessionRequired" = @p45, "FrontChannelLogoutUri" = @p46, "IdentityTokenLifetime" = @p47, "IncludeJwtId" = @p48, "IsDeleted" = @p49, "LastModificationTime" = @p50, "LastModifierId" = @p51, "LogoUri" = @p52, "PairWiseSubjectSalt" = @p53, "ProtocolType" = @p54, "RefreshTokenExpiration" = @p55, "RefreshTokenUsage" = @p56, "RequireClientSecret" = @p57, "RequireConsent" = @p58, "RequirePkce" = @p59, "RequireRequestObject" = @p60, "SlidingRefreshTokenLifetime" = @p61, "UpdateAccessTokenClaimsOnRefresh" = @p62, "UserCodeType" = @p63, "UserSsoLifetime" = @p64
WHERE "Id" = @p65 AND "ConcurrencyStamp" = @p66;
UPDATE "IdentityServerClients" SET "AbsoluteRefreshTokenLifetime" = @p67, "AccessTokenLifetime" = @p68, "AccessTokenType" = @p69, "AllowAccessTokensViaBrowser" = @p70, "AllowOfflineAccess" = @p71, "AllowPlainTextPkce" = @p72, "AllowRememberConsent" = @p73, "AllowedIdentityTokenSigningAlgorithms" = @p74, "AlwaysIncludeUserClaimsInIdToken" = @p75, "AlwaysSendClientClaims" = @p76, "AuthorizationCodeLifetime" = @p77, "BackChannelLogoutSessionRequired" = @p78, "BackChannelLogoutUri" = @p79, "ClientClaimsPrefix" = @p80, "ClientId" = @p81, "ClientName" = @p82, "ClientUri" = @p83, "ConcurrencyStamp" = @p84, "ConsentLifetime" = @p85, "CreationTime" = @p86, "CreatorId" = @p87, "DeleterId" = @p88, "DeletionTime" = @p89, "Description" = @p90, "DeviceCodeLifetime" = @p91, "EnableLocalLogin" = @p92, "Enabled" = @p93, "ExtraProperties" = @p94, "FrontChannelLogoutSessionRequired" = @p95, "FrontChannelLogoutUri" = @p96, "IdentityTokenLifetime" = @p97, "IncludeJwtId" = @p98, "IsDeleted" = @p99, "LastModificationTime" = @p100, "LastModifierId" = @p101, "LogoUri" = @p102, "PairWiseSubjectSalt" = @p103, "ProtocolType" = @p104, "RefreshTokenExpiration" = @p105, "RefreshTokenUsage" = @p106, "RequireClientSecret" = @p107, "RequireConsent" = @p108, "RequirePkce" = @p109, "RequireRequestObject" = @p110, "SlidingRefreshTokenLifetime" = @p111, "UpdateAccessTokenClaimsOnRefresh" = @p112, "UserCodeType" = @p113, "UserSsoLifetime" = @p114
WHERE "Id" = @p115 AND "ConcurrencyStamp" = @p116;
[17:34:08 ERR] An exception occurred in the database while saving changes for context type 'Volo.Abp.IdentityServer.EntityFrameworkCore.IdentityServerDbContext'.
Microsoft.EntityFrameworkCore.DbUpdateException: An error occurred while saving the entity changes. See the inner exception for details.
 ---> System.InvalidCastException: Cannot write DateTime with Kind=Local to PostgreSQL type 'timestamp with time zone', only UTC is supported. Note that it's not possible to mix DateTimes with different Kinds in an array/range. See the Npgsql.EnableLegacyTimestampBehavior AppContext switch to enable legacy behavior.
   at Npgsql.Internal.TypeHandlers.DateTimeHandlers.TimestampTzHandler.ValidateAndGetLength(DateTime value, NpgsqlParameter parameter)
   at Npgsql.Internal.TypeHandlers.DateTimeHandlers.TimestampTzHandler.ValidateObjectAndGetLength(Object value, NpgsqlLengthCache& lengthCache, NpgsqlParameter parameter)
   at Npgsql.NpgsqlParameter.ValidateAndGetLength()
   at Npgsql.NpgsqlParameterCollection.ValidateAndBind(ConnectorTypeMapper typeMapper)
   at Npgsql.NpgsqlCommand.ExecuteReader(CommandBehavior behavior, Boolean async, CancellationToken cancellationToken)
   at Npgsql.NpgsqlCommand.ExecuteReader(CommandBehavior behavior, Boolean async, CancellationToken cancellationToken)
   at Npgsql.NpgsqlCommand.ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReaderAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReaderAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.Update.ReaderModificationCommandBatch.ExecuteAsync(IRelationalConnection connection, CancellationToken cancellationToken)
   --- End of inner exception stack trace ---
   at Microsoft.EntityFrameworkCore.Update.ReaderModificationCommandBatch.ExecuteAsync(IRelationalConnection connection, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.Update.Internal.BatchExecutor.ExecuteAsync(IEnumerable`1 commandBatches, IRelationalConnection connection, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.Update.Internal.BatchExecutor.ExecuteAsync(IEnumerable`1 commandBatches, IRelationalConnection connection, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.Update.Internal.BatchExecutor.ExecuteAsync(IEnumerable`1 commandBatches, IRelationalConnection connection, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.StateManager.SaveChangesAsync(IList`1 entriesToSave, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.StateManager.SaveChangesAsync(StateManager stateManager, Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken)
   at Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.NpgsqlExecutionStrategy.ExecuteAsync[TState,TResult](TState state, Func`4 operation, Func`4 verifySucceeded, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync(Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken)
Microsoft.EntityFrameworkCore.DbUpdateException: An error occurred while saving the entity changes. See the inner exception for details.
 ---> System.InvalidCastException: Cannot write DateTime with Kind=Local to PostgreSQL type 'timestamp with time zone', only UTC is supported. Note that it's not possible to mix DateTimes with different Kinds in an array/range. See the Npgsql.EnableLegacyTimestampBehavior AppContext switch to enable legacy behavior.
   at Npgsql.Internal.TypeHandlers.DateTimeHandlers.TimestampTzHandler.ValidateAndGetLength(DateTime value, NpgsqlParameter parameter)
   at Npgsql.Internal.TypeHandlers.DateTimeHandlers.TimestampTzHandler.ValidateObjectAndGetLength(Object value, NpgsqlLengthCache& lengthCache, NpgsqlParameter parameter)
   at Npgsql.NpgsqlParameter.ValidateAndGetLength()
   at Npgsql.NpgsqlParameterCollection.ValidateAndBind(ConnectorTypeMapper typeMapper)
   at Npgsql.NpgsqlCommand.ExecuteReader(CommandBehavior behavior, Boolean async, CancellationToken cancellationToken)
   at Npgsql.NpgsqlCommand.ExecuteReader(CommandBehavior behavior, Boolean async, CancellationToken cancellationToken)
   at Npgsql.NpgsqlCommand.ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReaderAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReaderAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.Update.ReaderModificationCommandBatch.ExecuteAsync(IRelationalConnection connection, CancellationToken cancellationToken)


可在Program类上设置应用上下文开关
~~~C#

class Program
{
    static async Task Main(string[] args)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
~~~

https://github.com/abpframework/abp/issues/11437

#### 参与贡献

1. Fork 本仓库
2. 新建 Feat_xxx 分支
3. 提交代码
4. 新建 Pull Request