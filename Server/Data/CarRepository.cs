using Shared.Models;
using Server.DAL;
namespace Server.Data
{
	using Microsoft.EntityFrameworkCore;

	public class CarRepository : Repository<Car>, ICarRepository
	{
		public CarRepository(DbContext context) : base(context)
		{
		}

		public CarApiContext CarApiContext => Context as CarApiContext;

	}
}
