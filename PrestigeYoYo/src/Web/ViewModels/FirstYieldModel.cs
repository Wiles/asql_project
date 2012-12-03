/// Model for viewing a first yield report
/// Codeora 2012
///

namespace Prestige.ViewModels
{
    using System.Collections.Generic;
    using DotNet.Highcharts;

    public class FirstYieldModel
    {
        /// <summary>
        /// Gets or sets the charts.
        /// </summary>
        /// <value>
        /// The charts.
        /// </value>
        public ICollection<Highcharts> Charts { get; set; }
    }
}