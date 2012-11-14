///
///
///

namespace Prestige.Services
{
    using System.Linq;
    using Prestige.DB.Models;

    /// <summary>
    /// Interface for managing Products.
    /// </summary>
    public interface IProductFlawService
    {
        /// <summary>
        /// Adds the specified flaw.
        /// </summary>
        /// <param name="product">The product.</param>
        void Add(ProductFlawType flaw);

        /// <summary>
        /// Updates the specified flaw.
        /// </summary>
        /// <param name="product">The product.</param>
        void Update(ProductFlawType flaw);

        /// <summary>
        /// Deletes the specified flaw.
        /// </summary>
        /// <param name="product">The product.</param>
        void Delete(ProductFlawType flaw);

        /// <summary>
        /// Lists the flaws.
        /// </summary>
        /// <returns></returns>
        IQueryable<ProductFlawType> List();
    }
}
