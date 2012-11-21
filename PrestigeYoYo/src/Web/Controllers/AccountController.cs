///
///
///

namespace Prestige.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Web.Security;
    using Prestige.Services;
    using Prestige.Web.ViewModels;

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
        /// <param name="error">The error message.</param>
        /// <returns>User log on view.</returns>
        public ActionResult LogOn(
                string error)
        {
            var model = new LoginViewModel()
            {
                Error = error
            };

            return View(model);
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
                this.Session.Add("username", username);
                return Redirect(Request.QueryString["ReturnUrl"] ?? "/Report/Index");
            }

            var model = new LoginViewModel();
            model.UserName = username;
            model.RememberMe = remember == "on";
            model.Error = "Invalid username/password.";
            return View(model);
        }

        /// <summary>
        /// Logs the user off.
        /// </summary>
        /// <returns>Redirect to log on.</returns>
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            this.Session.Remove("username");
            return RedirectToAction("LogOn");
        }
    }
}
