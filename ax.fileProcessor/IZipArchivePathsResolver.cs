using System.Collections.Generic;

namespace ax.fileProcessor
{
    /// <summary>
    /// Zip archive paths resolver.
    /// </summary>
    public interface IZipArchivePathsResolver
    {
        IEnumerable<string> ResolvePaths(IEnumerable<ZipArchiveEntryItem> entries);
    }
}
