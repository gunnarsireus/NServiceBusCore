using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.EntityFrameworkCore;
using NServiceBus;
using Server.DAL;
using System.Globalization;
using Microsoft.AspNetCore;

namespace Server
{
	class Program
	{
		public static void Main(string[] args)
		{
			CultureInfo.CurrentUICulture = new CultureInfo("en-US");
			BuildWebHost(args).Run();
		}

		static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseDb("DataSource=App_Data/Car.db")
				.Build();
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
