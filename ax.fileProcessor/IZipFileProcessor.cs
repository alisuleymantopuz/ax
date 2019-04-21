using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;

namespace ax.fileProcessor
{
    public interface IZipFileProcessor
    {
        Result<string> Process(IFormFile file);

        Result ValidateFormat(IFormFile file);

        IEnumerable<string> PopulatePaths(IFormFile file);

        IEnumerable<string> EncryptPaths(IEnumerable<string> paths);
    }
}
