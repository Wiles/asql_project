///
///
///

namespace Prestige
{
    using System;
    using System.Text;
    using System.Web.Mvc;

    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Generates stylesheet tags.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="styles">The styles.</param>
        /// <returns></returns>
        public static MvcHtmlString StyleSheet(this UrlHelper url, params string[] styles)
        {
            if (styles == null)
            {
                throw new ArgumentNullException("styles");
            }

            StringBuilder sb = new StringBuilder();
            foreach (string css in styles)
            {
                sb.Append(string.Format("<link href='{0}' rel='stylesheet' type='text/css' />", url.Content(css)));
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// Generates script tags.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="scripts">The scripts.</param>
        /// <returns></returns>
        public static MvcHtmlString Script(this UrlHelper url, params string[] scripts)
        {
            if (scripts == null)
            {
                throw new ArgumentNullException("styles");
            }

            StringBuilder sb = new StringBuilder();
            foreach (string script in scripts)
            {
                sb.Append(string.Format("<script src='{0}' type='text/javascript'></script>", url.Content(script)));
            }

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}
