/// Model for viewing defects in a grid
/// Codeora 2012
///

namespace Prestige.ViewModels
{
    using System;

    public class DefectListModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public Guid Guid { get; set; }

        /// <summary>
        /// Gets or sets the decision.
        /// </summary>
        /// <value>
        /// The decision.
        /// </value>
        public string Decision { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>
        /// The reason.
        /// </value>
        public string Reason { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Identifier { get; set; }
    }
}