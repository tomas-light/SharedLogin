# Readme

How to setup for Asp.Net Core:

```csharp
public IServiceProvider ConfigureServices(IServiceCollection services)
{
    services.AddMvc();
    
    // other code
    
    var jwtBearerConfigurator = new JwtBearerConfigurator();

    services.AddAuthorization()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => jwtBearerConfigurator.CreateOptions(options));
        
    var containerBuilder = new ContainerBuilder();
    
    // register own dependencies
    
    var repositoryDependenciesModule = DbContextConfigurator.GetDbContextDependencies(
        dbConfiguration, 
        DbConfigurationOptions.Sql);
        
    containerBuilder.AddSharedLoginDependecies<ApplicationDbContext, User, Role, string>(
        mapperConfiguration => {
            mapperConfiguration.AddProfile<Mappings.AccountMappingProfile>();
            mapperConfiguration.AddProfile<Mappings.HistoryMappingProfile>();
        },
        repositoryDependenciesModule);
        
    containerBuilder.Populate(services);
    return new AutofacServiceProvider(containerBuilder.Build());
}
```

Scheme of application modules:

![module scheme](./Images/Scheme.png)


## Client interface 
https://www.youtube.com/watch?v=UnpitCKyKf0

Test client interface implementation to display of usage example.
