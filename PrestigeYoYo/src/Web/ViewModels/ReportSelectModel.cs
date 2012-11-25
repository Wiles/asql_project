///
///
///

namespace Prestige.ViewModels
{
    using System;

    public enum ReportTypes
    {
        FirstYield,
        FinalYield,
        DefectCategories
    }

    public class ReportSelectModel
    {
        /// <summary>
        /// Gets or sets the report type.
        /// </summary>
        /// <value>
        /// The report type.
        /// </value>
        public ReportTypes ReportType { get; set; }


    }
}