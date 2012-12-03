/// Production entry entity
/// Codeora 2012
///

namespace Prestige.DB.Models
{
    using System;
    using System.Collections.Generic;
    using Prestige.EF;

    public class ProductionEntry : IModelBase
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the stages.
        /// </summary>
        /// <value>
        /// The stages.
        /// </value>
        public ICollection<ProductionStage> Stages { get; set; }
    }
}
