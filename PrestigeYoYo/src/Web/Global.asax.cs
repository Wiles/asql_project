/// Web Application Initialization
/// Codeora 2012
///

namespace Prestige
{
    using System;
    using System.Data.Entity;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.Security;

    using Ninject;
    using Ninject.Web.Common;
    using Prestige.DB;
    using Prestige.Web.Controllers;

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
            // set the database initializer
#if DEBUG
            Database.SetInitializer(new DebugInitialization());
#else
            Database.SetInitializer(new ReleaseInitialization());
#endif

            // register routes
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

            // extract current action and controller from current route data
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

            // retrieve the error
            var ex = Server.GetLastError();

            // initialize an error controller to handle the exception
            var controller = new ErrorController();
            var routeData = new RouteData();

            // extract the error code and message
            var errorCode = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;
            var message = ex is HttpException ? ((HttpException)ex).Message : null;

            // clean up the exception
            httpContext.ClearError();
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = errorCode;
            httpContext.Response.TrySkipIisCustomErrors = true;

            // set the route data
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = "Index";
            routeData.Values["code"] = errorCode;
            routeData.Values["message"] = message;

            // reroute to the error controller
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
            // load a Ninject kernel
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            // inject into the roles provider
            kernel.Inject(Roles.Provider);

            return kernel;
        }
    }
}