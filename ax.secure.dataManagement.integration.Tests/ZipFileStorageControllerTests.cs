using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using ax.encryptionProvider;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace ax.secure.dataManagement.integration.Tests
{
    public class ZipFileStorageControllerTests : IClassFixture<TestFixture>
    {
        private HttpClient _client;
        private IConfiguration _configuration;
        public ZipFileStorageControllerTests(TestFixture fixture)
        {
            _client = fixture.Client;
            _configuration = fixture.Configuration;
        }


        [Fact]
        public async void Get_ZipFileStorageItems_Success()
        {
            var httpResponse = await _client.GetAsync("/api/ZipFileStorage/");

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            stringResponse.Should().NotBeNull();

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void Post_Raw_Content_To_Be_Saved_Success()
        {
            var paths = new List<string>() { "Ford", "BMW", "Fiat" };

            var encryptionConfiguration = new EncryptionConfiguration(_configuration.GetValue<string>("EncryptionKey"));

            var encryptionHelper = new AesEncryptionHelper(encryptionConfiguration);

            var encryptedList = paths.Select(x => encryptionHelper.Encrypt(x)).ToList();

            var rawContent = JsonConvert.SerializeObject(encryptedList);

            var stringContent = new StringContent(rawContent, Encoding.UTF8, "application/json");

            var httpResponse = await _client.PostAsync("/api/ZipFileStorage/", stringContent);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
