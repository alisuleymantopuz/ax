using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ax.controlPanel.Models
{
    /// <summary>
    /// Upload zip file model.
    /// </summary>
    public class UploadZipFileModel
    {
        /// <summary>
        /// Gets or sets the zip file.
        /// </summary>
        /// <value>The zip file.</value>
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Upload")]
        public IFormFile ZipFile { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
