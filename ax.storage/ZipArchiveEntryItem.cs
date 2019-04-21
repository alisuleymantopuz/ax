using System.Collections.Generic;

namespace ax.storage
{
    /// <summary>
    /// Zip archive entry item.
    /// </summary>
    public class ZipArchiveEntryItem
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>The files.</value>
        public List<string> Files { get; set; }

        /// <summary>
        /// Gets or sets the folders.
        /// </summary>
        /// <value>The folders.</value>
        public List<ZipArchiveEntryItem> Folders { get; set; }
    }
}
