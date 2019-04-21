using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;

namespace ax.storage
{
    /// <summary>
    /// Storage manager.
    /// </summary>
    public class StorageManager : IStorageManager
    {
        public IEncryptedZipArchiveHandler EncryptedZipArchiveHandler { get; set; }
        public ZipArchiveDBContext ZipArchiveDBContext { get; set; }

        public StorageManager(IEncryptedZipArchiveHandler encryptedZipArchiveHandler, ZipArchiveDBContext zipArchiveDBContext)
        {
            EncryptedZipArchiveHandler = encryptedZipArchiveHandler;
            ZipArchiveDBContext = zipArchiveDBContext;
        }

        /// <summary>
        /// Validates and Save the specified rawContent.
        /// </summary>
        /// <returns>The save with validations.</returns>
        /// <param name="rawContent">Raw content.</param>
        public Result Save(string rawContent)
        {
            if (string.IsNullOrEmpty(rawContent))
                return Result.Fail("Content must not be empty!");

            var deserializedContent = EncryptedZipArchiveHandler.DeserializeRawContent(rawContent);

            if (deserializedContent == null)
                return Result.Fail("Content can't be deserialized!");

            var decryptedPaths = EncryptedZipArchiveHandler.DecryptZipArchive(deserializedContent);

            var zipArchive = new ZipArchive
            {
                Content = JsonConvert.SerializeObject(decryptedPaths),
                CreationDate = DateTime.Now
            };

            ZipArchiveDBContext.ZipArchives.Add(zipArchive);
            ZipArchiveDBContext.SaveChanges();

            return Result.Ok();
        }

        /// <summary>
        /// List the specified rowCount.
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name="rowCount">Row count.</param>
        public Result<IEnumerable<ZipArchive>> List(int rowCount)
        {
            var zipArchives = ZipArchiveDBContext.ZipArchives.Take(rowCount).AsEnumerable();

            return Result.Ok(zipArchives);
        }
    }
}
