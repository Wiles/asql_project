/// Extension methods for the HtmlHelper
/// Codeora 2012
///

namespace Prestige
{
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using Prestige.ViewModels;

    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Creates the navigation bar.
        /// </summary>
        /// <param name="html">The Html Helper.</param>
        /// <returns>The navigation bar.</returns>
        public static MvcHtmlString NavigationBar(this HtmlHelper html)
        {
            var navigation = new NavButtonModel[]
            {
                new NavButtonModel("Reports", "Index", "Report"),
                new NavButtonModel("Scheduling", "Index", "Schedule"),
                new NavButtonModel("Management", "Index", "Management"),
                new NavButtonModel("Administration", "Index", "Admin")
            };

            var controller = html.ViewContext.Controller.ValueProvider
                                .GetValue("controller").RawValue.ToString();

            var sb = new StringBuilder("<ul id='menu'>");

            foreach (var nav in navigation)
            {
                if (string.Compare(controller, nav.Controller, true) == 0)
                {
                    sb.Append("<li class='selected'>");
                }
                else
                {
                    sb.Append("<li>");
                }

                sb.Append(
                        html.ActionLink(nav.Title, nav.Action, nav.Controller)
                                .ToHtmlString() + "</li>");
            }

            sb.Append("</ul>");
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}
