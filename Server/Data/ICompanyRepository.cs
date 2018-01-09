using Shared.Models;


namespace Server.Data
{
    public interface ICompanyRepository:IRepository<Company>
    {
	    //IEnumerable<Car> GetBankAcocuntsWithBalanceGe(decimal balance, int pageIndex, int pageSize=10);
	}
}
