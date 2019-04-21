using ax.fileProcessor.Tests.Fake;
using Xunit;
using System;

namespace ax.fileProcessor.Tests
{
    public class ZipArchivePathsResolverTests
    {
        [Fact]
        public void ResolvePaths_Success_With_EntryCollection()
        {
            var zipArchivePathsResolver = new ZipArchivePathsResolver();

            var paths = zipArchivePathsResolver.ResolvePaths(FakeObjectCreator.FakeZipArchiveEntryItems());

            Assert.NotNull(paths);
        }

        [Fact]
        public void ResolvePaths_Failed_With_Null_EntryCollection()
        {
            var zipArchivePathsResolver = new ZipArchivePathsResolver();

            Assert.Throws<Exception>(() => zipArchivePathsResolver.ResolvePaths(null));
        }
    }
}
