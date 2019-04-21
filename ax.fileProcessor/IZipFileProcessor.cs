using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;

namespace ax.fileProcessor
{
    /// <summary>
    /// Zip file processor.
    /// </summary>
    public interface IZipFileProcessor
    {
        /// <summary>
        /// Process the specified file.
        /// </summary>
        /// <returns>The process.</returns>
        /// <param name="file">File.</param>
        Result<string> Process(IFormFile file);

        /// <summary>
        /// Validates the format.
        /// </summary>
        /// <returns>The format.</returns>
        /// <param name="file">File.</param>
        Result ValidateFormat(IFormFile file);

        /// <summary>
        /// Populates the paths.
        /// </summary>
        /// <returns>The paths.</returns>
        /// <param name="file">File.</param>
        ZipArchiveEntryItem PopulatePaths(IFormFile file);

        /// <summary>
        /// Encrypts the paths.
        /// </summary>
        /// <returns>The paths.</returns>
        /// <param name="entryItem">Entry item.</param>
        ZipArchiveEntryItem EncryptPaths(ZipArchiveEntryItem entryItem);

        /// <summary>
        /// Forms the json.
        /// </summary>
        /// <returns>The json.</returns>
        /// <param name="paths">Paths.</param>
        string FormJSON(ZipArchiveEntryItem paths);
    }
}
