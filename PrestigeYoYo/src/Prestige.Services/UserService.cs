﻿///
///
///

namespace Prestige.Services
{
    using System;
    using System.Linq;
    using Prestige.DB.Models;
    using Prestige.Repositories;
    using System.Security.Cryptography;
    using System.Text;
using System.Collections.Generic;

    public class UserService : IUserService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService" /> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="encryptor">The encryptor.</param>
        public UserService(
                IUserRepository userRepository,
                IRoleRepository roleRepository,
                IEncryptor encryptor)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }
            else if (roleRepository == null)
            {
                throw new ArgumentNullException("roleRepository");
            }
            else if (encryptor == null)
            {
                throw new ArgumentNullException("encryptor");
            }

            this.UserRepository = userRepository;
            this.RoleRepository = roleRepository;
            this.Encryptor = encryptor;
        }

        /// <summary>
        /// Gets or sets the user repository.
        /// </summary>
        /// <value>
        /// The user repository.
        /// </value>
        private IUserRepository UserRepository { get; set; }

        /// <summary>
        /// Gets or sets the role repository.
        /// </summary>
        /// <value>
        /// The role repository.
        /// </value>
        private IRoleRepository RoleRepository { get; set; }

        /// <summary>
        /// Gets or sets the encryptor.
        /// </summary>
        /// <value>
        /// The encryptor.
        /// </value>
        private IEncryptor Encryptor { get; set; }

        /// <summary>
        /// Adds the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Add(User user)
        {
            user.UserName = user.UserName.ToLower();
            user.Password = this.Encryptor.GetHash(user.Password);
            this.UserRepository.Add(user);
            this.UserRepository.SaveChanges();
        }

        /// <summary>
        /// Changes a users password.
        /// </summary>
        /// <param name="id">The users id.</param>
        /// <param name="password">The new password.</param>
        public void ChangePassword(Guid id, string password)
        {
            var user = this.UserRepository.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.Password = this.Encryptor.GetHash(password);
                this.UserRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Sets the roles.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="roles">The roles.</param>
        public void SetRoles(Guid id, Role[] roles)
        {
            var user = this.UserRepository.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                user.Roles.Clear();
                foreach (var role in roles)
                {
                    user.Roles.Add(role);
                }

                this.UserRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <returns>IQueryable of roles.</returns>
        public IQueryable<Role> GetRoles()
        {
            return this.RoleRepository;
        }

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Delete(User user)
        {
            this.UserRepository.Delete(user);
            this.UserRepository.SaveChanges();
        }

        /// <summary>
        /// Authenticates the specified user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        ///   <c>true</c> if authenticated; otherwise <c>false</c>
        /// </returns>
        public bool Authenticate(string username, string password)
        {
            var low = username.ToLower();
            var user = this.UserRepository.FirstOrDefault(u => u.UserName == low);
            if (user != null)
            {
                var hash = this.Encryptor.GetHash(password);
                return user.Password == hash;
            }

            return false;
        }

        /// <summary>
        /// Determines whether a user is in a specified role.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="role">The role.</param>
        /// <returns>
        ///   <c>true</c> if user is in role; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUserInRole(string username, string role)
        {
            if (username == null || role == null)
            {
                return false;
            }

            var low = username.ToLower();
            var user = this.UserRepository.FirstOrDefault(u => u.UserName == low);
            if (user != null)
            {
                return user.Roles.Any(r => r.Name == role);
            }

            return false;
        }

        /// <summary>
        /// Lists the users.
        /// </summary>
        /// <returns>IQueryable of users.</returns>
        public IQueryable<User> List()
        {
            // hack to overload the expression...
            return this.UserRepository.Where(u => true);
        }
    }
}
