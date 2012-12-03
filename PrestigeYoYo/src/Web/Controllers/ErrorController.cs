/// Error handling controller
/// Codeora 2012
///

namespace Prestige.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    public class ErrorController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Generic error handling!</returns>
        public ActionResult Index(int? code, string message)
        {
            if (!code.HasValue)
            {
                code = 500;
            }

            switch(code)
            {
                case 403:
                    return View("Forbidden");
                case 404:
                    return View("NotFound");
            }

            var model = new Tuple<int, string>(code.Value, message);
            return View(model);
        }

        /// <summary>
        /// Forbiddens this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Forbidden()
        {
            return View();
        }
    }
}
