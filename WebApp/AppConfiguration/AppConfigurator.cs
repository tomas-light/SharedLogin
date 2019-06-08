namespace WebApp.AppConfiguration
{
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;

	using Configuration;
	using Configuration.Db;
	using Core.Services.Accounts;
	using Core.Services.Histories;
	using Infrastructure.DbContexts;
	using WebApp.AppConfiguration.DbContext;
	using WebApp.AppConfiguration.Dependencies;
	using WebApp.Data;

	public class AppConfigurator
	{
		private IConfiguration configuration;

		public AppConfigurator(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			var dbContextConfigurationStrategy = this.ChooseStrategy();
			dbContextConfigurationStrategy.AddDbContext<ApplicationDbContext>(services);

			var dbConfiguration = this.GetDbConfiguration();

			this.AddIdentity(services);
			this.AddAuthorization(services);

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			var serviceProvider = this.RegisterDependencies(services, dbConfiguration);
			return serviceProvider;
		}

		private IDbContextConfigurationStrategy ChooseStrategy()
		{
			var dbContextConfigurationStrategyFactory = new DbContextConfigurationStrategyFactory(this.configuration);
			return dbContextConfigurationStrategyFactory.Make(DbConfigurationOptions.Sql);
		}

		public IDbConfiguration GetDbConfiguration()
		{
			var dbContextConfigurationStrategy = this.ChooseStrategy();
			return dbContextConfigurationStrategy.GetConfiguration();
		}

		public static void InitDbContext(IConfiguration configuration)
		{
			var appConfigurator = new AppConfigurator(configuration);
			var dbConfiguration = appConfigurator.GetDbConfiguration();
			var context = DbContextConfigurator.CreateDbContext(dbConfiguration, DbConfigurationOptions.Sql);
			DbContextConfigurator.InitDbContext(context);
		}

		private void AddIdentity(IServiceCollection services)
		{
			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 6;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;

				options.SignIn.RequireConfirmedEmail = false;
				options.SignIn.RequireConfirmedPhoneNumber = false;

				options.User.AllowedUserNameCharacters =
						"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				options.User.RequireUniqueEmail = true;
			});

			services
				.AddIdentity<User, Role>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();
		}

		private void AddAuthorization(IServiceCollection services)
		{
			var jwtBearerConfigurator = new JwtBearerConfigurator();

			services.AddAuthorization()
					.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options => jwtBearerConfigurator.CreateOptions(options));
		}

		private IServiceProvider RegisterDependencies(IServiceCollection services, IDbConfiguration dbConfiguration)
		{
			var containerBuilder = new ContainerBuilder();
			IdentityConfigurator.RegisterDependencies(containerBuilder);

			var repositoryDependenciesModule = DbContextConfigurator.GetDbContextDependencies(dbConfiguration, DbConfigurationOptions.Sql);

			containerBuilder.AddSharedLoginDependecies<ApplicationDbContext, User, Role, string>(
				mapperConfiguration => {
					mapperConfiguration.AddProfile<Mappings.AccountMappingProfile>();
					mapperConfiguration.AddProfile<Mappings.HistoryMappingProfile>();
				},
				repositoryDependenciesModule);

			containerBuilder.Populate(services);
			return new AutofacServiceProvider(containerBuilder.Build());
		}
	}
}
