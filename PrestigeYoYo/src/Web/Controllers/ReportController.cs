using System;
using System.Web.Mvc;
using AutoMapper;
using Prestige.Services;

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
                IProductionEntryService entryService) : base(mapper)
        {
            if (productService == null)
            {
                throw new ArgumentNullException("productService");
            }

            if (entryService == null)
            {
                throw new ArgumentNullException("entryService");
            }

            this.ProductService = productService;
            this.EntryService = entryService;
        }

        /// <summary>
        /// Gets or sets the person service.
        /// </summary>
        /// <value>
        /// The person service.
        /// </value>
        private IProductService ProductService { get; set; }

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
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Reports";
            return View();
        }
    }
}
