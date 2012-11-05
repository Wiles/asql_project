///
///
///

namespace Prestige.DB.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Prestige.EF;

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
