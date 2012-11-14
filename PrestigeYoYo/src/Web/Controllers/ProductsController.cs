using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Prestige.DB.Models;
using Prestige.Services;
using Prestige.ViewModels;

namespace Prestige.Controllers
{
    public class ProductsController : PrestigeController
    {
        public ProductsController(
                IMappingEngine mapper,
                IProductService productService) : base(mapper)
        {
            if (productService == null)
            {
                throw new ArgumentNullException("productService");
            }

            this.ProductService = productService;
        }

        /// <summary>
        /// Gets or sets the person service.
        /// </summary>
        /// <value>
        /// The person service.
        /// </value>
        private IProductService ProductService { get; set; }

        /// <summary>
        /// Index view.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "View Products";
            return View();
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
            var products = this.ProductService.List().ToArray().AsQueryable();
            var filtered = products;

            if (_search)
            {
                filtered = products.Search(searchField, searchString);
            }

            var sorted = filtered.Sort(sidx, sord);

            var total = filtered.Count();
            var pages = (int)Math.Ceiling((double)total / (double)rows);

            var models = Mapper.Map<
                    IEnumerable<Product>,
                    IEnumerable<ProductListModel>>(
                            sorted.Skip((page - 1) * rows).Take(rows).ToArray());

            var objs = from m in models
                       select new
                       {
                           Id = m.Id,
                           cell = new object[] { m.Id, m.SKU, m.Description, m.Colour }
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
        /// <param name="id">The id.</param>
        /// <returns>The edit product view.</returns>
        public ActionResult Edit(string id)
        {
            Guid guid = Guid.Empty;

            if (!Guid.TryParse(id, out guid))
            {
                throw new ArgumentException("id");
            }

            return View();
        }
    }
}
