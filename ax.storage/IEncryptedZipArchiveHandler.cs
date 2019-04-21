using System;
using System.Collections.Generic;

namespace ax.storage
{
    /// <summary>
    /// Encrypted zip archive handler.
    /// </summary>
    public interface IEncryptedZipArchiveHandler
    {
        /// <summary>
        /// Deserializes the content of the raw.
        /// </summary>
        /// <returns>The raw content.</returns>
        /// <param name="rawContent">Raw content.</param>
        IEnumerable<string> DeserializeRawContent(string rawContent);

        /// <summary>
        /// Decrypts the zip archive.
        /// </summary>
        /// <returns>The zip archive.</returns>
        /// <param name="decryptedPaths">Decrypted paths.</param>
        IEnumerable<string> DecryptZipArchive(IEnumerable<string> decryptedPaths);
    }
}
