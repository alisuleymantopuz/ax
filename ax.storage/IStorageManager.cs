using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace ax.storage
{

    /// <summary>
    /// Storage manager.
    /// </summary>
    public interface IStorageManager
    {
        /// <summary>
        /// Save the specified rawContent.
        /// </summary>
        /// <returns>The save.</returns>
        /// <param name="rawContent">Raw content.</param>
        Result Save(string rawContent);

        /// <summary>
        /// List the specified rowCount.
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name="rowCount">Row count.</param>
        Result<IEnumerable<ZipArchive>> List(int rowCount);
    }
}
