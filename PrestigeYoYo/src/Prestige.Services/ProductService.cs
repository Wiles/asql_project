///
///
///

namespace Prestige.Services
{
    using System;
    using System.Linq;
    using Prestige.DB.Models;
    using Prestige.Repositories;

    /// <summary>
    /// Service for managing Products.
    /// </summary>
    public class ProductService : IProductService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ProductService(IProductRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.Repository = repository;
        }

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        private IProductRepository Repository { get; set; }

        /// <summary>
        /// Adds the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void Add(Product product)
        {
            this.Repository.Add(product);
            this.Repository.SaveChanges();
        }

        /// <summary>
        /// Updates the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void Update(Product product)
        {
            this.Repository.SaveChanges();
        }

        /// <summary>
        /// Deletes the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void Delete(Product product)
        {
            this.Repository.Delete(product);
            this.Repository.SaveChanges();
        }

        /// <summary>
        /// Lists the products.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Product> List()
        {
            return this.Repository;
        }
    }
}
