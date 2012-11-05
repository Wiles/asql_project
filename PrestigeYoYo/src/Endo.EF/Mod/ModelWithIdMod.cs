/// ModelWithIdMod.cs
/// Thomas Kempton 2012
///

namespace Prestige.EF
{
    using System;
    using System.Data;
    using System.Data.Objects;
    using System.Linq;

    /// <summary>
    /// Assigns Ids to models.
    /// </summary>
    public class ModelWithIdMod : DatabaseMod
    {
        /// <summary>
        /// PreSave step.
        /// </summary>
        /// <param name="objectContext">The object context.</param>
        public override void PreSave(ObjectContext objectContext)
        {
            var entries = objectContext.ObjectStateManager
                .GetObjectStateEntries(EntityState.Added)
                .Where(e => !e.IsRelationship && e.Entity is IModelBase);

            foreach (var entry in entries)
            {
                var entity = entry.Entity as IModelBase;

                if (entity.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                }
            }
        }
    }
}
