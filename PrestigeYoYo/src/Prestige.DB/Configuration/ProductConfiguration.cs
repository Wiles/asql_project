/// Product table configuration.
/// Codeora 2012
///

namespace Prestige.DB
{
    using System.Data.Entity.ModelConfiguration;
    using Prestige.DB.Models;

    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
        }
    }
}
