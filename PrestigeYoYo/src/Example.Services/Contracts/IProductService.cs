using System;
using System.Collections.Generic;
using System.Linq;
using Prestige.DB.Models;

namespace Prestige.Services
{
    /// <summary>
    /// Interface for managing persons.
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
