///
///
///

namespace Prestige.DB.Models
{
    using System;
    using Prestige.EF;

    public class Schedule : IModelBase
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the day.
        /// </summary>
        /// <value>
        /// The day.
        /// </value>
        public string Day { get; set; }

        /// <summary>
        /// Gets or sets the hour.
        /// </summary>
        /// <value>
        /// The hour.
        /// </value>
        public int Hour { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public Product Product { get; set; }
    }
}
