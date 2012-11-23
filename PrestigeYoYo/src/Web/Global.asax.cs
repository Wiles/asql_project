using System.Data.Entity;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Ninject;
using Ninject.Web.Common;
using Prestige.DB;
using System;
using System.Web;
using Prestige.Web.Controllers;

namespace Prestige
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    /// <summary>
    /// Class for managing your application.
    /// </summary>
    public class MvcApplication : NinjectHttpApplication
    {
        /// <summary>
        /// Registers the global filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Report", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        /// <summary>
        /// Called when the application is started.
        /// </summary>
        protected override void OnApplicationStarted()
        {
#if DEBUG
            Database.SetInitializer(new DebugInitialization());
#else
            Database.SetInitializer(new ReleaseInitialization());
#endif

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// Handles the Error event of the Application.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///   The <see cref="EventArgs" /> instance containing the event data.
        /// </param>
        public void Application_Error(object sender, EventArgs e)
        {
            var httpContext = ((MvcApplication)sender).Context;

            var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));
            var currentController = " ";
            var currentAction = " ";

            if (currentRouteData != null)
            {
                if (currentRouteData.Values["controller"] != null && !String.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
                {
                    currentController = currentRouteData.Values["controller"].ToString();
                }

                if (currentRouteData.Values["action"] != null && !String.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
                {
                    currentAction = currentRouteData.Values["action"].ToString();
                }
            }

            var ex = Server.GetLastError();

            var controller = new ErrorController();
            var routeData = new RouteData();
            var action = "Index";

            var errorCode = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;
            var message = ex is HttpException ? ((HttpException)ex).Message : null;

            httpContext.ClearError();
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = errorCode;
            httpContext.Response.TrySkipIisCustomErrors = true;
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = action;
            routeData.Values["code"] = errorCode;
            routeData.Values["message"] = message;

            controller.ViewData.Model = new HandleErrorInfo(ex, currentController, currentAction);
            ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>
        /// The created kernel.
        /// </returns>
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Inject(Roles.Provider);
            return kernel;
        }
    }
}