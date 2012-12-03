/// Model for generating reports
/// Codeora 2012
///

namespace Prestige.ViewModels
{
    using System;
    using Prestige.DB.Models;

    public class GenerateReportModel
    {
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the work area.
        /// </summary>
        /// <value>
        /// The work area.
        /// </value>
        public string WorkArea { get; set; }

        /// <summary>
        /// Gets or sets the line.
        /// </summary>
        /// <value>
        /// The line.
        /// </value>
        public int? Line { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public Guid? ProductId { get; set; }
    }
}