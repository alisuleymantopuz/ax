using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

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

            IEnumerable<string> paths = PopulatePaths(file);

            IEnumerable<string> encryptedPaths = EncryptPaths(paths);

            string json = FormJSON(encryptedPaths);

            return Result.Ok(json);
        }

        /// <summary>
        /// Availability the check.
        /// </summary>
        /// <param name="file">File.</param>
        public abstract Result ValidateFormat(IFormFile file);

        /// <summary>
        /// Populates the file list.
        /// </summary>
        /// <returns>The file list.</returns>
        /// <param name="file">File.</param>
        public abstract IEnumerable<string> PopulatePaths(IFormFile file);

        /// <summary>
        /// Encrypts the paths.
        /// </summary>
        /// <returns>The paths.</returns>
        /// <param name="paths">Paths.</param>
        public abstract IEnumerable<string> EncryptPaths(IEnumerable<string> paths);

        /// <summary>
        /// Forms the json.
        /// </summary>
        /// <returns>The json.</returns>
        /// <param name="paths">Paths.</param>
        public abstract string FormJSON(IEnumerable<string> paths);
    }
}
