using ax.encryptionProvider;
using Moq;
using FluentAssertions;
using Xunit;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ax.fileProcessor.Tests
{

    public class ZipFileProcessorTests
    {
        public Mock<IAesEncryptionHelper> MockAesEncryptionHelper { get; set; }
        public Mock<IZipArchivePathsResolver> MockZipArchivePathsResolver { get; set; }
        public Mock<IZipArchiveFactory> MockZipArchiveFactory { get; set; }


        public ZipFileProcessorTests()
        {
            MockAesEncryptionHelper = new Mock<IAesEncryptionHelper>();
            MockZipArchivePathsResolver = new Mock<IZipArchivePathsResolver>();
            MockZipArchiveFactory = new Mock<IZipArchiveFactory>();
        }

        [Fact]
        public void ValidateFormat_Success()
        {
            var content = "Archives";
            var fileName = "Archive.zip";
            var fileMock = new Mock<IFormFile>();
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;

            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var zipFileProcessor = new ZipFileProcessor(MockAesEncryptionHelper.Object, MockZipArchivePathsResolver.Object, MockZipArchiveFactory.Object);

            var result = zipFileProcessor.ValidateFormat(fileMock.Object);

            result.IsSuccess.Should().Be(true);
        }

        [Fact]
        public void ValidateFormat_Failed_With_NullFormFile()
        {
            var zipFileProcessor = new ZipFileProcessor(MockAesEncryptionHelper.Object, MockZipArchivePathsResolver.Object, MockZipArchiveFactory.Object);

            var result = zipFileProcessor.ValidateFormat(null);

            result.IsFailure.Should().Be(true);
        }


        [Fact]
        public void ValidateFormat_Failed_With_WrongFormat()
        {
            var content = "test/Readme.txt";
            var fileName = "Archive.png";
            var fileMock = new Mock<IFormFile>();
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;

            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var zipFileProcessor = new ZipFileProcessor(MockAesEncryptionHelper.Object, MockZipArchivePathsResolver.Object, MockZipArchiveFactory.Object);

            var result = zipFileProcessor.ValidateFormat(fileMock.Object);

            result.IsFailure.Should().Be(true);
        }

        [Fact]
        public void PopulatePaths_Success()
        {
            //Due to readonlycollection, Zip entries can't be mocked!
            using (FileStream zipToOpen = new FileStream("../../../Data/FakeArchive.zip", FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    MockZipArchiveFactory.Setup(x => x.CreateZipArchive(It.IsAny<Stream>())).Returns(archive);

                    var zipFileProcessor = new ZipFileProcessor(MockAesEncryptionHelper.Object, MockZipArchivePathsResolver.Object, MockZipArchiveFactory.Object);

                    var fileMock = new Mock<IFormFile>();
                    fileMock.Setup(_ => _.OpenReadStream()).Returns(zipToOpen);
                    fileMock.Setup(_ => _.FileName).Returns("FakeArchive.zip");
                    fileMock.Setup(_ => _.Length).Returns(zipToOpen.Length);

                    var result = zipFileProcessor.PopulatePaths(fileMock.Object);
                }
            }
        }

        [Fact]
        public void EncryptPaths_Success()
        {
            MockAesEncryptionHelper.Setup(x => x.Encrypt(It.IsAny<string>())).Returns("encrypted");

            var zipFileProcessor = new ZipFileProcessor(MockAesEncryptionHelper.Object, MockZipArchivePathsResolver.Object, MockZipArchiveFactory.Object);

            var fakePath = new List<string>() { "decrypted" };

            var encryptedPaths = zipFileProcessor.EncryptPaths(fakePath);

            encryptedPaths.Count().Should().Be(1);

            encryptedPaths.First().Should().Be("encrypted");
        }

        [Fact]
        public void FormJSON_Success()
        {
            var zipFileProcessor = new ZipFileProcessor(MockAesEncryptionHelper.Object, MockZipArchivePathsResolver.Object, MockZipArchiveFactory.Object);

            var fakePath = new List<string>() { "decrypted" };

            var expected = JsonConvert.SerializeObject(fakePath);

            var formJson = zipFileProcessor.FormJSON(fakePath);

            Assert.Equal(expected, formJson);
        }
    }
}
