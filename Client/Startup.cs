using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NServiceBus;
using Client.DAL;
using Client.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Client.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Client
{
	public class Startup
	{
		IEndpointInstance EndpointInstance { get; set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var endpointConfiguration = new EndpointConfiguration("NServiceBusCore.Client");
			var transport = endpointConfiguration.UseTransport<LearningTransport>();
			endpointConfiguration.UsePersistence<LearningPersistence>();
			endpointConfiguration.MakeInstanceUniquelyAddressable("1");
			endpointConfiguration.EnableCallbacks();

			EndpointInstance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

			services.AddSingleton(EndpointInstance);

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			//// Add application services.
			services.AddTransient<IEmailSender, EmailSender>();

			services.AddMvc();

			var task = ConfigureServicesAsync(services);

			task.Wait();

		}

		async Task ConfigureServicesAsync(IServiceCollection services)
		{
			string aspNetDb = null;
			var aspNetDbLocation = new AspNetDbLocation();
			try
			{
				var getAspNetDb = await aspNetDbLocation.GetAspNetDbAsync(EndpointInstance);
				aspNetDb = getAspNetDb.AspNetDb;
			}
			catch (Exception e)
			{
				//Do nothing
			}
			if (aspNetDb != null)
			{
				services.AddDbContext<ApplicationDbContext>(options =>
					options.UseSqlite("Data Source=" + aspNetDb));
			}
			else
			{
				services.AddDbContext<ApplicationDbContext>(options =>
					options.UseSqlite("Data Source=" + Directory.GetCurrentDirectory() + "\\App_Data\\AspNet.db"));
			}
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddDebug();
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					"default",
					"{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}