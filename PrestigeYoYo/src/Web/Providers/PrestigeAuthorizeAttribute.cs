///
///
///

namespace Prestige
{
    using System.Web;
    using System.Web.Mvc;

    public class PrestigeAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// When overridden, provides an entry
        /// point for custom authorization checks.
        /// </summary>
        /// <param name="httpContext">
        ///   The HTTP context, which encapsulates all HTTP-specific
        ///   information about an individual HTTP request.
        /// </param>
        /// <returns>
        /// true if the user is authorized; otherwise, false.
        /// </returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!base.AuthorizeCore(httpContext))
            {
                throw new HttpException(403, "You are not authorized to access this page.");
            }

            return true;
        }
    }
}