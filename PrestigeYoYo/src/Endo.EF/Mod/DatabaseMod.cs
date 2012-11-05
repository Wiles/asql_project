/// DatabaseMod.cs
/// Thomas Kempton 2012
///

namespace Prestige.EF
{
    using System.Data.Objects;

    /// <summary>
    /// Database Access Modifier Base.
    /// </summary>
    public abstract class DatabaseMod
    {
        /// <summary>
        /// PreSave step.
        /// </summary>
        /// <param name="objectContext">The object context.</param>
        public virtual void PreSave(ObjectContext objectContext) { }

        /// <summary>
        /// PostSave step.
        /// </summary>
        /// <param name="objectContext">The object context.</param>
        public virtual void PostSave(ObjectContext objectContext) { }
    }
}
