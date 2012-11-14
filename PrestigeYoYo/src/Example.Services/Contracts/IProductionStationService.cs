///
///
///

namespace Prestige.Services
{
    using System.Linq;
    using Prestige.DB.Models;

    public interface IProductionStationService
    {
        /// <summary>
        /// Adds the specified station.
        /// </summary>
        /// <param name="station">The station.</param>
        void Add(ProductionStation station);

        /// <summary>
        /// Updates the specified station.
        /// </summary>
        /// <param name="station">The station.</param>
        void Update(ProductionStation station);

        /// <summary>
        /// Deletes the specified station.
        /// </summary>
        /// <param name="station">The station.</param>
        void Delete(ProductionStation station);

        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns></returns>
        IQueryable<ProductionStation> List();
    }
}
