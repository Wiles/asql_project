///
///
///

namespace Prestige.Services
{
    using System;
    using System.Linq;
    using Prestige.DB.Models;
    using Prestige.Repositories;

    public class ProductionStageService : IProductionStageService
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ProductionStageService" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ProductionStageService(IProductionStageRepository repository)
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
        private IProductionStageRepository Repository { get; set; }

        /// <summary>
        /// Adds the specified product.
        /// </summary>
        /// <param name="stage">The stage.</param>
        public void Add(ProductionStage stage)
        {
            this.Repository.Add(stage);
            this.Repository.SaveChanges();
        }

        /// <summary>
        /// Updates the specified product.
        /// </summary>
        /// <param name="stage">The stage.</param>
        public void Update(ProductionStage stage)
        {
            this.Repository.SaveChanges();
        }

        /// <summary>
        /// Deletes the specified product.
        /// </summary>
        /// <param name="stage">The stage.</param>
        public void Delete(ProductionStage stage)
        {
            this.Repository.Delete(stage);
            this.Repository.SaveChanges();
        }

        /// <summary>
        /// Lists the products.
        /// </summary>
        /// <returns>An IQueryable of Production Stage Results.</returns>
        public IQueryable<ProductionStage> List()
        {
            return this.Repository;
        }
    }
}
