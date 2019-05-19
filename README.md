# Readme

How to setup for Asp.Net Core:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc();
    
    // other code
    
    Configger.Configure(services, Configuration, Configuration.GetConnectionString("DefaultConnection"), DbConfigurationOptions.Sql);
}
```

Scheme of application modules:

![module scheme](./Images/Scheme.png)
