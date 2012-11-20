///
///
///

namespace Prestige.Services
{
    using System.Linq;
    using Prestige.DB.Models;

    public interface IUserService
    {
        /// <summary>
        /// Determines whether a user is in a specified role.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="role">The role.</param>
        /// <returns>
        ///   <c>true</c> if user is in role; otherwise, <c>false</c>.
        /// </returns>
        bool IsUserInRole(string username, string role);

        bool Authenticate(string username, string password);

        void Add(User user);

        void Delete(User user);

        IQueryable<User> List();

        void Update(User user);
    }
}
