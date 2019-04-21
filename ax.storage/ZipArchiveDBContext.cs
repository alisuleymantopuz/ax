using Microsoft.EntityFrameworkCore;

namespace ax.storage
{
    /// <summary>
    /// Zip archive DBC ontext.
    /// </summary>
    public class ZipArchiveDBContext : DbContext
    {
        public ZipArchiveDBContext(DbContextOptions<ZipArchiveDBContext> options)
            : base(options)
        {
        }

        public ZipArchiveDBContext()
        {

        }

        public virtual DbSet<ZipArchive> ZipArchives { get; set; }
    }

}
