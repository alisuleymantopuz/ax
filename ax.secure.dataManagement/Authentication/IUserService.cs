using System.Collections.Generic;
using System.Threading.Tasks;

namespace ax.secure.dataManagement.Authentication
{
    /// <summary>
    /// User service.
    /// </summary>
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
    }
}
