using System;
using System.Security.Cryptography;
using System.Text;

namespace ax.encryptionProvider
{
    /// <summary>
    /// Aes encryption helper.
    /// </summary>
    public class AesEncryptionHelper : IAesEncryptionHelper
    {
        public EncryptionConfiguration EncryptionConfiguration { get; set; }

        public AesEncryptionHelper(EncryptionConfiguration encryptionConfiguration)
        {
            EncryptionConfiguration = encryptionConfiguration;
        }

        /// <summary>
        /// Encrypt the specified plainText.
        /// </summary>
        /// <returns>The encrypt.</returns>
        /// <param name="text">Plain text.</param>
        public string Encrypt(string text)
        {
            using (AesManaged aes = Create(EncryptionConfiguration.EncryptionKey))
            {
                var plainText = Encoding.ASCII.GetBytes(text);

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                var result = encryptor.TransformFinalBlock(plainText, 0, plainText.Length);

                return Convert.ToBase64String(result);
            }
        }

        /// <summary>
        /// Decrypt the specified chiperText.
        /// </summary>
        /// <returns>The decrypt.</returns>
        /// <param name="chiperText">Chiper text.</param>
        public string Decrypt(string chiperText)
        {
            using (AesManaged aesAlg = Create(EncryptionConfiguration.EncryptionKey))
            {
                var chiperBytes = Convert.FromBase64String(chiperText);

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                var result = decryptor.TransformFinalBlock(chiperBytes, 0, chiperBytes.Length);

                return Encoding.UTF8.GetString(result);
            }
        }

        /// <summary>
        /// Create the specified key.
        /// </summary>
        /// <returns>The create.</returns>
        /// <param name="key">Key.</param>
        private AesManaged Create(string key)
        {
            var aes = new AesManaged
            {
                Mode = CipherMode.ECB,
                BlockSize = 128,
                KeySize = 128,
                Padding = PaddingMode.PKCS7,
                Key = Encoding.UTF8.GetBytes(key)
            };

            return aes;
        }
    }
}
