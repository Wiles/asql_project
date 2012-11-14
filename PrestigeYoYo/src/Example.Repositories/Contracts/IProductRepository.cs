///
///
///

namespace Prestige.Repositories
{
    using Prestige.DB.Models;
    using Prestige.EF;

    /// <summary>
    /// Interface for managing Products in the Database.
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
    }
}
