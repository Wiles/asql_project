///
///
///

namespace Prestige.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using Prestige.DB.Models;
    using Prestige.EF;

    public class ProductionEntryRepository : RepositoryBase<ProductionEntry>, IProductionEntryRepository, IRepository<ProductionEntry>
    {
        /// <summary>
        /// Properties to include.
        /// </summary>
        private ICollection<string> propertiesToInclude;

        /// <summary>
        /// Types to include
        /// </summary>
        private IEnumerable<Type> typesToInclude = new List<Type>()
        {
            typeof(Product),
            typeof(ICollection<ProductionStage>),
            typeof(ProductFlawType)
        };

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ProductionEntryRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ProductionEntryRepository(DbContext context)
            : base(context)
        {
            propertiesToInclude = new List<string>()
                {
                    "Stages.ProductFlaw",
                    "Stages.Station"
                };

            foreach (var property in typeof(ProductionEntry).GetProperties())
            {
                foreach (var type in typesToInclude)
                {
                    if (property.PropertyType.IsAssignableFrom(type))
                    {
                        propertiesToInclude.Add(property.Name);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the expression.
        /// </summary>
        Expression IQueryable.Expression
        {
            get
            {
                var query =
                    this.Context.Set<ProductionEntry>() as IQueryable<ProductionEntry>;
                foreach (var property in propertiesToInclude)
                {
                    query = query.Include(property);
                }
                return query.Expression;
            }
        }
    }
}
