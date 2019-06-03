namespace WebApp
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	using WebApp.Data;
	using System;
	using Infrastructure.DbContexts.Sql;
    using WebApp.Controllers;
    using Configuration;
    using WebApp.AppConfiguration;
    using Autofac.Extensions.DependencyInjection;
    using WebApp.Mappings;
    using Autofac;
    using AutoMapper;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication.JwtBearer;

    public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				options.CheckConsentNeeded = context => true;
				//options.MinimumSameSitePolicy = SameSiteMode.None;
			});

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

			//services.ConfigureApplicationCookie(options =>
			//{
			//	options.ExpireTimeSpan = TimeSpan.FromHours(Configuration.GetValue<double>("CookieLifetime"));
			//	options.SlidingExpiration = true;
			//});

			//services.Configure<SecurityStampValidatorOptions>(opt => { opt.ValidationInterval = TimeSpan.Zero; });

			this.AddSqlContext(services);

			services
				.AddIdentity<User, Role>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			var jwtBearerConfigurator = new JwtBearerConfigurator();

			services.AddAuthorization()
					.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options => jwtBearerConfigurator.CreateOptions(options));

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			var configurator = new Configurator();
			var builder = configurator.ConfigureDependencies();
			
			var dbConfiguration = new SqlDbConfiguration
			{
				Database = Configuration.GetValue<string>("ConnectionStrings:Sql:Database"),
				Server = Configuration.GetValue<string>("ConnectionStrings:Sql:Server"),
				IsMultipleActiveResultSets = Configuration.GetValue<bool>("ConnectionStrings:Sql:MultipleActiveResultSets"),
				IsTrastedConnection = Configuration.GetValue<bool>("ConnectionStrings:Sql:Trusted_Connection"),
			};

			builder.AddSharedLoginServices<ApplicationDbContext, User, Role, string>(
				mc => {
					mc.AddProfile<AccountMappingProfile>();
					mc.AddProfile<HistoryMappingProfile>();
				},
				dbConfiguration,
				DbConfigurationOptions.Sql);

			//var dbConfiguration = new PostgreSqlDbConfiguration
			//{
			//	UserId = Configuration.GetValue<string>("ConnectionStrings:PostgreSql:User ID"),
			//	Password = Configuration.GetValue<string>("ConnectionStrings:PostgreSql:Password"),
			//	Host = Configuration.GetValue<string>("ConnectionStrings:PostgreSql:Host"),
			//	Port = Configuration.GetValue<string>("ConnectionStrings:PostgreSql:Port"),
			//	Database = Configuration.GetValue<string>("ConnectionStrings:PostgreSql:Database"),
			//};
			//return services.AddSharedLogin<ApplicationDbContext, IdentityUser, IdentityRole, string>(dbConfiguration, DbConfigurationOptions.PostgreSql);

			builder.Populate(services);
			return new AutofacServiceProvider(builder.Build());
		}

		private void AddSqlContext(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(optionBuilder =>
				optionBuilder.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));
		}

		private void AddPostgreSqlContext(IServiceCollection services)
		{
			services
				.AddEntityFrameworkNpgsql()
				.AddDbContext<ApplicationDbContext>(optionBuilder =>
					optionBuilder.UseNpgsql(
						Configuration.GetConnectionString("PostgreSqlConnection")));
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					"default",
					"{controller}/{action}",
					new { Controller = "Home", Action = nameof(HomeController.Index) });
			});

			app.MapWhen(
				context =>
				{
					return !context.Request.Path.Value.ToLower().StartsWith("/api");
				},
				builder =>
				{
					builder.UseMvc(routes =>
					{
						routes.MapSpaFallbackRoute(
							"spa",
							new { Controller = "Home", Action = nameof(HomeController.Index) });
					});
				});
		}
	}
}
