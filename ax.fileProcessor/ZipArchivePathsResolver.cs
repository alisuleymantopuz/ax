using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Compression;
using ax.encryptionProvider;

namespace ax.fileProcessor
{
    /// <summary>
    /// Zip archive paths resolver.
    /// </summary>
    public class ZipArchivePathsResolver : IZipArchivePathsResolver
    {
        /// <summary>
        /// Resolves the paths.
        /// </summary>
        /// <returns>The paths.</returns>
        /// <param name="entries">Entries.</param>
        public IEnumerable<string> ResolvePaths(IEnumerable<ZipArchiveEntryItem> entries)
        {
            if (entries == null)
                throw new Exception("Entry collection must not be null");

            var entryNames = new List<string>();

            foreach (var entry in entries)
            {
                if (entry.FullName.EndsWith("/", StringComparison.Ordinal) && string.IsNullOrEmpty(entry.Name))
                {
                    entryNames.Add(entry.FullName);
                }
                else
                {
                    entryNames.Add(entry.Name);
                }
            }

            return entryNames;
        }
    }
}
