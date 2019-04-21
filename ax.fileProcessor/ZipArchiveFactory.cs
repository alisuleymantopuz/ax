using System.IO;
using System.IO.Compression;

namespace ax.fileProcessor
{
    /// <summary>
    /// Zip archive factory.
    /// </summary>
    public class ZipArchiveFactory : IZipArchiveFactory
    {
        public ZipArchive CreateZipArchive(Stream stream)
        {
            return new ZipArchive(stream);
        }
    }
}
