using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using ax.encryptionProvider;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ax.fileProcessor
{
    /// <summary>
    /// Zip file processor.
    /// </summary>
    public class ZipFileProcessor : FileProcessor, IZipFileProcessor
    {
        public IAesEncryptionHelper AesEncryptionHelper { get;   set; }

        public IZipArchivePathsResolver ZipArchivePathsResolver { get;   set; }

        public IZipArchiveFactory ZipArchiveFactory { get; set; }

        public ZipFileProcessor(IAesEncryptionHelper aesEncryptionHelper, IZipArchivePathsResolver zipArchivePathsResolver, IZipArchiveFactory zipArchiveFactory)
        {
            AesEncryptionHelper = aesEncryptionHelper;
            ZipArchivePathsResolver = zipArchivePathsResolver;
            ZipArchiveFactory = zipArchiveFactory;
        }


        /// <summary>
        /// Availability the check.
        /// </summary>
        /// <param name="file">File.</param>
        public override Result ValidateFormat(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Result.Fail("File can not be empty!");

            if (!Path.GetExtension(file.FileName).Equals(".zip", StringComparison.OrdinalIgnoreCase))
                return Result.Fail("File must be a zip file!");

            return Result.Ok();
        }

        /// <summary>
        /// Populates the paths.
        /// </summary>
        /// <returns>The paths.</returns>
        /// <param name="file">File.</param>
        public override IEnumerable<string> PopulatePaths(IFormFile file)
        {
            using (ZipArchive archive = ZipArchiveFactory.CreateZipArchive(file.OpenReadStream()))
            {
                return ZipArchivePathsResolver.ResolvePaths(archive.Entries.Select(x => new ZipArchiveEntryItem
                {
                    FullName = x.FullName,
                    Name = x.Name
                }).AsEnumerable());
            }
        }

        /// <summary>
        /// Encrypts the paths.
        /// </summary>
        /// <returns>The paths.</returns>
        /// <param name="paths">Paths.</param>
        public override IEnumerable<string> EncryptPaths(IEnumerable<string> paths)
        {
            return paths.Select(x => AesEncryptionHelper.Encrypt(x)).ToList();
        }

        /// <summary>
        /// Forms the json.
        /// </summary>
        /// <returns>The json.</returns>
        /// <param name="paths">Paths.</param>
        public override string FormJSON(IEnumerable<string> paths)
        {
            return JsonConvert.SerializeObject(paths);
        }
    }
}
