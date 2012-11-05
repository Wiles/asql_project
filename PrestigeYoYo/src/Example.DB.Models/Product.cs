using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prestige.EF;

namespace Prestige.DB.Models
{
    /// <summary>
    /// Represents a Person in the database.
    /// </summary>
    public class Product : IModelBase
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string SKU { get; set; }

        public string Description { get; set; }

        public string Colour { get; set; }
    }
}
