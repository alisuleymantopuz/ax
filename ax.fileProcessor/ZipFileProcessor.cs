using System;
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
        public IAesEncryptionHelper AesEncryptionHelper { get; set; }

        public IZipArchivePathsResolver ZipArchivePathsResolver { get; set; }

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
        public override ZipArchiveEntryItem PopulatePaths(IFormFile file)
        {
            using (ZipArchive archive = ZipArchiveFactory.CreateZipArchive(file.OpenReadStream()))
            {
                var entryInfos = archive.Entries.Select(x => new ZipEntryInfo
                {
                    FullName = x.FullName,
                    Name = x.Name
                }).AsEnumerable();

                return ZipArchivePathsResolver.ResolvePaths(entryInfos);
            }
        }

        /// <summary>
        /// Encrypts the paths.
        /// </summary>
        /// <returns>The paths.</returns>
        /// <param name="paths">Paths.</param>
        public override ZipArchiveEntryItem EncryptPaths(ZipArchiveEntryItem paths)
        {
            if (paths == null)
                throw new Exception("Paths must not be null!");

            EnsureFieldsEncrypted(paths, AesEncryptionHelper);

            return paths;
        }

        private void EnsureFieldsEncrypted(ZipArchiveEntryItem paths, IAesEncryptionHelper aesEncryptionHelper)
        {
            paths.Name = aesEncryptionHelper.Encrypt(paths.Name);

            if (paths.Files != null && paths.Files.Any())
            {
                for (int i = 0; i < paths.Files.Count(); i++)
                {
                    paths.Files[i] = aesEncryptionHelper.Encrypt(paths.Files[i]);
                }
            }

            if (paths.Folders != null && paths.Folders.Any())
            {
                foreach (var folder in paths.Folders)
                {
                    EnsureFieldsEncrypted(folder, aesEncryptionHelper);
                }
            }
        }

        /// <summary>
        /// Forms the json.
        /// </summary>
        /// <returns>The json.</returns>
        /// <param name="paths">Paths.</param>
        public override string FormJSON(ZipArchiveEntryItem paths)
        {
            return JsonConvert.SerializeObject(paths);
        }
    }
}
