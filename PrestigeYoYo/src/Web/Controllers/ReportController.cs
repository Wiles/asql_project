﻿/// Reporting controller
/// Codeora 2012
///

namespace Prestige.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using AutoMapper;
    using DotNet.Highcharts;
    using DotNet.Highcharts.Enums;
    using DotNet.Highcharts.Helpers;
    using DotNet.Highcharts.Options;
    using Prestige.DB.Models;
    using Prestige.Services;
    using Prestige.ViewModels;
    using System.Collections.ObjectModel;

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
                IProductionStationService stationService)
            : base(mapper)
        {
            if (productService == null)
            {
                throw new ArgumentNullException("productService");
            }
            else if (productionStageService == null)
            {
                throw new ArgumentNullException("productionStageService");
            }
            else if (stationService == null)
            {
                throw new ArgumentNullException("stationService");
            }

            this.ProductService = productService;
            this.ProductionStageService = productionStageService;
            this.StationService = stationService;
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
        /// Gets or sets the station service.
        /// </summary>
        /// <value>
        /// The station service.
        /// </value>
        private IProductionStationService StationService { get; set; }

        /// <summary>
        /// Gets a list of available report types.
        /// </summary>
        /// <returns>List of report types.</returns>
        private static IEnumerable<ReportTypeModel> ReportTypes()
        {
            var list = new List<ReportTypeModel>();

            foreach (var method in typeof(ReportController).GetMethods())
            {
                var attrs = method.GetCustomAttributes(typeof(ReportMethodAttribute), true);
                if (attrs.Any())
                {
                    var attr = attrs[0] as ReportMethodAttribute;
                    list.Add(new ReportTypeModel(method.Name, attr.Name));
                }
            }

            return list;
        }
        
        /// <summary>
        /// Index view.
        /// </summary>
        /// <returns>The reports view.</returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Production Reports";

            var model = new ReportSelectModel();
            model.ReportTypes = ReportTypes();
            model.Products = this.ProductService.List().OrderBy(p => p.Description).ToList();
            model.WorkAreas = this.ProductionStageService.List()
                                        .Select(p => p.WorkArea).Distinct();

            return View(model);
        }

        /// <summary>
        /// Gets the lines specific to a work area.
        /// </summary>
        /// <param name="workArea">The work area.</param>
        /// <returns>The lines options.</returns>
        public ActionResult LinesOptions(string workArea)
        {
            var lines = this.ProductionStageService.List()
                            .Where(s => s.WorkArea == workArea)
                            .Select(s => s.LineNumber)
                            .Distinct()
                            .ToList();

            var sb = new StringBuilder("<option value=''>All</option>");
            foreach (var line in lines.OrderBy(i => i))
            {
                sb.Append(string.Format("<option value='{0}'>{0}</option>", line));
            }

            return Content(sb.ToString());
        }

        /// <summary>
        /// Generates the report.
        /// </summary>
        /// <param name="reportType">Type of the report.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="product">The product.</param>
        /// <param name="workArea">The work area.</param>
        /// <param name="line">The line.</param>
        /// <returns>The report view.</returns>
        public ActionResult GenerateReport(
                string reportType,
                DateTime startDate,
                DateTime endDate,
                Guid? productId,
                string workArea,
                int? line)
        {
            var model = new GenerateReportModel();
            model.StartDate = startDate;
            model.EndDate = endDate;
            model.Line = line;
            model.ProductId = productId;
            model.WorkArea = workArea;

            var method = typeof(ReportController).GetMethod(reportType);
            var ret = method.Invoke(this, new object[] { model });

            return ret as ActionResult;
        }

        /// <summary>
        /// Filters production stages by the report model.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="model">The model.</param>
        /// <returns>Filtered queryable.</returns>
        private IQueryable<ProductionStage> FilterStages(IQueryable<ProductionStage> list, GenerateReportModel model)
        {
            list = list.Where(s => s.TimeStamp > model.StartDate && s.TimeStamp < model.EndDate);

            if (model.ProductId.HasValue)
            {
                list = list.Where(s => s.ProductionEntry.Product.Id == model.ProductId);
            }

            if (!string.IsNullOrEmpty(model.WorkArea))
            {
                list = list.Where(s => s.WorkArea == model.WorkArea);
                if (model.Line.HasValue)
                {
                    var i = model.Line.Value;
                    list = list.Where(s => s.LineNumber == i);
                }
            }

            return list;
        }

        #region Report Action Methods

        /// <summary>
        /// Generates the first yield report.
        /// </summary>
        /// <returns>The first yield report view.</returns>
        [ReportMethod("First Yield")]
        public ActionResult FirstYield(GenerateReportModel model)
        {
            // get list of stations
            var stations = this.StationService.List().Where(s => s.StationType == "Inspection").ToList();
            var yieldModel = new FirstYieldModel();
            yieldModel.Charts = new Collection<Highcharts>();

            foreach (var station in stations.OrderBy(s => s.Identifier))
            {
                // inspection and related rework and scrap states
                var inspection = station.Identifier;
                var rework = inspection + "_REWORK";
                var reject = inspection + "_SCRAP";

                // linq to get the stages
                var list = this.FilterStages(this.ProductionStageService.List(), model)
                               .Where(s => s.Station.Identifier == rework
                                   || s.Station.Identifier == reject
                                   || s.Station.Identifier == inspection)
                               .GroupBy(s => s.Station)
                               .OrderBy(g => g.Count())
                               .Select(g => new { Name = (g.Key.StationType == "Inspection" ? "Passed" : g.Key.StationType), Y = g.Count() })
                               .ToArray();

                // get the total of "not first times"
                var bad = list.Where(a => a.Name != "Passed").Sum(a => a.Y);

                // we need to subract the number of
                // not first times from the first times
                var results = new List<object>();
                foreach (var a in list)
                {
                    if (a.Name == "Passed")
                    {
                        results.Add(new { Name = a.Name, Y = a.Y - bad });
                    }
                    else
                    {
                        results.Add(a);
                    }
                }
                
                // build chart
                var chart = ChartHelper.PieChart(
                    string.Format("{0} First Time Yield", station.Description),
                    "First Time Yield",
                    results.ToArray(),
                    station.Identifier);

                yieldModel.Charts.Add(chart);
            }

            return PartialView("FirstYield", yieldModel);
        }

        /// <summary>
        /// Generates the final yield report.
        /// </summary>
        /// <returns>The final yield report view.</returns>
        [ReportMethod("Final Yield")]
        public ActionResult FinalYield(GenerateReportModel model)
        {
            // linq to get the final yeild of good, bad, and ugly
            var list = this.FilterStages(this.ProductionStageService.List(), model)
                .Where(s => s.Station.StationType == "Scrap" || s.Station.StationType == "Complete")
                .GroupBy(e => e.Station)
                .OrderBy(g => g.Count())
                .Select(g => new { Name = g.Key.Description, Y = g.Count() })
                .ToArray();

            return PartialView("FinalYield", ChartHelper.PieChart("Final Yields", "Station Final Yields", list, "final"));
        }

        /// <summary>
        /// Generates the defects categories report.
        /// </summary>
        /// <returns>The defects categories report view.</returns>
        [ReportMethod("Defect Occurrence")]
        public ActionResult DefectCategories(GenerateReportModel model)
        {
            // linq to get defects by occurrence type
            var defectCategories = this.FilterStages(this.ProductionStageService.List(), model)
                .Where(s => s.ProductFlaw != null)
                .GroupBy(s => s.ProductFlaw)
                .OrderByDescending(g => g.Count())
                .Select(g => new { Name = g.Key.Reason, Count = g.Count() })
                .ToArray();

            // get the bar values
            var barValues = defectCategories.Select(d => d.Count);

            // get the line values
            var linesValues = new List<int>();
            foreach (var num in barValues)
            {
                var next = linesValues.LastOrDefault() + num;
                linesValues.Add(next);
            }

            // build the chart
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

            return PartialView("DefectCategories", chart);
        }

        #endregion
    }
}
