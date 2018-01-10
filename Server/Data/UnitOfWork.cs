namespace Server.Data
{
	using Server.DAL;


	public class UnitOfWork:IUnitOfWork
    {
	    readonly CarApiContext _context;

	    public UnitOfWork(CarApiContext context)
	    {
		    _context = context;
		    Cars = new CarRepository(_context);
		    Companies = new CompanyRepository(_context);
		}

	    public void Dispose()
	    {
		   Context.Dispose();
	    }

	    public ICarRepository Cars { get; private set; }
	    public ICompanyRepository Companies { get; private set; }

		public CarApiContext Context => Context1;

		public CarApiContext Context1 => _context;

		public int Complete()
		{
			return Context.SaveChanges();
		}
    }
}
