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

    /// <summary>
    /// The scheduling controller.
    /// </summary>
    public class ScheduleController : PrestigeController
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ScheduleController" /> class.
        /// </summary>
        /// <param name="scheduleService">The schedule service.</param>
        public ScheduleController(
                    IMappingEngine mapper,
                    IScheduleService scheduleService,
                    IProductService productService,
                    IUserService userService) : base(mapper)
        {
            if (scheduleService == null)
            {
                throw new ArgumentNullException("scheduleService");
            }
            else if (productService == null)
            {
                throw new ArgumentNullException("productService");
            }
            else if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }

            this.ScheduleService = scheduleService;
            this.ProductService = productService;
            this.UserService = userService;
        }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        private IScheduleService ScheduleService { get; set; }

        /// <summary>
        /// Gets or sets the product service.
        /// </summary>
        /// <value>
        /// The product service.
        /// </value>
        private IProductService ProductService { get; set; }

        /// <summary>
        /// Gets or sets the user service.
        /// </summary>
        /// <value>
        /// The user service.
        /// </value>
        private IUserService UserService { get; set; }

        /// <summary>
        /// Get the index view.
        /// </summary>
        /// <returns>The index view.</returns>
        public ActionResult Index()
        {
            var schedule = this.ScheduleService.List().Where(s => s.Product != null).ToArray();
            var products = this.ProductService.List().OrderBy(p => p.SKU).ToArray();
            ViewBag.Title = "Scheduling";
            return View(
                new Tuple<
                    IEnumerable<Schedule>,
                    IEnumerable<Product>>(
                        schedule,
                        products));
        }

        /// <summary>
        /// Editors this instance.
        /// </summary>
        /// <returns>The editor view.</returns>
        public ActionResult Editor()
        {
            var user = this.Session["username"] as string;
            if (this.UserService.IsUserInRole(user, "Administrator"))
            {
                var products = this.ProductService.List().OrderBy(p => p.SKU).ToArray();
                return PartialView(products);
            }

            return new EmptyResult();
        }

        /// <summary>
        /// Edits the specified day/time schedule.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="start">The start.</param>
        /// <param name="finish">The finish.</param>
        /// <param name="product">The product.</param>
        /// <returns>Redirect to the index action.</returns>
        [HttpPost]
        public ActionResult Edit(string day, int start, int finish, Guid? product)
        {
            try
            {
                var prod = this.ProductService.List()
                                    .FirstOrDefault(p => p.Id == product);

                this.ScheduleService.Add(day, start, finish, prod);
            }
            catch (ArgumentException)
            {
                // woohoo!
            }

            return RedirectToAction("Index");
        }
    }
}
