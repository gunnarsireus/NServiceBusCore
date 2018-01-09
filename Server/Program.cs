using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Server
{
	using System.Globalization;
	using Microsoft.AspNetCore;
	using Microsoft.AspNetCore.Hosting;
	using server;

	class Program
    {
	    public static void Main(string[] args)
	    {
		    CultureInfo.CurrentUICulture = new CultureInfo("en-US");
			BuildWebHost(args).RunAsync().GetAwaiter().GetResult();
	    }

	    public static IWebHost BuildWebHost(string[] args) =>
		    WebHost.CreateDefaultBuilder(args)
			    .UseStartup<Startup>()
			    .Build();
	}
}
