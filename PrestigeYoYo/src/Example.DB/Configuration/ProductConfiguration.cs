using System.Data.Entity.ModelConfiguration;
using Prestige.DB.Models;

namespace Prestige.DB
{
    class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
        }
    }
}
