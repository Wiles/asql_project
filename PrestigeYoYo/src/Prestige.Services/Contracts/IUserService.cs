///
///
///

namespace Prestige.Services
{
    using System;
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

        /// <summary>
        /// Authenticates the specified user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        ///   <c>true</c> if authenticated; otherwise <c>false</c>
        /// </returns>
        bool Authenticate(string username, string password);

        /// <summary>
        /// Adds the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        void Add(User user);

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        void Delete(User user);

        /// <summary>
        /// Lists the users.
        /// </summary>
        /// <returns>IQueryable of users.</returns>
        IQueryable<User> List();

        /// <summary>
        /// Changes a users password.
        /// </summary>
        /// <param name="id">The users id.</param>
        /// <param name="password">The new password.</param>
        void ChangePassword(Guid id, string password);
        
        /// <summary>
        /// Sets the roles.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="roles">The roles.</param>
        void SetRoles(Guid id, Role[] roles);
        
        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <returns>IQueryable of roles.</returns>
        IQueryable<Role> GetRoles();
    }
}
