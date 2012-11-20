///
///
///

namespace Prestige.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Prestige.DB.Models;
    using Prestige.Services;
    using Prestige.ViewModels;

    public class StationsController : PrestigeController
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ProductsController" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="productService">The product service.</param>
        public StationsController(
                IMappingEngine mapper,
                IProductionStationService stationService) : base(mapper)
        {
            if (stationService == null)
            {
                throw new ArgumentNullException("stationService");
            }

            this.StationService = stationService;
        }

        /// <summary>
        /// Gets or sets the person service.
        /// </summary>
        /// <value>
        /// The person service.
        /// </value>
        private IProductionStationService StationService { get; set; }

        /// <summary>
        /// Index view.
        /// </summary>
        /// <returns>The index view.</returns>
        public ActionResult Index()
        {
            return PartialView();
        }

        /// <summary>
        /// Lists existing products for Grid consumption.
        /// </summary>
        /// <param name="sidx">The sidx.</param>
        /// <param name="sord">The sord.</param>
        /// <param name="page">The page.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="_search">if set to <c>true</c> [_search].</param>
        /// <param name="searchField">The search field.</param>
        /// <param name="searchOper">The search oper.</param>
        /// <param name="searchString">The search string.</param>
        /// <returns>JSON encoded products list.</returns>
        [HttpPost]
        public ActionResult Data(
                string sidx,
                string sord,
                int page,
                int rows,
                bool _search,
                string searchField,
                string searchOper,
                string searchString)
        {
            var products = this.StationService.List().ToArray().AsQueryable();
            var filtered = products;

            if (_search)
            {
                filtered = products.Search(searchField, searchString);
            }

            var sorted = filtered.Sort(sidx, sord);

            var total = filtered.Count();
            var pages = (int)Math.Ceiling((double)total / (double)rows);

            if (page > pages)
            {
                page = pages;
            }

            var models = Mapper.Map<
                    IEnumerable<ProductionStation>,
                    IEnumerable<StationListModel>>(
                        sorted.Skip((page - 1) * rows).Take(rows).ToArray());

            var objs = from m in models
                       select new
                       {
                           Id = m.Guid,
                           cell = new object[] { m.Guid, m.Description, m.Identifier }
                       };

            var data = new
            {
                total = pages,
                page = page,
                records = total,
                rows = objs
            };

            return Json(data);
        }

        /// <summary>
        /// Edits a product by id.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="identifier">The identifier.</param>
        /// <returns>Empty result.</returns>
        [HttpPost]
        public ActionResult Edit(
                string guid,
                string description,
                string identifier)
        {
            Guid id = Guid.Empty;

            if (!Guid.TryParse(guid, out id))
            {
                return new EmptyResult();
            }

            var station = this.StationService.List().FirstOrDefault(
                        p => p.Id == id);

            if (station != null)
            {
                station.Description = description;
                station.Identifier = identifier;
                this.StationService.Update(station);
            }

            return new EmptyResult();
        }

        /// <summary>
        /// Edits a product by id.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="identifier">The identifier.</param>
        /// <returns>Empty result.</returns>
        [HttpPost]
        public ActionResult Add(string description, string identifier)
        {
            var station = new ProductionStation()
            {
                Description = description,
                Identifier = identifier
            };

            this.StationService.Add(station);
            return new EmptyResult();
        }
    }
}
