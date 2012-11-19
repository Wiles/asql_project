/// DeletedMod.cs
/// Thomas Kempton 2012
///

namespace Prestige.EF
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Objects;
    using System.Linq;
    
    /// <summary>
    /// Manages Soft Deleting Entities
    /// </summary>
    public class DeletedMod : DatabaseMod
    {
        /// <summary>
        /// Modified entities' keys.
        /// </summary>
        private ISet<EntityKey> modifiedEntities;

        /// <summary>
        /// Gets the modified entities.
        /// </summary>
        /// <value>
        /// The modified entities.
        /// </value>
        private ISet<EntityKey> ModifiedEntities
        {
            get
            {
                return this.modifiedEntities ??
                    (this.modifiedEntities = new HashSet<EntityKey>());
            }
        }

        /// <summary>
        /// PreSave step.
        /// </summary>
        /// <param name="objectContext">The object context.</param>
        public override void PreSave(ObjectContext objectContext)
        {
            objectContext.DetectChanges();
            this.PreProcessEntities(objectContext);
            this.PreProcessRelationships(objectContext);
        }

        /// <summary>
        /// Processes the entities.
        /// </summary>
        /// <param name="objectContext">The object context.</param>
        private void PreProcessEntities(ObjectContext objectContext)
        {
            var entries = objectContext.ObjectStateManager
                .GetObjectStateEntries(EntityState.Deleted)
                .Where(e => !e.IsRelationship && e.Entity is IRecoverable);

            foreach (var entry in entries)
            {
                entry.ChangeState(EntityState.Modified);
                (entry.Entity as IRecoverable).IsDeleted = true;

                if (!this.ModifiedEntities.Contains(entry.EntityKey))
                {
                    var relatedEnds = entry.RelationshipManager.GetAllRelatedEnds();

                    foreach (var relatedEnd in relatedEnds)
                    {
                        relatedEnd.Load();
                    }

                    this.ModifiedEntities.Add(entry.EntityKey);
                }
            }
        }

        /// <summary>
        /// Processes the relationships.
        /// </summary>
        /// <param name="objectContext">The object context.</param>
        private void PreProcessRelationships(ObjectContext objectContext)
        {
            var entries = objectContext.ObjectStateManager
                .GetObjectStateEntries(EntityState.Deleted)
                .Where(e => e.IsRelationship);

            foreach (var entry in entries)
            {
                int modifiedFieldsCount = entry.OriginalValues.FieldCount;
                bool shouldRestore = true;
                bool isKnownEntity = false;

                for (int i = 0; i < modifiedFieldsCount; i++)
                {
                    var key = entry.OriginalValues[i] as EntityKey;
                    var entity = objectContext.GetObjectByKey(key) as IRecoverable;
                    var entityState = objectContext.ObjectStateManager.GetObjectStateEntry(key);

                    if (this.ModifiedEntities.Contains(key))
                    {
                        isKnownEntity = true;
                    }

                    if (entity == null && entityState.State == EntityState.Deleted)
                    {
                        shouldRestore = false;
                        break;
                    }
                }

                if (isKnownEntity && shouldRestore)
                {
                    entry.ChangeState(EntityState.Unchanged);
                }
            }
        }
    }
}
