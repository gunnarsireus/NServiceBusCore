using System.Collections.Generic;
using System;
using Shared.Models;

namespace Client.Models.HomeViewModel
{
	public class HomeViewModel : Car
    {
	    public HomeViewModel(Guid companyId) : base(companyId)
	    {
		    CompanyId = companyId;
	    }
		public List<Company> Companies { get; set; }
	}
}