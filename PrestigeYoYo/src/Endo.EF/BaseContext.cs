/// BaseContext.cs
/// Thomas Kempton 2012
///

namespace Prestige.EF
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;

    /// <summary>
    /// Database Context Base Class
    /// </summary>
    public abstract class BaseContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public BaseContext(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Gets or sets the mods.
        /// </summary>
        /// <value>
        /// The mods.
        /// </value>
        protected ICollection<DatabaseMod> Mods { get; set; }

        /// <summary>
        /// Gets the object context.
        /// </summary>
        /// <value>
        /// The object context.
        /// </value>
        protected ObjectContext ObjectContext
        {
            get
            {
                return (this as IObjectContextAdapter).ObjectContext;
            }
        }

        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void  OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            int ret = 0;

            foreach (var mod in this.Mods)
            {
                mod.PreSave(this.ObjectContext);
            }

            ret = base.SaveChanges();

            foreach (var mod in this.Mods)
            {
                mod.PostSave(this.ObjectContext);
            }

            return ret;
        }
    }
}
