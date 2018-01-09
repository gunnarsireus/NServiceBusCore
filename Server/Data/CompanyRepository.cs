using Shared.Models;
using Server.DAL;

namespace Server.Data
{
	using Microsoft.EntityFrameworkCore;

	public class CompanyRepository : Repository<Company>, ICompanyRepository
	{
		public CompanyRepository(DbContext context) : base(context)
		{
		}

		public CarApiContext CarApiContext => Context as CarApiContext;

	}
}
