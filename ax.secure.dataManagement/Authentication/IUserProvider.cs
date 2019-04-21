using System.Collections.Generic;

namespace ax.secure.dataManagement.Authentication
{
    /// <summary>
    /// User provider.
    /// </summary>
    public interface IUserProvider
    {
        IList<User> GetApplicationUsers();
    }
}
