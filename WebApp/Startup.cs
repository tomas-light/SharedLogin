namespace WebApp
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Identity.UI;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	using Configuration;
	using WebApp.Data;
    using System;
    using Infrastructure.DbContexts.Sql;
    using Infrastructure.DbContexts.PostgreSql;

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
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			this.AddSqlContext(services);

			services.AddDefaultIdentity<IdentityUser>()
				.AddDefaultUI(UIFramework.Bootstrap4)
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			var dbConfiguration = new SqlDbConfiguration
			{
				Database = Configuration.GetValue<string>("ConnectionStrings:Sql:Database"),
				Server = Configuration.GetValue<string>("ConnectionStrings:Sql:Server"),
				IsMultipleActiveResultSets = Configuration.GetValue<bool>("ConnectionStrings:Sql:MultipleActiveResultSets"),
				IsTrastedConnection = Configuration.GetValue<bool>("ConnectionStrings:Sql:Trusted_Connection"),
			};
			return services.AddSharedLogin<ApplicationDbContext, IdentityUser, IdentityRole, string>(dbConfiguration, DbConfigurationOptions.Sql);

			//var dbConfiguration = new PostgreSqlDbConfiguration
			//{
			//	UserId = Configuration.GetValue<string>("ConnectionStrings:PostgreSql:User ID"),
			//	Password = Configuration.GetValue<string>("ConnectionStrings:PostgreSql:Password"),
			//	Host = Configuration.GetValue<string>("ConnectionStrings:PostgreSql:Host"),
			//	Port = Configuration.GetValue<string>("ConnectionStrings:PostgreSql:Port"),
			//	Database = Configuration.GetValue<string>("ConnectionStrings:PostgreSql:Database"),
			//};
			//return services.AddSharedLogin(dbConfiguration, DbConfigurationOptions.PostgreSql);
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
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
