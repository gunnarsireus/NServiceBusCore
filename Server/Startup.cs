using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NServiceBus;
using System;

namespace Server
{
	using Microsoft.EntityFrameworkCore;
	using Server.DAL;

	public class Startup
	{

		readonly DbContextOptionsBuilder<CarApiContext> _optionsBuilder =
			new DbContextOptionsBuilder<CarApiContext>();

		DbContextOptionsBuilder<CarApiContext> OptionsBuilder => _optionsBuilder;
		public Startup(IConfiguration configuration)
		{
			_optionsBuilder.UseSqlite("DataSource=App_Data/Car.db");
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

			using (var context = new CarApiContext(OptionsBuilder.Options))
			{
				context.Database.EnsureCreated();
				context.EnsureSeedData();
			}

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
