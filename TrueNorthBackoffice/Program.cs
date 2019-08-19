using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace TrueNorthBackoffice
{
	public static class Program
	{
		public static void Main(String[] args) => CreateWebHostBuilder(args).Build().Run();

		public static IWebHostBuilder CreateWebHostBuilder(String[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseStartup<Startup>();
	}
}
