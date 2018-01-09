using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace Shared.Models
{
	public class Car
	{
		public Car()
		{
			Id = Guid.NewGuid();
			CreationTime = DateTime.Now.ToString(new CultureInfo("se-SE"));
			Online = true;
		}

		public Car(Guid companyId)
		{
			Id = Guid.NewGuid();
			CreationTime = DateTime.Now.ToString(new CultureInfo("se-SE"));
			Online = true;
			CompanyId = companyId;
		}
		public Guid Id { get; set; }
		public Guid CompanyId { get; set; }

		[Display(Name = "Skapat datum")]
		public string CreationTime { get; set; }

		[Display(Name = "VIN (VehicleID)")]
		[RegularExpression(@"^[A-Z0-9]{6}\d{11}$", ErrorMessage = "{0} anges som X1Y2Z300001239876")]
		[Remote("VinAvailableAsync", "Car", ErrorMessage = "VIN upptaget")]
		public string VIN { get; set; }

		[Display(Name = "Reg. Nr.")]
		[RegularExpression(@"^[A-Z]{3}\d{3}$", ErrorMessage = "{0} anges som XYZ123")]
		[Remote("RegNrAvailableAsync", "Car", ErrorMessage = "Registreringsnummer upptaget")]
		public string RegNr { get; set; }

		[Display(Name = "Status")]
		public bool Online { get; set; }

		[Display(Name = "Online (X) eller Offline ()?")]
		public string OnlineOrOffline => (this.Online) ? "Online" : "Offline";

		public bool Disabled { get; set; } //Used to block changes of Online/Offline status
	}
}
