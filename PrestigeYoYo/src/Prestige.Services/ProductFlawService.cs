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
    public class ProductFlawService : IProductFlawService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ProductFlawService(IProductFlawRepository repository)
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
        private IProductFlawRepository Repository { get; set; }

        /// <summary>
        /// Adds the specified product.
        /// </summary>
        /// <param name="flaw">The flaw.</param>
        public void Add(ProductFlawType flaw)
        {
            this.Repository.Add(flaw);
            this.Repository.SaveChanges();
        }

        /// <summary>
        /// Updates the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void Update(ProductFlawType flaw)
        {
            this.Repository.SaveChanges();
        }

        /// <summary>
        /// Deletes the specified product.
        /// </summary>
        /// <param name="flaw">The flaw.</param>
        public void Delete(ProductFlawType flaw)
        {
            this.Repository.Delete(flaw);
            this.Repository.SaveChanges();
        }

        /// <summary>
        /// Lists the products.
        /// </summary>
        /// <returns>An IQueryable of Product Flaw Types.</returns>
        public IQueryable<ProductFlawType> List()
        {
            return this.Repository;
        }
    }
}
