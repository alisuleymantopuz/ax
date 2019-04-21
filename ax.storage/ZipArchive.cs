using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ax.storage
{
    /// <summary>
    /// Zip archive.
    /// </summary>
    public class ZipArchive
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        public string Content { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
