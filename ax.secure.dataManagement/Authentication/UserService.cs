using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ax.secure.dataManagement.Authentication
{
    /// <summary>
    /// User service.
    /// </summary>
    public class UserService : IUserService
    {
        public IUserProvider UserProvider { get; set; }

        public UserService(IUserProvider userProvider)
        {
            UserProvider = userProvider;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => UserProvider.GetApplicationUsers().SingleOrDefault(x => x.Username == username && x.Password == password));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            user.Password = null;
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.Run(() => UserProvider.GetApplicationUsers().Select(x =>
            {
                x.Password = null;
                return x;
            }));
        }
    }
}
