///
///
///

namespace Prestige.DB.Models
{
    using System;
    using Prestige.EF;

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
    }
}
