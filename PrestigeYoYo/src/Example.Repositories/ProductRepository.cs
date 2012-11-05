/// ProductRepository.cs
/// Thomas Kempton 2012
///

namespace Prestige.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using Prestige.EF;
    using Prestige.DB.Models;

    public class ProductRepository : RepositoryBase<Product>, IProductRepository, IRepository<Product>
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
            typeof(InspectionStation)
        };

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ProductRepository(DbContext context) : base(context)
        {
            propertiesToInclude = new List<string>();

            foreach (var property in typeof(Product).GetProperties())
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
                var query = this.Context.Set<Product>() as IQueryable<Product>;
                foreach (var property in propertiesToInclude)
                {
                    query = query.Include(property);
                }
                return query.Expression;
            }
        }
    }
}
