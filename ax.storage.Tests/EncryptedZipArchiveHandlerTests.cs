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

            var zipArchiveEntryItem = new ZipArchiveEntryItem { Name = "encrypted" };
            zipArchiveEntryItem.Files = new List<string>() { "encrypted", "encrypted", "encrypted" };

            var subZipArchiveEntryItem = new ZipArchiveEntryItem { Name = "encrypted" };
            subZipArchiveEntryItem.Files = new List<string>() { "encrypted", "encrypted", "encrypted" };

            zipArchiveEntryItem.Folders = new List<ZipArchiveEntryItem>
            {
                subZipArchiveEntryItem
            };

            var encryptedZipArchiveHandler = new EncryptedZipArchiveHandler(MockAesEncryptionHelper.Object);

            var decryptedPaths = encryptedZipArchiveHandler.DecryptZipArchive(zipArchiveEntryItem);

            decryptedPaths.Name.Should().Be("decrypted");

            decryptedPaths.Files.First().Should().Be("decrypted");

            decryptedPaths.Folders.First().Name.Should().Be("decrypted");

            decryptedPaths.Folders.First().Files.First().Should().Be("decrypted");
        }

        [Fact]
        public void DeserializeRawContent_Success()
        {
            var rawContent = "{\"Name\":\"z3T3j3ZDSFLM6DVc5l7s5Q==\",\"Files\":[\"LwhnBoFuFhXYPwNPzW5Vmg==\"],\"Folders\":[]}";

            var encryptedZipArchiveHandler = new EncryptedZipArchiveHandler(MockAesEncryptionHelper.Object);

            ZipArchiveEntryItem deserializedContent = encryptedZipArchiveHandler.DeserializeRawContent(rawContent);

            deserializedContent.Should().NotBeNull();

            deserializedContent.Name.Should().NotBeNull();
        }
    }
}
