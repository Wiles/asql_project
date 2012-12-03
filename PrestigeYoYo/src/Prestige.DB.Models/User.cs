/// User entity
/// Codeora 2012
///

namespace Prestige.DB.Models
{
    using System;
    using Prestige.EF;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a user in the database.
    /// </summary>
    public class User : IModelBase
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        /// <value>
        /// The user name.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password hash.
        /// </summary>
        /// <value>
        /// The password hash.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public ICollection<Role> Roles { get; set; }
    }
}
