﻿using Microsoft.AspNetCore.Hosting;
using System.Globalization;
using Microsoft.AspNetCore;
using Server.Extensions;
namespace Server
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			CultureInfo.CurrentUICulture = new CultureInfo("en-US");
			BuildWebHost(args).Run();
		}

		static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseSqLiteDb("DataSource=App_Data/Car.db")
				.Build();
	}

}
