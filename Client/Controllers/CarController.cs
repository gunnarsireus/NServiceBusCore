using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Client.Models;
using NServiceBus;
using Client.Models.CarViewModel;
using Microsoft.AspNetCore.Cors;
using Shared.Models;

namespace Client.Controllers
{

	public class CarController : Controller
	{
		readonly UserManager<ApplicationUser> _userManager;
		readonly IEndpointInstance _endpoint;

		public CarController(UserManager<ApplicationUser> userManager, IEndpointInstance endpoint)
		{
			_userManager = userManager;
			_endpoint = endpoint;
		}

		[HttpGet]
		[EnableCors("AllowAllOrigins")]
		public async Task<IActionResult> GetAllCars()
		{
			var getCarsResponse = await Utils.Utils.GetCarsResponseAsync();
			return Json(getCarsResponse.Cars);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateOnline([FromBody] Car car)
		{
			if (!ModelState.IsValid) return Json(new { success = false });
			var getCarResponse = await Utils.Utils.GetCarResponseAsync(car.Id);
			var oldCar = getCarResponse.Car;
			oldCar.Online = car.Online;
			await Utils.Utils.UpdateCarResponseAsync(oldCar);
			return Json(new { success = true });
		}

		// GET: Car
		public async Task<IActionResult> Index(string id)
		{

			var getCarsResponse = await Utils.Utils.GetCarsResponseAsync();

			var getCompaniesResponse = await Utils.Utils.GetCompaniesResponseAsync();

			if (getCompaniesResponse.Companies.Any() && id == null)
				id = getCompaniesResponse.Companies[0].Id.ToString();

			getCarsResponse.Cars[0].CompanyId = getCompaniesResponse.Companies[0].Id;

			var selectList = new List<SelectListItem>
			{
				new SelectListItem
				{
					Text = "V�lj f�retag",
					Value = ""
				}
			};

			selectList.AddRange(getCompaniesResponse.Companies.Select(company => new SelectListItem
			{
				Text = company.Name,
				Value = company.Id.ToString(),
				Selected = company.Id.ToString() == id
			}));

			var companyId = Guid.NewGuid();
			if (id != null)
			{
				companyId = new Guid(id);
				getCarsResponse.Cars = getCarsResponse.Cars.Where(o => o.CompanyId == companyId).ToList();
			}

			var carListViewModel = new CarListViewModel(companyId)
			{
				CompanySelectList = selectList,
				Cars = getCarsResponse.Cars
			};

			ViewBag.CompanyId = id;
			return View(carListViewModel);
		}

		// GET: Car/Details/5
		public async Task<IActionResult> Details(Guid id)
		{
			var getCarResponse = await Utils.Utils.GetCarResponseAsync(id);
			var getCompanyResponse = await Utils.Utils.GetCompanyResponseAsync(getCarResponse.Car.CompanyId);
			ViewBag.CompanyName = getCompanyResponse.Company.Name;
			return View(getCarResponse.Car);
		}

		// GET: Car/Create
		public async Task<IActionResult> Create(string id)
		{
			var companyId = new Guid(id);
			var car = new Car(companyId);
			var getCompanyResponse = await Utils.Utils.GetCompanyResponseAsync(companyId);
			ViewBag.CompanyName = getCompanyResponse.Company.Name;
			return View(car);
		}

		// POST: Car/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(
			[Bind("CompanyId,VIN,RegNr,Online")] Car car)
		{
			if (!ModelState.IsValid) return View(car);
			car.Id = Guid.NewGuid();
			var createCarResponse = await Utils.Utils.CreateCarResponseAsync(car);

			return RedirectToAction("Index", new { id = car.CompanyId });
		}

		// GET: Car/Edit/5
		public async Task<IActionResult> Edit(Guid id)
		{
			var getCarResponse = await Utils.Utils.GetCarResponseAsync(id);
			getCarResponse.Car.Disabled = true; //Prevent updates of Online/Offline while editing
			var updateCarResponse = Utils.Utils.UpdateCarResponseAsync(getCarResponse.Car);
			var getCompanyResponse = await Utils.Utils.GetCompanyResponseAsync(getCarResponse.Car.CompanyId);
			ViewBag.CompanyName = getCompanyResponse.Company.Name;
			return View(getCarResponse.Car);
		}

		// POST: Car/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id, Online")] Car car)
		{
			if (!ModelState.IsValid) return View(car);
			var oldCarResponse = await Utils.Utils.GetCarResponseAsync(id);
			var oldCar = oldCarResponse.Car;
			oldCar.Online = car.Online;
			oldCar.Disabled = false; //Enable updates of Online/Offline when editing done
			var updateCarResponse = Utils.Utils.UpdateCarResponseAsync(oldCar);

			return RedirectToAction("Index", new { id = oldCar.CompanyId });
		}

		// GET: Car/Delete/5
		public async Task<IActionResult> Delete(Guid id)
		{
			var getCarResponse = await Utils.Utils.GetCarResponseAsync(id);
			return View(getCarResponse.Car);
		}

		// POST: Car/Delete/5
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var getCarResponse = await Utils.Utils.GetCarResponseAsync(id);
			await Utils.Utils.DeleteCarResponseAsync(id);
			return RedirectToAction("Index", new { id = getCarResponse.Car.CompanyId });
		}

		public async Task<bool> RegNrAvailableAsync(string regNr)
		{
			var getCarsResponse = await Utils.Utils.GetCarsResponseAsync();
			return getCarsResponse.Cars.All(c => c.RegNr != regNr);
		}

		public async Task<bool> VinAvailableAsync(string vin)
		{
			var getCarsResponse = await Utils.Utils.GetCarsResponseAsync();
			return getCarsResponse.Cars.All(c => c.VIN != vin);
		}
	}
}