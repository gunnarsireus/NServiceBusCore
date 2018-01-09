using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NServiceBus;
using Server.DAL;
using System;
using Microsoft.EntityFrameworkCore;

namespace server
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			Console.Title = "Samples.Mvc.Server";
			var endpointConfiguration = new EndpointConfiguration("Samples.Mvc.Server");
			endpointConfiguration.EnableCallbacks(makesRequests: false);
			endpointConfiguration.UsePersistence<LearningPersistence>();
			endpointConfiguration.UseTransport<LearningTransport>();
			var endpointInstance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

			//services.AddDbContext<CarApiContext>(options =>
			//	options.UseSqlite("DataSource=App_Data/Car.db"));

			Console.WriteLine("Press any key to exit");
			Console.ReadKey();


		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddDebug();
			//app.UseMvc();
		}
	}
}
