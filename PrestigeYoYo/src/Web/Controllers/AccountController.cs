﻿/// Account controller
/// Codeora 2012
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
            // check authentication
            if (this.Service.Authenticate(username, password))
            {
                // redirect to target, or /
                FormsAuthentication.SetAuthCookie(username, remember == "on");
                return Redirect(
                            Request.QueryString["ReturnUrl"] ??
                                        Url.Action("Index", "Report"));
            }

            // bad username/password, return with error
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
            // eat the cookie
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOn");
        }
    }
}
