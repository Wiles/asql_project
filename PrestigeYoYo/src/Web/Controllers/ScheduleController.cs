﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prestige.Services;
using AutoMapper;
using Prestige.DB.Models;

namespace Prestige.Controllers
{
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
                    IProductService productService) : base(mapper)
        {
            if (scheduleService == null)
            {
                throw new ArgumentNullException("scheduleService");
            }

            if (productService == null)
            {
                throw new ArgumentNullException("productService");
            }

            this.ScheduleService = scheduleService;
            this.ProductService = productService;
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
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
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
        /// Edits the specified day.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="start">The start.</param>
        /// <param name="finish">The finish.</param>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        public ActionResult Edit(string day, int start, int finish, Guid product)
        {
            var prod = this.ProductService.List()
                                .FirstOrDefault(p => p.Id == product);

            this.ScheduleService.Add(day, start, finish, prod);

            return RedirectToAction("Index");
        }
    }
}