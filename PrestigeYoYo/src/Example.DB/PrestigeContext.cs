/// PrestigeContext.cs
/// Thomas Kempton 2012
///

namespace Prestige.DB
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using Prestige.EF;

    /// <summary>
    /// Prestige Database Context.
    /// </summary>
    public class PrestigeContext : BaseContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrestigeContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public PrestigeContext(string connectionString) : base(connectionString)
        {
            this.Mods = new List<DatabaseMod>();
            this.Mods.Add(new ModelWithIdMod());
        }

        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = false;

            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new ProductFlawConfiguration());
            modelBuilder.Configurations.Add(
                        new InspectionStationConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
