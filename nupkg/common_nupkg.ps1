# Paths
$packFolder = (Get-Item -Path "./" -Verbose).FullName
$rootFolder = Join-Path $packFolder "../"

# List of solutions
$solutions = (
	"templates"
)

# List of projects
$projects = (

    # templates
    "templates/src/AbpvNext",
    "templates/src/AbpvNext.Application",
    "templates/src/AbpvNext.Application.Contracts",
    "templates/src/AbpvNext.Domain",
    "templates/src/AbpvNext.Domain.Shared",
    "templates/src/AbpvNext.EntityFrameworkCore.MySQL",
    "templates/src/AbpvNext.EntityFrameworkCore.Oracle",
    "templates/src/AbpvNext.EntityFrameworkCore.PostgreSql",
    "templates/src/AbpvNext.EntityFrameworkCore.Sqlite",
    "templates/src/AbpvNext.EntityFrameworkCore.SqlServer",
    "templates/src/AbpvNext.HttpApi",
    "templates/src/AbpvNext.HttpApi.Client",
    "templates/src/AbpvNext.HttpApi.WebCore",
    "templates/src/AbpvNext.IdentityServer",
    "templates/src/AbpvNext.MongoDB"
)
