///
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

    public class UserService : IUserService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UserService(IUserRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.Repository = repository;
        }

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        private IUserRepository Repository { get; set; }

        /// <summary>
        /// Adds the specified product.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Add(User user)
        {
            user.UserName = user.UserName.ToLower();
            user.Password = GetHash(user.Password);
            this.Repository.Add(user);
            this.Repository.SaveChanges();
        }

        /// <summary>
        /// Updates the specified product.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Update(User user)
        {
            user.UserName = user.UserName.ToLower();
            user.Password = GetHash(user.Password);
            this.Repository.SaveChanges();
        }

        /// <summary>
        /// Deletes the specified product.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Delete(User user)
        {
            this.Repository.Delete(user);
            this.Repository.SaveChanges();
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
            var user = this.Repository.FirstOrDefault(u => u.UserName == low);
            if (user != null)
            {
                var hash = GetHash(password);
                return user.Password == hash;
            }

            return false;
        }

        /// <summary>
        /// Gets the hash of a string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>The hash.</returns>
        private static string GetHash(string str)
        {
            var md5 = MD5.Create();
            var bytes = Encoding.ASCII.GetBytes(str);
            var hash = md5.ComputeHash(bytes);

            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Lists the products.
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> List()
        {
            return this.Repository;
        }
    }
}
