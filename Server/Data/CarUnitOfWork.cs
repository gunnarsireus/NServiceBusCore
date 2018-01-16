using Server.Data;
using Server.DAL;

namespace Server.Data
{
	public class CarUnitOfWork:ICarUnitOfWork
    {
	    readonly CarApiContext _context;

	    public CarUnitOfWork(CarApiContext context)
	    {
		    _context = context;
		    Cars = new CarRepository(_context);
		    Companies = new CompanyRepository(_context);
		}

	    public void Dispose()
	    {
		   _context.Dispose();
	    }

	    public ICarRepository Cars { get; private set; }
	    public ICompanyRepository Companies { get; private set; }
		public int Complete()
		{
			return _context.SaveChanges();
		}
    }
}
