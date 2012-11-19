///
///
///

namespace Prestige.Services
{
    using System.Linq;
    using Prestige.DB.Models;

    public interface IProductionEntryService
    {
        void Add(ProductionEntry product);

        void Delete(ProductionEntry product);

        IQueryable<ProductionEntry> List();

        void Update(ProductionEntry product);
    }
}
