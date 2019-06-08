namespace WebApp
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	using System;
    using WebApp.Controllers;
    using WebApp.AppConfiguration;
    using Configuration;

    public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			var appConfigurator = new AppConfigurator(Configuration);
			return appConfigurator.ConfigureServices(services);
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

			AppConfigurator.InitDbContext(Configuration);
		}
	}
}
