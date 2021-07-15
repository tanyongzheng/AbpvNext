

##### AbpvNext.Domain
  领域模块
  1. 修改模板自带的IdentityServer数据种子生成类（IdentityServerDataSeedContributor）<br/>
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

2. 修改模板自带的数据迁移服务，设置数据迁移文件保存到的项目路径，（初始化及后面Add-Migration命令保存的项目）
设置全局静态变量 AbpvNextDomainStaticData.EFCoreDbMigrationSrcDirectoryPath="samples/WebApiDemo",按实际项目来默认src
  原模板方法
~~~CSharp

        private string GetEntityFrameworkCoreProjectFolderPath()
        {
            var slnDirectoryPath = GetSolutionDirectoryPath();

            if (slnDirectoryPath == null)
            {
                throw new Exception("Solution folder not found!");
            }

            var srcDirectoryPath = Path.Combine(slnDirectoryPath, "src");

            return Directory.GetDirectories(srcDirectoryPath)
                .FirstOrDefault(d => d.EndsWith(".EntityFrameworkCore"));
        }
~~~

  按配置指定迁移项目目录的方法
~~~CSharp

        private string GetEntityFrameworkCoreProjectFolderPath()
        {
            var slnDirectoryPath = GetSolutionDirectoryPath();

            if (slnDirectoryPath == null)
            {
                throw new Exception("Solution folder not found!");
            }

            var srcDirectoryPath = Path.Combine(slnDirectoryPath, AbpvNextDomainStaticData.EFCoreDbMigrationSrcDirectoryPath);

            return Directory.GetDirectories(srcDirectoryPath)
                .FirstOrDefault(d => d.EndsWith(".EntityFrameworkCore"));
        }
~~~