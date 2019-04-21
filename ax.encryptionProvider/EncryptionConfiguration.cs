using Microsoft.Extensions.Configuration;

namespace ax.encryptionProvider
{
    /// <summary>
    /// Encryption configuration.
    /// </summary>
    public class EncryptionConfiguration
    {
        /// <summary>
        /// The company default key.
        /// </summary>
        public const string DefaultKey = "1234567890123456";

        /// <summary>
        /// Gets the encryption key.
        /// </summary>
        /// <value>The encryption key.</value>
        public string EncryptionKey { get; }

        public EncryptionConfiguration(string encyrptionKey)
        {
            this.EncryptionKey = encyrptionKey;

            if (string.IsNullOrEmpty(this.EncryptionKey))
                this.EncryptionKey = DefaultKey;
        }
    }
}
