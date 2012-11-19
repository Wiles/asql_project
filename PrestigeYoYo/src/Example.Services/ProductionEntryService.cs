///
///
///

namespace Prestige.Services
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Prestige.DB.Models;
    using Prestige.Repositories;

    public class ProductionEntryService : IProductionEntryService
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ProductMadeService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ProductionEntryService(IProductionEntryRepository repository)
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
        private IProductionEntryRepository Repository { get; set; }

        /// <summary>
        /// Adds the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void Add(ProductionEntry product)
        {
            this.Repository.Add(product);
            this.Repository.SaveChanges();
        }

        /// <summary>
        /// Updates the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void Update(ProductionEntry product)
        {
            this.Repository.SaveChanges();
        }

        /// <summary>
        /// Deletes the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void Delete(ProductionEntry product)
        {
            this.Repository.Delete(product);
            this.Repository.SaveChanges();
        }

        /// <summary>
        /// Lists the products.
        /// </summary>
        /// <returns>A list of production entries.</returns>
        public IQueryable<ProductionEntry> List()
        {
            // hack to load extremities
            return this.Repository.Include("Stages")
                                  .Include("Stages.Station")
                                  .Include("Stages.ProductFlaw")
                                  .Where(p => true);
        }
    }
}
