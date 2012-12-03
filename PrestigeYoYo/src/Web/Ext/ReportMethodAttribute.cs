/// Attribute for flagging a controller action as a report generation method
/// Codeora 2012
///

namespace Prestige
{
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ReportMethodAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ReportMethodAttribute" /> class.
        /// </summary>
        /// <param name="name">The report name.</param>
        public ReportMethodAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }
    }
}