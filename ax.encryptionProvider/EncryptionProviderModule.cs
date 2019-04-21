using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ax.encryptionProvider
{
    /// <summary>
    /// File processor module.
    /// </summary>
    public static class EncryptionProviderModule
    {
        /// <summary>
        /// Registers the encryption provider module.
        /// </summary>
        /// <param name="services">Services.</param>
        /// <param name="configuration">Configuration.</param>
        public static void RegisterEncryptionProviderModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAesEncryptionHelper, AesEncryptionHelper>();

            var encryptionConfiguration = new EncryptionConfiguration(configuration["EncryptionKey"]);
            services.AddSingleton(encryptionConfiguration);
        }
    }
}
