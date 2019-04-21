using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
        public ZipArchiveEntryItem ResolvePaths(IEnumerable<ZipEntryInfo> entries)
        {
            if (entries == null)
                throw new Exception("Entry collection must not be null");

            List<Tuple<string, bool>> fullNames = PopulateFullNames(entries);

            var root = new ZipArchiveEntryItem()
            {
                Name = "/",
                Folders = new List<ZipArchiveEntryItem>()
            };

            foreach (var path in fullNames)
            {
                var parts = path.Item1.Split('/');

                EnsurePartExists(root, parts, path.Item2);
            }

            return root;
        }

        /// <summary>
        /// Populates the full names.
        /// </summary>
        /// <returns>The full names.</returns>
        /// <param name="entries">Entries with isDirectory info.</param>
        public List<Tuple<string, bool>> PopulateFullNames(IEnumerable<ZipEntryInfo> entries)
        {
            var fullNames = new List<Tuple<string, bool>>();

            foreach (var entry in entries)
            {
                if (entry.FullName.EndsWith("/", StringComparison.Ordinal) && string.IsNullOrEmpty(entry.Name))
                    fullNames.Add(new Tuple<string, bool>(entry.FullName, true));
                else
                    fullNames.Add(new Tuple<string, bool>(entry.FullName, false));
            }

            return fullNames;
        }

        /// <summary>
        /// Ensures the part exists.
        /// </summary>
        /// <param name="zipArchiveEntryItem">Zip archive entry item.</param>
        /// <param name="parts">Parts.</param>
        private void EnsurePartExists(ZipArchiveEntryItem zipArchiveEntryItem, IEnumerable<string> parts, bool isDirectory)
        {
            if (parts.Any())
            {
                var title = parts.First();

                if (!isDirectory && Regex.IsMatch(title, @"^[\w,\s-]+\.[A-Za-z]{3}$"))
                {
                    if (zipArchiveEntryItem.Files == null)
                    {
                        zipArchiveEntryItem.Files = new List<string>();
                    }

                    zipArchiveEntryItem.Files.Add(title);

                    return;
                }

                ZipArchiveEntryItem child = zipArchiveEntryItem.Folders.SingleOrDefault(x => x.Name.Equals(title));

                if (child == null)
                {
                    child = new ZipArchiveEntryItem()
                    {
                        Name = title,
                        Folders = new List<ZipArchiveEntryItem>()
                    };

                    zipArchiveEntryItem.Folders.Add(child);
                }

                EnsurePartExists(child, parts.Skip(1), isDirectory);
            }
        }
    }
}
