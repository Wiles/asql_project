/// Product service contract
/// Codeora 2012
///

namespace Prestige.Services
{
    using System.Linq;
    using Prestige.DB.Models;

    /// <summary>
    /// Interface for managing Products.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Adds the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        void Add(Product product);

        /// <summary>
        /// Updates the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        void Update(Product product);

        /// <summary>
        /// Deletes the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        void Delete(Product product);

        /// <summary>
        /// Lists the products.
        /// </summary>
        /// <returns></returns>
        IQueryable<Product> List();
    }
}
