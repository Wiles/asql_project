///
///
///

namespace Prestige.Repositories
{
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using Prestige.DB.Models;
    using Prestige.EF;

    public class UserRepository : RepositoryBase<User>, IUserRepository, IRepository<User>
    {

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="UserRepository" /> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public UserRepository(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets the expression.
        /// </summary>
        Expression IQueryable.Expression
        {
            get
            {
                var query = this.Context.Set<User>() as IQueryable<User>;
                query = query.Include("Roles");
                return query.Expression;
            }
        }
    }
}
