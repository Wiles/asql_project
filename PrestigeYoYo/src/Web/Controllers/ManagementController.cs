///
///
///

namespace Prestige.Web.Controllers
{
    using System.Web.Mvc;
    using Prestige.Controllers;
    using AutoMapper;

    [PrestigeAuthorize(Roles = "Administrator")]
    public class ManagementController : PrestigeController
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ManagementController" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        public ManagementController(IMappingEngine mapper) : base(mapper)
        {
        }

        /// <summary>
        /// Gets the index view.
        /// </summary>
        /// <returns>The index view.</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
