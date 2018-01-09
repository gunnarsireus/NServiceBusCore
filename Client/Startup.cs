using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NServiceBus;
using Client.Data;
using Client.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Shared.Models;
using Shared.Response;

namespace Client
{
	using System;
	using System.IO;
	using System.Threading.Tasks;
	using Microsoft.EntityFrameworkCore;


	public class Startup
    {
	    public Startup(IHostingEnvironment env)
	    {
			var builder = new ConfigurationBuilder()
			    .SetBasePath(env.ContentRootPath)
			    .AddEnvironmentVariables();
		    Configuration = builder.Build();
	    }

	    IConfigurationRoot Configuration { get; }
	    public static IEndpointInstance EndpointInstance { get; set; }


		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
        {
	        var endpointConfiguration = new EndpointConfiguration("Samples.Mvc.WebApplication");
	        var transport = endpointConfiguration.UseTransport<LearningTransport>();
	        endpointConfiguration.UsePersistence<LearningPersistence>();
	        endpointConfiguration.MakeInstanceUniquelyAddressable("1");
			endpointConfiguration.EnableCallbacks();

			EndpointInstance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

			//services.AddSingleton<IMessageSession>(endpointInstance);
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

	    public async Task ConfigureServicesAsync(IServiceCollection services)
	    {
		    string aspNetDb = null;
		    var aspNetDbLocation = new AspNetDbLocation();
		    try
		    {
			    var getAspNetDb = await aspNetDbLocation.GetAspNetDbAsync();
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