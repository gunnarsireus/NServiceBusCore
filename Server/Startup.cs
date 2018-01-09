using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NServiceBus;
using System;

namespace Server
{

	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			Console.Title = "NServiceBusCore.Server";
			var endpointConfiguration = new EndpointConfiguration("NServiceBusCore.Server");
			endpointConfiguration.EnableCallbacks(makesRequests: false);
			endpointConfiguration.UsePersistence<LearningPersistence>();
			endpointConfiguration.UseTransport<LearningTransport>();
			var endpointInstance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

			Console.WriteLine("Press any key to exit");
			Console.ReadKey();


		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddDebug();
		}
	}
}
