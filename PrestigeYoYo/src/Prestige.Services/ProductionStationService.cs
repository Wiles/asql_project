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
    public class ProductionStationService : IProductionStationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ProductionStationService(IProductionStationRepository repository)
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
        private IProductionStationRepository Repository { get; set; }

        /// <summary>
        /// Adds the specified product.
        /// </summary>
        /// <param name="station">The station.</param>
        public void Add(ProductionStation station)
        {
            this.Repository.Add(station);
            this.Repository.SaveChanges();
        }

        /// <summary>
        /// Updates the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void Update(ProductionStation station)
        {
            this.Repository.SaveChanges();
        }

        /// <summary>
        /// Deletes the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void Delete(ProductionStation station)
        {
            this.Repository.Delete(station);
            this.Repository.SaveChanges();
        }

        /// <summary>
        /// Lists the products.
        /// </summary>
        /// <returns></returns>
        public IQueryable<ProductionStation> List()
        {
            return this.Repository;
        }
    }
}

