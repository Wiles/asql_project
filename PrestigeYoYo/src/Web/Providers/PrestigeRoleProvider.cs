/// Custom role provider to verify user roles
/// Codeora 2012
///

namespace Prestige.Providers
{
    using System;
    using System.Linq;
    using System.Web.Security;
    using Ninject;
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
            var user = service.List().FirstOrDefault(
                                            u => u.UserName == username);
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
            var user = service.List().FirstOrDefault(
                                        u => u.UserName == username);
            if (user == null)
            {
                return new string[0];
            }

            return user.Roles.Select(r => r.Name).ToArray();
        }

        /// <summary>
        /// Adds the specified user names to the specified
        /// roles for the configured applicationName.
        /// </summary>
        /// <param name="usernames">
        ///   A string array of user names to be added to the specified roles.
        /// </param>
        /// <param name="roleNames">
        ///   A string array of the role names
        ///   to add the specified user names to.
        /// </param>
        public override void AddUsersToRoles(
                string[] usernames,
                string[] roleNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets or sets the name of the application
        /// to store and retrieve role information for.
        /// </summary>
        /// <returns>
        ///   The name of the application to store
        ///   and retrieve role information for.
        /// </returns>
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

        /// <summary>
        /// Adds a new role to the data source
        /// for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to create.</param>
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a role from the data source
        /// for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to delete.</param>
        /// <param name="throwOnPopulatedRole">
        ///   If true, throw an exception if <paramref name="roleName" />
        ///   has one or more members and do not delete
        ///   <paramref name="roleName" />.
        /// </param>
        /// <returns>
        ///   true if the role was successfully deleted; otherwise, false.
        /// </returns>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            return false;
        }

        /// <summary>
        /// Gets an array of user names in a role where the
        /// user name contains the specified user name to match.
        /// </summary>
        /// <param name="roleName">The role to search in.</param>
        /// <param name="usernameToMatch">The user name to search for.</param>
        /// <returns>
        ///   A string array containing the names of all the users
        ///   where the user name matches <paramref name="usernameToMatch" />
        ///   and the user is a member of the specified role.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a list of all the roles for the configured applicationName.
        /// </summary>
        /// <returns>
        ///   A string array containing the names of all the roles stored
        ///   in the data source for the configured applicationName.
        /// </returns>
        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a list of users in the specified
        /// role for the configured applicationName.
        /// </summary>
        /// <param name="roleName">
        ///   The name of the role to get the list of users for.
        /// </param>
        /// <returns>
        ///   A string array containing the names of all the users who are
        ///   members of the specified role for the configured applicationName.
        /// </returns>
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the specified user names from the
        /// specified roles for the configured applicationName.
        /// </summary>
        /// <param name="usernames">
        ///   A string array of user names to
        ///   be removed from the specified roles.
        /// </param>
        /// <param name="roleNames">
        ///   A string array of role names to
        ///   remove the specified user names from.
        /// </param>
        public override void RemoveUsersFromRoles(
                    string[] usernames,
                    string[] roleNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a value indicating whether the specified role name already
        /// exists in the role data source for the configured applicationName.
        /// </summary>
        /// <param name="roleName">
        ///   The name of the role to search for in the data source.
        /// </param>
        /// <returns>
        ///   true if the role name already exists in the data source
        ///   for the configured applicationName; otherwise, false.
        /// </returns>
        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}