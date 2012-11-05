using System;

namespace Prestige.ViewModels
{
    public class ProductListModel
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