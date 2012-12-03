/// Role repository
/// Codeora 2012
///

namespace Prestige.Repositories
{
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using Prestige.DB.Models;
    using Prestige.EF;

    public class RoleRepository : RepositoryBase<Role>, IRoleRepository, IRepository<Role>
    {

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="RoleRepository" /> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public RoleRepository(DbContext context) : base(context)
        {
        }
    }
}
