///
///
///

namespace Prestige.ViewModels
{
    using System.Collections.Generic;
    using Prestige.DB.Models;

    public class ReportSelectModel
    {
        /// <summary>
        /// Gets or sets the report type.
        /// </summary>
        /// <value>
        /// The report type.
        /// </value>
        public IEnumerable<ReportTypeModel> ReportTypes { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public IEnumerable<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the work area.
        /// </summary>
        /// <value>
        /// The work area.
        /// </value>
        public IEnumerable<string> WorkAreas { get; set; }
    }
}