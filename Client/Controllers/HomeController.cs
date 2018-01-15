using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Client.Models.HomeViewModel;
using Client.Models;
using NServiceBus;
using Shared.Models;

namespace Client.Controllers
{
	public class HomeController : Controller
	{
		readonly IEndpointInstance _endpointInstance;
		public HomeController(IEndpointInstance endPointEndpointInstance)
		{
			_endpointInstance = endPointEndpointInstance;
		}

		public async Task<IActionResult> Index()
		{
			List<Company> companies;
			try
			{
				var responseTask = await Utils.Utils.GetCompaniesResponseAsync(_endpointInstance);
				companies = responseTask.Companies;
			}
			catch (Exception e)
			{
				TempData["CustomError"] = "Ingen kontakt med servern! CarAPI måste startas innan Client kan köras!";
				return View("Index", new HomeViewModel(Guid.NewGuid()) { Companies = new List<Company>() });
			}

			var getCarsResponse = await Utils.Utils.GetCarsResponseAsync(_endpointInstance);
			var allCars = getCarsResponse.Cars.ToList();
			foreach (var car in allCars)
			{
				car.Disabled = false; //Enable updates of Online/Offline
				var updateCarResponse = Utils.Utils.UpdateCarResponseAsync(car, _endpointInstance);
			}

			foreach (var company in companies)
			{
				var companyCars = allCars.Where(o => o.CompanyId == company.Id).ToList();
				company.Cars = companyCars;
			}
			var homeViewModel = new HomeViewModel(Guid.NewGuid()) { Companies = companies };
			return View("Index", homeViewModel);
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}