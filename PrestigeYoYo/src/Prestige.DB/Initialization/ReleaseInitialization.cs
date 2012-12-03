/// ReleaseInitialization.cs
/// Thomas Kempton 2012
///

namespace Prestige.DB
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using Prestige.DB.Models;

    /// <summary>
    /// Release Database Initialization for Prestige.
    /// </summary>
    public class ReleaseInitialization : CreateDatabaseIfNotExists<PrestigeContext>
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ReleaseInitialization" /> class.
        /// </summary>
        public ReleaseInitialization()
        {
            var user = new Role() { Name = "User" };
            var admin = new Role() { Name = "Administrator" };

            this.Roles = new Collection<Role>() { user, admin };

            this.Users = new Collection<User>()
            {
                new User()
                {
                    UserName = "root",
                    Password = "63a9f0ea7bb98050796b649e85481845",
                    Roles = new Collection<Role>() { admin }
                }
            };
        }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        private ICollection<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        private ICollection<Role> Roles { get; set; }

        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(PrestigeContext context)
        {
            Seed(context, this.Roles);
            Seed(context, this.Users);
        }

        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context">The context.</param>
        /// <param name="entities">The entities.</param>
        private void Seed<T>(PrestigeContext context, IEnumerable<T> entities) where T : class
        {
            var set = context.Set<T>();

            foreach (var entity in entities)
            {
                set.Add(entity);
            }
        }
    }
}
