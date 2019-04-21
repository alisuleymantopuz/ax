namespace ax.encryptionProvider
{
    public interface IAesEncryptionHelper
    {
        /// <summary>
        /// Encrypt the specified plainText.
        /// </summary>
        /// <returns>The encrypt.</returns>
        /// <param name="plainText">Plain text.</param>
        string Encrypt(string text);

        /// <summary>
        /// Decrypt the specified chiperText.
        /// </summary>
        /// <returns>The decrypt.</returns>
        /// <param name="chiperText">Chiper text.</param>
        string Decrypt(string chiperText);
    }
}