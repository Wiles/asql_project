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
    public class HomeController : PrestigeController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="personService">The person service.</param>
        public HomeController(IMappingEngine mapper,
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
            var products = this.ProductService.List().ToArray();

            var model = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductListModel>>(products);

            ViewBag.Title = "Home!";
            return View(model);
        }

        [HttpPost]
        public ActionResult login(string username, string password)
        {
            return Json("");
        }
    }
}
