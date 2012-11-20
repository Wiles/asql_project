///
///
///

namespace Prestige.Providers
{
    using System;
    using System.Linq;
    using System.Web.Security;
    using Ninject;
    using Prestige.Services;
    using System.Web.Mvc;
    using Prestige.Web.Providers;

    public class PrestigeRoleProvider : RoleProvider
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="PrestigeRoleProvider" /> class.
        /// </summary>
        public PrestigeRoleProvider()
        {
        }

        /// <summary>
        /// Gets or sets the user service.
        /// </summary>
        /// <value>
        /// The user service.
        /// </value>
        [Inject]
        public IUserServiceFactory UserServiceFactory { get; set; }

        /// <summary>
        /// Gets a value indicating whether the specified user is
        /// in the specified role for the configured applicationName.
        /// </summary>
        /// <param name="username">The user name to search for.</param>
        /// <param name="roleName">The role to search in.</param>
        /// <returns>
        ///   true if the specified user is in the specified role
        ///   for the configured applicationName; otherwise, false.
        /// </returns>
        public override bool IsUserInRole(string username, string roleName)
        {
            var service = this.UserServiceFactory.GetService();
            var user = service.List().FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                return false;
            }

            return user.Roles.Any(r => r.Name == roleName);
        }

        /// <summary>
        /// Gets a list of the roles that a specified user
        /// is in for the configured applicationName.
        /// </summary>
        /// <param name="username">
        ///   The user to return a list of roles for.
        /// </param>
        /// <returns>
        ///   A string array containing the names of all the roles that
        ///   the specified user is in for the configured applicationName.
        /// </returns>
        public override string[] GetRolesForUser(string username)
        {
            var service = this.UserServiceFactory.GetService();
            var user = service.List().FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                return new string[0];
            }

            return user.Roles.Select(r => r.Name).ToArray();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            return false;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}