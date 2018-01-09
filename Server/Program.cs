using System.Globalization;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Server
{
	class Program
    {
	    public static void Main(string[] args)
	    {
		    CultureInfo.CurrentUICulture = new CultureInfo("en-US");
			BuildWebHost(args).RunAsync().GetAwaiter().GetResult();
	    }

	    static IWebHost BuildWebHost(string[] args) =>
		    WebHost.CreateDefaultBuilder(args)
			    .UseStartup<Startup>()
			    .Build();
	}
}
