using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Client.Models;
using Client.Models.CompanyViewModel;
using Shared.Models;

namespace CarClient.Controllers
{
	public class CompanyController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		public CompanyController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}


		// GET: Company

		public async Task<IActionResult> Index()
		{
			var getCompaniesResponse = await Client.Utils.Utils.GetCompaniesResponseAsync();
			var companies = getCompaniesResponse.Companies;

			foreach (var company in companies)
			{
				var getCarsResponse = await Client.Utils.Utils.GetCarsResponseAsync();
				var cars = getCarsResponse.Cars;
				cars = cars.Where(c => c.CompanyId == company.Id).ToList();
				company.Cars = cars;
			}

			var companyViewModel = new CompanyViewModel { Companies = companies };

			return View(companyViewModel);
		}

		// GET: Company/Details/5
		public async Task<IActionResult> Details(Guid id)
		{
			var getCompanyResponse = await Client.Utils.Utils.GetCompanyResponseAsync(id);
			var company = getCompanyResponse.Company;

			return View(company);
		}

		// GET: Company/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Company/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Name,Address,CreationTime")] Company company)
		{
			if (!ModelState.IsValid) return View(company);
			company.Id = Guid.NewGuid();
			var createCompanyResponse = await Client.Utils.Utils.CreateCompanyResponseAsync(company);

			return RedirectToAction(nameof(Index));
		}

		// GET: Company/Edit/5
		public async Task<IActionResult> Edit(Guid id)
		{
			var getCompanyResponse = await Client.Utils.Utils.GetCompanyResponseAsync(id);
			var company = getCompanyResponse.Company;
			return View(company);
		}

		// POST: Company/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,CreationTime, Name, Address")] Company company)
		{
			if (!ModelState.IsValid) return View(company);
			var getCompanyResponse = await Client.Utils.Utils.GetCompanyResponseAsync(id);
			var oldCompany = getCompanyResponse.Company;

			oldCompany.Name = company.Name;
			oldCompany.Address = company.Address;
			var updateCompanyResponse = await Client.Utils.Utils.UpdateCompanyResponseAsync(company);

			return RedirectToAction(nameof(Index));
		}

		// GET: Company/Delete/5
		public async Task<IActionResult> Delete(Guid id)
		{
			var getCompanyResponse = await Client.Utils.Utils.GetCompanyResponseAsync(id);
			var company = getCompanyResponse.Company;
			return View(company);
		}

		// POST: Company/Delete/5
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var deleteCompanyResponse = await Client.Utils.Utils.DeleteCompanyResponseAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}