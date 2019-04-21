using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ax.storage
{
    /// <summary>
    /// Storage module.
    /// </summary>
    public static class StorageModule
    {
        /// <summary>
        /// Registers the storage module.
        /// </summary>
        /// <param name="services">Services.</param>
        /// <param name="configuration">Configuration.</param>
        public static void RegisterStorageModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEncryptedZipArchiveHandler, EncryptedZipArchiveHandler>();
            services.AddScoped<IStorageManager, StorageManager>();
        }

        /// <summary>
        /// Registers the data context.
        /// </summary>
        /// <param name="services">Services.</param>
        /// <param name="configuration">Configuration.</param>
        /// <param name="databaseName">Database name.</param>
        public static void RegisterDataContext(this IServiceCollection services, IConfiguration configuration, string databaseName) {

            services.AddDbContext<ZipArchiveDBContext>(options => options.UseInMemoryDatabase(databaseName: databaseName));
        }
    }
}
