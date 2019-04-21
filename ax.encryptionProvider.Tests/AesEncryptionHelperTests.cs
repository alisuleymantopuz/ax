using System.Text;
using Xunit;

namespace ax.encryptionProvider.Tests
{
    public class AesEncryptionHelperTests
    {
        public const string DefaultKey = "1234567890123456";

        [Fact]
        public void Encryption_With_Text_Success()
        {
            EncryptionConfiguration encryptionConfiguration = new EncryptionConfiguration(DefaultKey);

            IAesEncryptionHelper aesEncryptionHelper = new AesEncryptionHelper(encryptionConfiguration);

            var text = "aesEncrypt";
            
            var encrypted = aesEncryptionHelper.Encrypt(text);

            Assert.NotNull(encrypted);

            Assert.NotEmpty(encrypted);
        }

        [Fact]
        public void Encryption_And_Decryption_With_Text_Success()
        {
            EncryptionConfiguration encryptionConfiguration = new EncryptionConfiguration(DefaultKey);

            IAesEncryptionHelper aesEncryptionHelper = new AesEncryptionHelper(encryptionConfiguration);

            var text = "aesEncrypt";
            
            var encrypted = aesEncryptionHelper.Encrypt(text);

            var decrypted = aesEncryptionHelper.Decrypt(encrypted);

            Assert.NotNull(encrypted);
            
            Assert.NotNull(decrypted);
            
            Assert.Equal(text, decrypted);
        }
    }
}
