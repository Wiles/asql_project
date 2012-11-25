using System;
using System.Web.Mvc;
using AutoMapper;
using Prestige.Services;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Enums;
using System.Drawing;
using DotNet.Highcharts;
using System.Linq;
using Prestige.DB.Models;
using System.Collections;
using System.Collections.Generic;
using Prestige.ViewModels;

namespace Prestige.Controllers
{
    /// <summary>
    /// Class for managing Home requests.
    /// </summary>
    public class ReportController : PrestigeController
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="productService">The product service.</param>
        public ReportController(
                IMappingEngine mapper,
                IProductService productService,
                IProductionStageService productionStageService,
                IProductionEntryService entryService)
            : base(mapper)
        {
            if (productService == null)
            {
                throw new ArgumentNullException("productService");
            }

            if (productionStageService == null)
            {
                throw new ArgumentNullException("productionStageService");
            }

            if (entryService == null)
            {
                throw new ArgumentNullException("entryService");
            }

            this.ProductService = productService;
            this.ProductionStageService = productionStageService;
            this.EntryService = entryService;
        }

        /// <summary>
        /// Gets or sets the product service.
        /// </summary>
        /// <value>
        /// The product service.
        /// </value>
        private IProductService ProductService { get; set; }

        /// <summary>
        /// Gets or sets the production stage service.
        /// </summary>
        /// <value>
        /// The production stage service.
        /// </value>
        private IProductionStageService ProductionStageService { get; set; }

        /// <summary>
        /// Gets or sets the entry service.
        /// </summary>
        /// <value>
        /// The entry service.
        /// </value>
        private IProductionEntryService EntryService { get; set; }

        /// <summary>
        /// Index view.
        /// </summary>
        /// <returns>The reports view.</returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Production Reports";
            return View(new ReportSelectModel());
        }

        public ActionResult FirstYield()
        {
            ViewBag.Title = "First Time Yield Report";

            var results = EntryService.List().ToArray().AsQueryable()
                    .Where(e => !e.Stages.Any(s => s.ProductFlaw != null))
                    .SelectMany(e => e.Stages)
                    .GroupBy(e => e.Station)
                    .OrderBy(g => g.Count())
                    .Select(g => new { Name = g.Key.Identifier, Y = g.Count() })
                    .ToArray();

            return View(PieChart("First Time Yields", "Station First Time Yields", results));
        }

        public ActionResult FinalYield()
        {
            ViewBag.Title = "Final Yield Report";

            var results = EntryService.List().ToArray().AsQueryable()
                    .Where(e => !e.Stages.Any(s => s.ProductFlaw != null && s.ProductFlaw.Decision != "Rework"))
                    .SelectMany(e => e.Stages)
                    .GroupBy(e => e.Station)
                    .OrderBy(g => g.Count())
                    .Select(g => new { Name = g.Key.Identifier, Y = g.Count() })
                    .ToArray();

            return View(PieChart("First Time Yields", "Station First Time Yields", results));
        }

        private Highcharts PieChart(string chartName, string axisName, object[] results)
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

        public ActionResult DefectCategories()
        {
            ViewBag.Title = "Defect Categories Report";
            
            var defectCategories = EntryService.List().ToArray().AsQueryable()
                .SelectMany(s => s.Stages)
                .Where(s => s.ProductFlaw != null)
                .GroupBy(s => s.ProductFlaw)
                .OrderByDescending(g => g.Count())
                .Select(g => new { Name = g.Key.Identifier, NumberOfDefects = g.Count() })
                .ToArray();

            var barValues = defectCategories.Select(d => d.NumberOfDefects);

            var linesValues = new List<int>();
            foreach (var num in barValues)
            {
                var next = linesValues.LastOrDefault() + num;
                linesValues.Add(next);
            }

            Highcharts chart = new Highcharts("chart")
           .InitChart(new Chart { ZoomType = ZoomTypes.Xy })
           .SetTitle(new Title { Text = "Defect Categories" })
           .SetSubtitle(new Subtitle { Text = "" })
           .SetXAxis(new XAxis { Categories = defectCategories.Select(d => d.Name).ToArray() })
           .SetYAxis(new[]
            {
                new YAxis
                {
                    Labels = new YAxisLabels
                    {
                        Formatter = "function() { return this.value; }",
                        Style = "color: '#89A54E'"
                    },
                    Title = new XAxisTitle
                    {
                        Text = "Count",
                        Style = "color: '#89A54E'"
                    }
                },
                new YAxis
                {
                    Labels = new YAxisLabels
                    {
                        Formatter = "function() { return this.value; }",
                        Style = "color: '#4572A7'"
                    },
                    Title = new XAxisTitle
                    {
                        Text = "Defect Categories",
                        Style = "color: '#4572A7'"
                    },
                    Opposite = true
                }
            })
           .SetTooltip(new Tooltip
           {
               Formatter = "function() { return ''+ this.x +': '+ this.y; }"
           })
           .SetLegend(new Legend
           {
               Layout = Layouts.Vertical,
               Align = HorizontalAligns.Left,
               X = 120,
               VerticalAlign = VerticalAligns.Top,
               Y = 100,
               Floating = true,
               BackgroundColor = ColorTranslator.FromHtml("#FFFFFF")
           })
           .SetSeries(new[]
            {
                new Series
                {
                    Name = "Count",
                    Color = ColorTranslator.FromHtml("#4572A7"),
                    Type = ChartTypes.Column,
                    YAxis = 0,
                    Data = new Data(barValues.Cast<object>().ToArray())
                },
                new Series
                {
                    Name = "Defect Categories",
                    Color = ColorTranslator.FromHtml("#89A54E"),
                    Type = ChartTypes.Spline,
                    YAxis = 0,
                    Data = new Data(linesValues.Cast<object>().ToArray())
                }
            });

            return View(chart);
        }
    }
}
