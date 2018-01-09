using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shared.Models.HomeViewModel
{
	using System;
	using Shared.Models;

	public class HomeViewModel : Car
    {
	    public HomeViewModel(Guid companyId) : base(companyId)
	    {
		    CompanyId = companyId;
	    }
		public List<Company> Companies { get; set; }
	}
}