///
///
///

namespace Prestige.ViewModels
{
    public class NavButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="NavButtonModel" /> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="action">The action.</param>
        /// <param name="controller">The controller.</param>
        public NavButtonModel(string title, string action, string controller)
        {
            this.Title = title;
            this.Action = action;
            this.Controller = controller;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; private set; }

        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        /// <value>
        /// The controller.
        /// </value>
        public string Controller { get; private set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action { get; private set; }
    }
}