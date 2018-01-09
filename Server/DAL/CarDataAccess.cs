using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Server.DAL
{

	public class CarDataAccess
	{
		readonly DbContextOptionsBuilder<CarApiContext> _optionsBuilder =
            new DbContextOptionsBuilder<CarApiContext>();

		DbContextOptionsBuilder<CarApiContext> OptionsBuilder => _optionsBuilder;

		public CarDataAccess()
        {
            _optionsBuilder.UseSqlite("DataSource=App_Data/Car.db");
        }

	    public ICollection<Car> GetCars()
	    {
		    using (var context = new CarApiContext(OptionsBuilder.Options))
		    {
			    return context.Cars.ToList();
		    }
	    }

	    public Car GetCar(Guid id)
	    {
		    using (var context = new CarApiContext(OptionsBuilder.Options))
		    {
			    return context.Cars.SingleOrDefault(o => o.Id == id);
		    }
	    }

	    public void AddCar(Car car)
	    {
		    using (var context = new CarApiContext(OptionsBuilder.Options))
		    {
			    context.Cars.Add(car);
			    context.SaveChanges();
		    }
	    }

	    public void DeleteCar(Guid id)
	    {
		    using (var context = new CarApiContext(OptionsBuilder.Options))
		    {
			    var Car = GetCar(id);
			    context.Cars.Remove(Car);
			    context.SaveChanges();
		    }
	    }

		public void UpdateCar(Car car)
		{
			using (var context = new CarApiContext(OptionsBuilder.Options))
			{
				context.Cars.Update(car);
				context.SaveChanges();
			}
		}

		public ICollection<Company> GetCompanies()
        {
            using (var context = new CarApiContext(OptionsBuilder.Options))
            {
                return context.Companies.ToList();
            }
        }

        public Company GetCompany(Guid id)
        {
            using (var context = new CarApiContext(OptionsBuilder.Options))
            {
                return context.Companies.SingleOrDefault(o => o.Id == id);
            }
        }

        public void AddCompany(Company company)
        {
            using (var context = new CarApiContext(OptionsBuilder.Options))
            {
                context.Companies.Add(company);
                context.SaveChanges();
            }
        }

        public void DeleteCompany(Guid id)
        {
            using (var context = new CarApiContext(OptionsBuilder.Options))
            {
                var company = GetCompany(id);
                context.Companies.Remove(company);
                context.SaveChanges();
            }
        }

		public void UpdateCompany(Company company)
		{
			using (var context = new CarApiContext(OptionsBuilder.Options))
			{
				context.Companies.Update(company);
				context.SaveChanges();
			}
		}
	}
}