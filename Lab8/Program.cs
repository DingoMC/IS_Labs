using IS_Lab8_JWT;

class Program
{
	public static IHostBuilder CreateHostBuilder(string[] args)
	=> Host.CreateDefaultBuilder(args)
	.ConfigureWebHostDefaults(webBuilder =>
	{
			webBuilder.UseStartup<Startup>().UseUrls("http://localhost:8080");
	});
	public static void Main(string[] args)
	{
		CreateHostBuilder(args).Build().Run();
	}
}