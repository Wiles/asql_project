﻿/// Product entity
/// Codeora 2012
///

namespace Prestige.DB.Models
{
    using System;
    using Prestige.EF;

    /// <summary>
    /// Represents a Product in the database.
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

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the colour.
        /// </summary>
        /// <value>
        /// The colour.
        /// </value>
        public string Colour { get; set; }
    }
}
