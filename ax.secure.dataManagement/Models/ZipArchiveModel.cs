using System;
namespace ax.secure.dataManagement.Models
{
    /// <summary>
    /// Zip archive model.
    /// </summary>
    public class ZipArchiveModel
    {
        public ZipArchiveModel()
        {
        }

        public string Content { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
