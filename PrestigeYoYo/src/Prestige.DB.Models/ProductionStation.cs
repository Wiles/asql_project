///
///
///

namespace Prestige.DB.Models
{
    using System;
    using Prestige.EF;

    /// <summary>
    /// Represents an Inspection Station in the database.
    /// </summary>
    public class ProductionStation : IModelBase
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the type of the station.
        /// </summary>
        /// <value>
        /// The type of the station.
        /// </value>
        public string StationType { get; set; }
    }
}
