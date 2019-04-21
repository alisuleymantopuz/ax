using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace ax.secure.dataManagement.Authentication
{
    /// <summary>
    /// User provider.
    /// </summary>
    public class UserProvider : IUserProvider
    {
        public IConfiguration Configuration { get; set; }

        public UserProvider(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IList<User> GetApplicationUsers()
        {
            return new List<User>
            {
                new User
                {
                    Username = Configuration["Username"],
                    Password = Configuration["Password"]
                }
            };
        }
    }
}
