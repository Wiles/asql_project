///
///
///

namespace Prestige.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Web.Security;
    using Prestige.Services;

    public class AccountController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public AccountController(IUserService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }

            this.Service = service;
        }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        private IUserService Service { get; set; }

        /// <summary>
        /// Requests user log on.
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOn()
        {
            return View();
        }

        /// <summary>
        /// Logs the user on.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="remember">if set to <c>true</c> [remember].</param>
        /// <returns>Redirect.</returns>
        [HttpPost]
        public ActionResult LogOn(
                string username,
                string password,
                string remember)
        {
            if (this.Service.Authenticate(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, remember == "on");
                return Redirect(Request.QueryString["ReturnUrl"] ?? "/");
            }

            return View((object)username);
        }

        /// <summary>
        /// Logs the user off.
        /// </summary>
        /// <returns>Redirect to log on.</returns>
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOn");
        }
    }
}
