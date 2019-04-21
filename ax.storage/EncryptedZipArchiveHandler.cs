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
        /// <param name="decryptedPaths">Decrypted paths.</param>
        public IEnumerable<string> DecryptZipArchive(IEnumerable<string> decryptedPaths)
        {
            return decryptedPaths.Select(x => AesEncryptionHelper.Decrypt(x)).ToList();
        }

        /// <summary>
        /// Deserializes the content of the raw.
        /// </summary>
        /// <returns>The raw content.</returns>
        /// <param name="rawContent">Raw content.</param>
        public IEnumerable<string> DeserializeRawContent(string rawContent)
        {
            return JsonConvert.DeserializeObject<IEnumerable<string>>(rawContent);
        }
    }
}
