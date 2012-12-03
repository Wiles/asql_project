/// Products controller
/// Codeora 2012
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

    public class ProductsController : PrestigeController
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ProductsController" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="productService">The product service.</param>
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
            var products = this.ProductService.List().ToArray().AsQueryable();
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
                    IEnumerable<Product>,
                    IEnumerable<ProductListModel>>(
                            sorted.Skip((page - 1) * rows).Take(rows).ToArray());

            var objs = from m in models
                       select new
                       {
                           Id = m.Guid,
                           cell = new object[] { m.Guid, m.SKU, m.Description, m.Colour }
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
        /// <param name="guid">The id.</param>
        /// <param name="sku">The sku.</param>
        /// <param name="description">The description.</param>
        /// <param name="colour">The colour.</param>
        /// <returns>Empty result.</returns>
        [HttpPost]
        public ActionResult Edit(string guid, string sku, string description, string colour)
        {
            Guid id = Guid.Empty;

            if (!Guid.TryParse(guid, out id))
            {
                return new EmptyResult();
            }

            var product = this.ProductService.List().FirstOrDefault(
                        p => p.Id == id);

            if (product != null)
            {
                product.SKU = sku;
                product.Description = description;
                product.Colour = colour;

                this.ProductService.Update(product);
            }

            return new EmptyResult();
        }

        /// <summary>
        /// Edits a product by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The edit product view.</returns>
        [HttpPost]
        public ActionResult Add(string sku, string description, string colour)
        {
            var product = new Product()
            {
                SKU = sku,
                Description = description,
                Colour = colour
            };

            this.ProductService.Add(product);
            return new EmptyResult();
        }
    }
}
