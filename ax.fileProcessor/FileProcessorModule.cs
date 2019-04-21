using ax.fileProcessor.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ax.fileProcessor
{
    /// <summary>
    /// File processor module.
    /// </summary>
    public static class FileProcessorModule
    {
        /// <summary>
        /// Registers the file processor module.
        /// </summary>
        /// <param name="services">Services.</param>
        /// <param name="configuration">Configuration.</param>
        public static void RegisterFileProcessorModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IZipFileProcessor, ZipFileProcessor>();
            services.AddSingleton<IZipContentStorageHelper, ZipContentStorageHelper>();
            services.AddSingleton<IZipArchivePathsResolver, ZipArchivePathsResolver>();
            services.AddSingleton<IAuthenticationHeaderValueProvider, AuthenticationHeaderValueProvider>();
            services.AddTransient<IZipArchiveFactory, ZipArchiveFactory>();

            var axSecureUrlConfiguration = new AxSecureUrlConfiguration(configuration["AxSecureUrl"]);
            services.AddSingleton(axSecureUrlConfiguration);

            services.AddHttpClient();
        }
    }
}
