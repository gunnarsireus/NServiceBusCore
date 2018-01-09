using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Server.DAL
{
	public class CarApiContext : DbContext
	{
		public CarApiContext(DbContextOptions options)
			: base(options)
		{
		}

		public DbSet<Car> Cars { get; set; }

		public DbSet<Company> Companies { get; set; }
	}
}