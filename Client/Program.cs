using Microsoft.AspNetCore.Hosting;
using System.Globalization;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore;

namespace Client
{
	internal class Program
    {
	    public static void Main(string[] args)
	    {
		    CultureInfo.CurrentUICulture = new CultureInfo("en-US");
		    var builder = new ConfigurationBuilder()
			    .SetBasePath(Directory.GetCurrentDirectory())
			    .AddJsonFile("appsettings.json");

		    builder.Build();

		    BuildWebHost(args).Run();
		}

	    static IWebHost BuildWebHost(string[] args)
	    {
		    return WebHost.CreateDefaultBuilder(args)
			    .UseStartup<Startup>()
			    .Build();
	    }
	}
}