# AbpvNext

#### 介绍
Abp vNext框架的基础封装

#### 软件架构

##### 1.AbpvNext
 基础模块，依赖只Abp的Autofac模块，可用于控制台，WPF等项目

##### 2.AbpvNext.Domain.Shared
  领域共享模块
 1. 集成Abp Web模板的所有依赖
 2. 是否启用多租户常量（MultiTenancyConsts.IsEnabled）改为静态变量（项目启动前配置，可放到Program类的最前面做配置）

##### 3.AbpvNext.Domain.Shared
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

	<ItemGroup>
		<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="5.0.*">
			<IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
			<PrivateAssets>compile; contentFiles; build; buildMultitargeting; buildTransitive; analyzers; native</PrivateAssets>
		</PackageReference>
	</ItemGroup>

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
2. 使用EFCore则项目名必须是以EntityFrameworkCore结尾


#### 参与贡献

1. Fork 本仓库
2. 新建 Feat_xxx 分支
3. 提交代码
4. 新建 Pull Request