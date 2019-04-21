using ax.fileProcessor.Storage;
using Xunit;
using Moq;
using FluentAssertions;
using System.Net.Http;
using ax.fileProcessor.Tests.Fake;
using System.Threading.Tasks;
using System;
using ax.fileProcessor.Tests.Stub;

namespace ax.fileProcessor.Tests
{
    public class ZipContentStorageHelperTests
    {
        public Mock<AxSecureUrlConfiguration> MockAxSecureConfiguration { get; set; }

        public Mock<IAuthenticationHeaderValueProvider> MockAuthenticationHeaderValueProvider { get; set; }

        public Mock<IHttpClientFactory> MockHttpClientFactory { get; set; }

        public ZipContentStorageHelperTests()
        {
            MockAxSecureConfiguration = new Mock<AxSecureUrlConfiguration>("http://host:5000/test/test");

            MockAuthenticationHeaderValueProvider = new Mock<IAuthenticationHeaderValueProvider>();

            MockHttpClientFactory = new Mock<IHttpClientFactory>();

            var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) =>
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                return Task.FromResult(response);
            });

            var client = new HttpClient(clientHandlerStub);

            MockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);
        }

        [Fact]
        public async void SendContent_WithAuthorizationAndContent_Success()
        {
            var zipContentStorageHelper = new ZipContentStorageHelper(MockAxSecureConfiguration.Object,
                                                                      MockAuthenticationHeaderValueProvider.Object,
                                                                      MockHttpClientFactory.Object);

            var authCredential = FakeObjectCreator.CreateAuthCredential();

            var result = await zipContentStorageHelper.SendContent("{\"data\": {}}", authCredential);

            result.IsSuccess.Should().Be(true);
        }

        [Fact]
        public async void SendContent_WithoutAuthCredential_Failed()
        {
            var zipContentStorageHelper = new ZipContentStorageHelper(MockAxSecureConfiguration.Object,
                                                                      MockAuthenticationHeaderValueProvider.Object,
                                                                      MockHttpClientFactory.Object);

            var authCredential = FakeObjectCreator.CreateAuthCredential();

            await Assert.ThrowsAsync<Exception>(() => zipContentStorageHelper.SendContent("{\"data\": {}}", null));
        }
    }


}
