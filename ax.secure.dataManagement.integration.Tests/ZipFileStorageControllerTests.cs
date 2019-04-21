using System.Net;
using System.Net.Http;
using System.Text;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
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
            var rawContent = "{\"Name\":\"z3T3j3ZDSFLM6DVc5l7s5Q==\",\"Files\":[\"LwhnBoFuFhXYPwNPzW5Vmg==\"],\"Folders\":[]}";

            var stringContent = new StringContent(rawContent, Encoding.UTF8, "application/json");

            var httpResponse = await _client.PostAsync("/api/ZipFileStorage/", stringContent);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
