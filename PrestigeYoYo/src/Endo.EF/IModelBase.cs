/// IModelBase.cs
/// Thomas Kempton 2012
///

namespace Prestige.EF
{
    using System;

    /// <summary>
    /// Interface for models with Ids.
    /// </summary>
    public interface IModelBase
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        Guid Id { get; set; }
    }
}
