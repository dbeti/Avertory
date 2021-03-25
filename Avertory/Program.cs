using DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Avertory.Extensions;
using Microsoft.Extensions.Configuration;

namespace Avertory
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().MigrateDatabase<AvertoryDbContext>().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
					{
						var env = hostingContext.HostingEnvironment;
						config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
							.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
						config.AddEnvironmentVariables();
					});
					webBuilder.UseStartup<Startup>();
				});
	}
}
