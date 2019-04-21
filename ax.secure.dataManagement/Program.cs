using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ax.secure.dataManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(urls: "http://*:5100");
    }
}
