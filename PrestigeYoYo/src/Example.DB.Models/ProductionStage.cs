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

    public class ProductionStage : IModelBase
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the production id.
        /// </summary>
        /// <value>
        /// The production id.
        /// </value>
        public Guid ProductionId { get; set; }

        /// <summary>
        /// Gets or sets the work area.
        /// </summary>
        /// <value>
        /// The work area.
        /// </value>
        public string WorkArea { get; set; }

        /// <summary>
        /// Gets or sets the line number.
        /// </summary>
        /// <value>
        /// The line number.
        /// </value>
        public int LineNumber { get; set; }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>
        /// The time stamp.
        /// </value>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the station.
        /// </summary>
        /// <value>
        /// The station.
        /// </value>
        public ProductionStation Station { get; set; }

        /// <summary>
        /// Gets or sets the product flaw.
        /// </summary>
        /// <value>
        /// The product flaw.
        /// </value>
        public ProductFlawType ProductFlaw { get; set; }
    }
}
