///
///
///

namespace Prestige
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DotNet.Highcharts;
    using DotNet.Highcharts.Options;
    using DotNet.Highcharts.Enums;
    using System.Drawing;
    using DotNet.Highcharts.Helpers;

    public class ChartHelper
    {
        /// <summary>
        /// Creates a pie chart.
        /// </summary>
        /// <param name="chartName">Name of the chart.</param>
        /// <param name="axisName">Name of the axis.</param>
        /// <param name="results">The results.</param>
        /// <returns>A chart object.</returns>
        public static Highcharts PieChart(string chartName, string axisName, object[] results)
        {
            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { PlotShadow = false })
                .SetTitle(new Title { Text = chartName })
                .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage.toFixed(2) +' %'; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Pie = new PlotOptionsPie
                    {
                        AllowPointSelect = true,
                        Cursor = Cursors.Pointer,
                        DataLabels = new PlotOptionsPieDataLabels
                        {
                            Color = ColorTranslator.FromHtml("#000000"),
                            ConnectorColor = ColorTranslator.FromHtml("#000000"),
                            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage.toFixed(2) +' %'; }"
                        }
                    }
                })
                .SetSeries(new Series
                {
                    Type = ChartTypes.Pie,
                    Name = axisName,
                    Data = new Data(results)
                });

            return chart;
        }
    }
}