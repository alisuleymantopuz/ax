using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;

namespace ax.fileProcessor
{
    /// <summary>
    /// Abstract file processor template for different kind of file types.
    /// </summary>
    public abstract class FileProcessor
    {
        public Result<string> Process(IFormFile file)
        {
            var result = ValidateFormat(file);

            if (result.IsFailure)
                return Result.Fail<string>(result.Error);

            ZipArchiveEntryItem paths = PopulatePaths(file);

            ZipArchiveEntryItem encryptedPaths = EncryptPaths(paths);

            string json = FormJSON(encryptedPaths);

            return Result.Ok(json);
        }

        /// <summary>
        /// Availability the check.
        /// </summary>
        /// <param name="file">File.</param>
        public abstract Result ValidateFormat(IFormFile file);

        /// <summary>
        /// Populates the paths.
        /// </summary>
        /// <returns>The paths.</returns>
        /// <param name="file">File.</param>
        public abstract ZipArchiveEntryItem PopulatePaths(IFormFile file);

        /// <summary>
        /// Encrypts the paths.
        /// </summary>
        /// <returns>The paths.</returns>
        /// <param name="paths">Paths.</param>
        public abstract ZipArchiveEntryItem EncryptPaths(ZipArchiveEntryItem paths);

        /// <summary>
        /// Forms the json.
        /// </summary>
        /// <returns>The json.</returns>
        /// <param name="paths">Paths.</param>
        public abstract string FormJSON(ZipArchiveEntryItem  paths);
    }
}
