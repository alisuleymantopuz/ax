using System;
using ax.fileProcessor.Storage;
using ax.fileProcessor.Tests.Fake;
using Xunit;

namespace ax.fileProcessor.Tests
{
    public class AuthenticationHeaderValueProviderTests
    {
        [Fact]
        public void AuthenticationHeaderValue_Get_Success()
        {
            var authenticationHeaderValue = new AuthenticationHeaderValueProvider();

            var fakeAuthCredential = FakeObjectCreator.CreateAuthCredential();

            var headerValue = authenticationHeaderValue.Get(fakeAuthCredential);

            Assert.NotNull(headerValue);
        }

        [Fact]
        public void AuthenticationHeaderValue_Get_Failed_With_Null_Credential()
        {
            var authenticationHeaderValue = new AuthenticationHeaderValueProvider();

            Assert.Throws<Exception>(() => authenticationHeaderValue.Get(null));
        }
    }
}
