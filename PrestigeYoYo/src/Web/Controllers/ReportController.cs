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
            ViewBag.Title = "Reports";
            return View();
        }
    }
}
