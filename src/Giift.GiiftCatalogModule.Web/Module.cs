using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Core.Security;
using VirtoCommerce.Platform.Core.Settings;
using Giift.GiiftCatalogModule.Core;
using Giift.GiiftCatalogModule.Data.Repositories;
using Giift.GiiftCatalogModule.Core.Models;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.CatalogModule.Core.Model;
using VirtoCommerce.CatalogModule.Data.Model;
using Giift.GiiftCatalogModule.Data.Models;
using VirtoCommerce.CatalogModule.Data.Repositories;
using VirtoCommerce.Platform.Core.JsonConverters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Giift.GiiftCatalogModule.Web.Controllers;

namespace Giift.GiiftCatalogModule.Web;

public class Module : IModule, IHasConfiguration
{
    public ManifestModuleInfo ModuleInfo { get; set; }
    public IConfiguration Configuration { get; set; }

    public void Initialize(IServiceCollection serviceCollection)
    {    
        AbstractTypeFactory<CategoryLink>.OverrideType<CategoryLink, ExtendedCategoryLink>().MapToType<ExtendedCategoryItemRelationEntity>();
        AbstractTypeFactory<CategoryItemRelationEntity>.OverrideType<CategoryItemRelationEntity, ExtendedCategoryItemRelationEntity>();
        // database initialization
        var configuration = serviceCollection.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("VirtoCommerce.CatalogModule") ?? configuration.GetConnectionString("VirtoCommerce");
        serviceCollection.AddDbContext<GiiftCatalogModuleDbContext>(options => options.UseSqlServer(connectionString));
        serviceCollection.AddTransient<ICatalogRepository, ExtendedCatalogRepositoryImpl>();
    }

    public void PostInitialize(IApplicationBuilder appBuilder)
    {
        var serviceProvider = appBuilder.ApplicationServices;

        // Register settings
        var settingsRegistrar = serviceProvider.GetRequiredService<ISettingsRegistrar>() ;
        settingsRegistrar.RegisterSettings(ModuleConstants.Settings.AllSettings, ModuleInfo.Id);

        // Register permissions
        var permissionsRegistrar = serviceProvider.GetRequiredService<IPermissionsRegistrar>();
        permissionsRegistrar.RegisterPermissions(ModuleConstants.Security.Permissions.AllPermissions
            .Select(x => new Permission { ModuleId = ModuleInfo.Id, GroupName = "CatalogModule", Name = x })
            .ToArray());
        // Apply migrations
        using (var serviceScope = appBuilder.ApplicationServices.CreateScope())
        {
            using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<GiiftCatalogModuleDbContext>())
            {
                dbContext.Database.EnsureCreated();
                dbContext.Database.Migrate();
            }
        }
    }

    public void Uninstall()
    {
        // Nothing to do here
    }
}
