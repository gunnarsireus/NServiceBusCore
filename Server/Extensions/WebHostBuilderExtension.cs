using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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

		public static IWebHostBuilder InitSqLiteDb(this IWebHostBuilder hostBuilder, string db)
		{
			_optionsBuilder.UseSqlite(db);
			using (var context = new CarApiContext(_optionsBuilder.Options))
			{
				context.Database.EnsureCreated();
				context.EnsureSeedData();
			}
			return hostBuilder;
		}
	}
}
