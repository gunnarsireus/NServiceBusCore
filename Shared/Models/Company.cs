using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Shared.Models
{
	using System.Collections.Generic;

	public class Company
	{
		public Company()
		{
			CreationTime = DateTime.Now.ToString(new CultureInfo("se-SE"));
		}
		public Company(Guid id):this()
		{
			Id = id;
		}
		public Guid Id { get; set; }

		[Display(Name = "Skapat datum")]
		public string CreationTime { get; set; }
		[Display(Name = "Namn")]
		public string Name { get; set; }

		[Display(Name = "Adress")]
		public string Address { get; set; }

		public ICollection<Car> Cars { get; set; }
	}
}
