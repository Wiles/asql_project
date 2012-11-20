///
///
///

namespace Prestige.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Prestige.Web.Controllers;
    using AutoMapper;

    public class AdminController : PrestigeController
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="AdminController" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        public AdminController(IMappingEngine mapper) : base(mapper)
        {

        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
