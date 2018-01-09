using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shared.Models.CarViewModel
{
	public class CarListViewModel : Car
    {
		public CarListViewModel(Guid companyId) : base(companyId)
		{
			CompanyId = companyId;
		}
		new Guid CompanyId { get; set; }
		public List<SelectListItem> CompanySelectList { get; set; }
		public List<Car> Cars { get; set; }
	}
}