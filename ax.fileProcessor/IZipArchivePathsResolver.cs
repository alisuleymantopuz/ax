using System;
using System.Collections.Generic;

namespace ax.fileProcessor
{
    /// <summary>
    /// Zip archive paths resolver.
    /// </summary>
    public interface IZipArchivePathsResolver
    {
        /// <summary>
        /// Resolves the paths.
        /// </summary>
        /// <returns>The paths.</returns>
        /// <param name="entries">Entries.</param>
        ZipArchiveEntryItem ResolvePaths(IEnumerable<ZipEntryInfo> entries);

        /// <summary>
        /// Populates the full names.
        /// </summary>
        /// <returns>The full names.</returns>
        /// <param name="entries">Entries.</param>
        List<Tuple<string, bool>> PopulateFullNames(IEnumerable<ZipEntryInfo> entries);
    }
}
