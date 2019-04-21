using System;
namespace ax.fileProcessor.Storage
{
    /// <summary>
    /// Auth credential.
    /// </summary>
    public class AuthCredential
    {
        public AuthCredential()
        {
        }
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }
    }
}
