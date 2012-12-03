/// Schedule repository
/// Codeora 2012
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

    public class ScheduleRepository : RepositoryBase<Schedule>, IScheduleRepository, IRepository<Schedule>
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
            typeof(Product)
        };

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ScheduleRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ScheduleRepository(DbContext context)
            : base(context)
        {
            propertiesToInclude = new List<string>();

            foreach (var property in typeof(Schedule).GetProperties())
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
                var query = this.Context.Set<Schedule>() as IQueryable<Schedule>;
                foreach (var property in propertiesToInclude)
                {
                    query = query.Include(property);
                }
                return query.Expression;
            }
        }

    }
}
