using System.Collections.Generic;
using System.Linq;
using ax.encryptionProvider;
using FluentAssertions;
using Moq;
using Xunit;

namespace ax.storage.Tests
{
    public class EncryptedZipArchiveHandlerTests
    {
        public Mock<IAesEncryptionHelper> MockAesEncryptionHelper { get; set; }

        public EncryptedZipArchiveHandlerTests()
        {
            MockAesEncryptionHelper = new Mock<IAesEncryptionHelper>();
        }

        [Fact]
        public void DecryptZipArchive_Success()
        {
            MockAesEncryptionHelper.Setup(x => x.Decrypt(It.IsAny<string>())).Returns("decrypted");

            var fakePath = new List<string>() { "encrypted" };

            var encryptedZipArchiveHandler = new EncryptedZipArchiveHandler(MockAesEncryptionHelper.Object);

            var decryptedPaths = encryptedZipArchiveHandler.DecryptZipArchive(fakePath);

            decryptedPaths.Count().Should().Be(1);

            decryptedPaths.First().Should().Be("decrypted");
        }

        [Fact]
        public void DeserializeRawContent_Success()
        {
            var rawContent = "[ \"Ford\", \"BMW\", \"Fiat\" ]";

            var encryptedZipArchiveHandler = new EncryptedZipArchiveHandler(MockAesEncryptionHelper.Object);

            IEnumerable<string> dersializedContent = encryptedZipArchiveHandler.DeserializeRawContent(rawContent);

            dersializedContent.Count().Should().Be(3);

            dersializedContent.First().Should().Be("Ford");
        }
    }
}
