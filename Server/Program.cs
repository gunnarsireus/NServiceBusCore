using System.Globalization;
using Microsoft.AspNetCore.Hosting;

namespace Server
{
	using System;
	using System.IO;
	using Microsoft.EntityFrameworkCore;
	using NServiceBus;
	using Server.DAL;

	class Program
	{
		public static void Main()
		{
			CultureInfo.CurrentUICulture = new CultureInfo("en-US");
			var host = new WebHostBuilder()
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseDb("DataSource=App_Data/Car.db")
				.Build();

			host.Run();
		}
	}

	public static class WebHostBuilderExtension
	{
		//
		// Summary:
		//     Defines the context for Car.db
		// Parameters:
		//   db:
		static readonly DbContextOptionsBuilder<CarApiContext> _optionsBuilder = new DbContextOptionsBuilder<CarApiContext>();

		public static IWebHostBuilder UseDb(this IWebHostBuilder hostBuilder, string db)
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
