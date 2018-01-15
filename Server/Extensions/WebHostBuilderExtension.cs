using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using NServiceBus;
using Server.DAL;

namespace Server.Extensions
{
	public static class WebHostBuilderExtension
	{
		//
		// Summary:
		//     Defines the context for Car.db
		// Parameters:
		//   db:
		static readonly DbContextOptionsBuilder<CarApiContext> _optionsBuilder = new DbContextOptionsBuilder<CarApiContext>();

		public static IWebHostBuilder UseSqLiteDb(this IWebHostBuilder hostBuilder, string db)
		{
			_optionsBuilder.UseSqlite(db);
			using (var context = new CarApiContext(_optionsBuilder.Options))
			{
				context.Database.EnsureCreated();
				context.EnsureSeedData();
			}

			Console.Title = "NServiceBusCore.Server";
			var endpointConfiguration = new EndpointConfiguration("NServiceBusCore.Server");
			endpointConfiguration.EnableCallbacks(makesRequests: false);
			endpointConfiguration.UsePersistence<LearningPersistence>();
			endpointConfiguration.UseTransport<LearningTransport>();
			var endpointInstance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

			Console.WriteLine("Press any key to exit");
			Console.ReadKey();

			return hostBuilder;
		}
	}
}
