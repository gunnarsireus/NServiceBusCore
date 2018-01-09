using System.Globalization;
using Microsoft.AspNetCore.Hosting;

namespace Server
{
	using System.IO;

	class Program
    {
	    public static void Main()
	    {
		    CultureInfo.CurrentUICulture = new CultureInfo("en-US");
		    var host = new WebHostBuilder()
			    .UseKestrel()
			    .UseContentRoot(Directory.GetCurrentDirectory())
			    .UseIISIntegration()
			    .UseStartup<Startup>()
			    .Build();

		    host.Run();
		}
	}
}
