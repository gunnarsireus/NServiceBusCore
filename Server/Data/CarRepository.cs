using Shared.Models;
using Server.DAL;
using Microsoft.EntityFrameworkCore;

namespace Server.Data
{
	public class CarRepository : Repository<Car>, ICarRepository
	{
		public CarRepository(DbContext context) : base(context)
		{
		}

		public CarApiContext CarApiContext => Context as CarApiContext;

	}
}
