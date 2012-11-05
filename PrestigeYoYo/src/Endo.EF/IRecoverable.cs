/// IRecoverable.cs
/// Thomas Kempton 2012
///

namespace Prestige.EF
{
    /// <summary>
    /// Interface for entities that are soft-deleted.
    /// </summary>
    public interface IRecoverable
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        bool IsDeleted { get; set; }
    }
}
