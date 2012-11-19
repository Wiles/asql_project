/// ProductFlawRepository.cs
/// Thomas Kempton 2012
///

namespace Prestige.Repositories
{
    using System.Data.Entity;
    using Prestige.DB.Models;
    using Prestige.EF;

    /// <summary>
    /// Repository for accessing Products in the Database.
    /// </summary>
    public class ProductFlawRepository : RepositoryBase<ProductFlawType>, IProductFlawRepository, IRepository<ProductFlawType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ProductFlawRepository(DbContext context)
            : base(context)
        {
        }
    }
}
