using System;
using System.Collections.Generic;
using System.Linq;
using ax.encryptionProvider;
using Newtonsoft.Json;

namespace ax.storage
{
    /// <summary>
    /// Encrypted zip archive handler.
    /// </summary>
    public class EncryptedZipArchiveHandler : IEncryptedZipArchiveHandler
    {
        public IAesEncryptionHelper AesEncryptionHelper { get; set; }

        public EncryptedZipArchiveHandler(IAesEncryptionHelper aesEncryptionHelper)
        {
            AesEncryptionHelper = aesEncryptionHelper;
        }

        /// <summary>
        /// Decrypts the zip archive.
        /// </summary>
        /// <returns>The zip archive.</returns>
        /// <param name="paths">Decrypted paths.</param>
        public ZipArchiveEntryItem DecryptZipArchive(ZipArchiveEntryItem paths)
        {
            if (paths == null)
                throw new Exception("Paths must not be null!");

            EnsureFieldsDecrypted(paths, AesEncryptionHelper);

            return paths;
        }

        private void EnsureFieldsDecrypted(ZipArchiveEntryItem paths, IAesEncryptionHelper aesEncryptionHelper)
        {
            paths.Name = aesEncryptionHelper.Decrypt(paths.Name);

            if (paths.Files != null && paths.Files.Any())
            {
                for (int i = 0; i < paths.Files.Count(); i++)
                {
                    paths.Files[i] = aesEncryptionHelper.Decrypt(paths.Files[i]);
                }
            }

            if (paths.Folders != null && paths.Folders.Any())
            {
                foreach (var folder in paths.Folders)
                {
                    EnsureFieldsDecrypted(folder, aesEncryptionHelper);
                }
            }
        }

        /// <summary>
        /// Deserializes the content of the raw.
        /// </summary>
        /// <returns>The raw content.</returns>
        /// <param name="rawContent">Raw content.</param>
        public ZipArchiveEntryItem DeserializeRawContent(string rawContent)
        {
            return JsonConvert.DeserializeObject<ZipArchiveEntryItem>(rawContent);
        }
    }
}
