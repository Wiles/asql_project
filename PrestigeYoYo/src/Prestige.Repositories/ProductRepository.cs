/// Product Repository
/// Codeora 2012
///

namespace Prestige.Repositories
{
    using System.Data.Entity;
    using Prestige.DB.Models;
    using Prestige.EF;

    /// <summary>
    /// Repository for accessing Products in the Database.
    /// </summary>
    public class ProductRepository : RepositoryBase<Product>, IProductRepository, IRepository<Product>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ProductRepository(DbContext context) : base(context)
        {
        }
    }
}
