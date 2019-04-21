using System.Net.Http;
using System.Net.Http.Headers;
using ax.fileProcessor.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace ax.secure.dataManagement.integration.Tests
{
    /// <summary>
    /// Test fixture.
    /// </summary>
    public class TestFixture
    {
        public HttpClient Client { get; }

        public IConfiguration Configuration { get; }

        private TestServer Server;

        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
        }

        public TestFixture()
        {
            var projectDir = System.IO.Directory.GetCurrentDirectory();

            var configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(projectDir)
                    .AddJsonFile("appsettings.json")
                    .Build();

            var webHostBuilder = new WebHostBuilder()
                                .UseEnvironment("Development")
                                .UseContentRoot(projectDir)
                                .UseConfiguration(configurationBuilder)
                                .UseStartup<TestStartup>();

            Server = new TestServer(webHostBuilder);

            Configuration = configurationBuilder;

            Client = Server.CreateClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var authenticationHeaderValueProvider = new AuthenticationHeaderValueProvider();
            Client.DefaultRequestHeaders.Authorization = authenticationHeaderValueProvider.Get(new AuthCredential
            {
                Password = "axi",
                Username = "axi"
            });
        }
    }
}
