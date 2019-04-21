using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ax.storage.Tests
{

    public class StorageManagerTests
    {
        public Mock<IEncryptedZipArchiveHandler> MockEncryptedZipArchiveHandler { get; set; }

        public Mock<ZipArchiveDBContext> MockZipArchiveDBContext { get; set; }

        public Mock<DbSet<ZipArchive>> MockZipArchiveDbSetMock { get; set; }

        public List<ZipArchive> _zipArchives = new List<ZipArchive>();

        public StorageManagerTests()
        {
            var fakeZipArchiveContent = new ZipArchive
            {
                Content = "[ \"Ford\", \"BMW\", \"Fiat\" ]",
                CreationDate = DateTime.Now,
                Id = new Random().Next(0, 100)
            };

            _zipArchives.Add(fakeZipArchiveContent);

            MockEncryptedZipArchiveHandler = new Mock<IEncryptedZipArchiveHandler>();
            MockZipArchiveDBContext = new Mock<ZipArchiveDBContext>();
            MockZipArchiveDbSetMock = new Mock<DbSet<ZipArchive>>();

            var zipArchivesQueryable = _zipArchives.AsQueryable();
            MockZipArchiveDbSetMock.As<IQueryable<ZipArchive>>().Setup(m => m.Provider).Returns(zipArchivesQueryable.Provider);
            MockZipArchiveDbSetMock.As<IQueryable<ZipArchive>>().Setup(m => m.Expression).Returns(zipArchivesQueryable.Expression);
            MockZipArchiveDbSetMock.As<IQueryable<ZipArchive>>().Setup(m => m.ElementType).Returns(zipArchivesQueryable.ElementType);
            MockZipArchiveDbSetMock.As<IQueryable<ZipArchive>>().Setup(m => m.GetEnumerator()).Returns(zipArchivesQueryable.GetEnumerator());
            MockZipArchiveDBContext.Setup(m => m.ZipArchives).Returns(MockZipArchiveDbSetMock.Object);
        }

        [Fact]
        public void SaveRawContent_Success()
        {
            var deserializedExpected = new List<string> { "Ford", "BMW", "Fiat" };

            var rawContent = "[ \"Ford\", \"BMW\", \"Fiat\" ]";

            MockEncryptedZipArchiveHandler.Setup(x => x.DeserializeRawContent(It.IsAny<string>())).Returns(deserializedExpected);

            var storeManager = new StorageManager(MockEncryptedZipArchiveHandler.Object, MockZipArchiveDBContext.Object);

            var result = storeManager.Save(rawContent);

            result.IsSuccess.Should().BeTrue();

            MockZipArchiveDbSetMock.Verify(x => x.Add(It.IsAny<ZipArchive>()), Times.Once);

            MockZipArchiveDBContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public void SaveRawContent_Failed_With_EmptyContent()
        {
            var storeManager = new StorageManager(MockEncryptedZipArchiveHandler.Object, MockZipArchiveDBContext.Object);

            var result = storeManager.Save(null);

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void SaveRawContent_Failed_With_NonDeserializedStatus()
        {
            var rawContent = "[ \"Ford\", \"BMW\", \"Fiat\" ]";

            List<string> list = null;

            MockEncryptedZipArchiveHandler.Setup(x => x.DeserializeRawContent(It.IsAny<string>())).Returns(list);

            var storeManager = new StorageManager(MockEncryptedZipArchiveHandler.Object, MockZipArchiveDBContext.Object);

            var result = storeManager.Save(rawContent);

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void ListZipArchives_Success()
        {
            var storeManager = new StorageManager(MockEncryptedZipArchiveHandler.Object, MockZipArchiveDBContext.Object);

            var result = storeManager.List(1);

            result.Should().NotBeNull();
        }
    }
}
