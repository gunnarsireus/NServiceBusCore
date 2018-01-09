using Microsoft.AspNetCore.Hosting;

namespace Client
{
	using System.Globalization;
	using System.IO;

	public class Program
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