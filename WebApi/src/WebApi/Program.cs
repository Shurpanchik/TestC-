using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace WebApi
{
	public class Program
	{
		public static void Main()
		{
			var contentRoot = Directory.GetCurrentDirectory();

			var config = new ConfigurationBuilder()
				.SetBasePath(contentRoot)
				.AddJsonFile("hosting.json", optional: false)
				.Build();

			var host = new WebHostBuilder()
				.UseContentRoot(contentRoot)
				.UseConfiguration(config)
				.UseKestrel()
				.UseStartup<Startup>()
				.Build();

			host.Run();
		}
	}
}