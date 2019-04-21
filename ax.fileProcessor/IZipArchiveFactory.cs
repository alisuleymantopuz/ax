using System;
using System.IO;
using System.IO.Compression;

namespace ax.fileProcessor
{
    /// <summary>
    /// Zip archive factory.
    /// </summary>
    public interface IZipArchiveFactory
    {
        ZipArchive CreateZipArchive(Stream stream);
    }
}
