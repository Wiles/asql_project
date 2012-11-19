///
///
///

namespace Prestige.Services
{
    using System.Linq;
    using Prestige.DB.Models;

    public interface IProductionStageService
    {
        /// <summary>
        /// Adds the specified product.
        /// </summary>
        /// <param name="stage">The stage.</param>
        void Add(ProductionStage stage);

        /// <summary>
        /// Updates the specified product.
        /// </summary>
        /// <param name="stage">The stage.</param>
        void Update(ProductionStage stage);

        /// <summary>
        /// Deletes the specified product.
        /// </summary>
        /// <param name="stage">The stage.</param>
        void Delete(ProductionStage stage);

        /// <summary>
        /// Lists the products.
        /// </summary>
        /// <returns>An IQueryable of Production Stage Results.</returns>
        IQueryable<ProductionStage> List();
    }
}
