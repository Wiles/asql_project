/// DebugInitalization.cs
/// Thomas Kempton 2012
///

namespace Prestige.DB
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using Prestige.DB.Models;

    /// <summary>
    /// Database Context for Prestige.
    /// </summary>
    public class DebugInitialization : DropCreateDatabaseIfModelChanges<PrestigeContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DebugInitialization"/> class.
        /// </summary>
        public DebugInitialization()
        {
            this.Products = new Collection<Product>()
            {
                new Product()
                {
                    SKU = "Y001-1",
                    Description = "Prestige Classic",
                    Colour = "Red"
                },
                new Product()
                {
                    SKU = "Y001-2",
                    Description = "Prestige Classic",
                    Colour = "Blue"
                },
                new Product()
                {
                    SKU = "Y001-3",
                    Description = "Prestige Classic",
                    Colour = "Green"
                },
                new Product()
                {
                    SKU = "Y002-0",
                    Description = "Clear Plastic",
                    Colour = "Clear"
                },
                new Product()
                {
                    SKU = "Y005-1",
                    Description = "Whistler",
                    Colour = "Red"
                },
                new Product()
                {
                    SKU = "Y005-2",
                    Description = "Whistler",
                    Colour = "Blue"
                },
                new Product()
                {
                    SKU = "Y005-3",
                    Description = "Whistler",
                    Colour = "Green"
                }
            };

            var moldInspection = new InspectionStation()
                {
                    StationNumber = 1,
                    Description = "Mold Inspection"
                };

            var paintInspection = new InspectionStation()
                {
                    StationNumber = 2,
                    Description = "Paint Inspection"
                };

            var assemblyInspection = new InspectionStation()
                {
                    StationNumber = 3,
                    Description = "Assembly Inspection"
                };

            this.Stations = new Collection<InspectionStation>()
            {
                moldInspection,
                paintInspection,
                assemblyInspection
            };

            this.Flaws = new Collection<ProductFlawType>()
            {
                new ProductFlawType()
                {
                    InspectionLocation = moldInspection,
                    Decision = "Reject",
                    Reason = "Inconsistent thickness"
                },
                new ProductFlawType()
                {
                    InspectionLocation = moldInspection,
                    Decision = "Reject",
                    Reason = "Pitting"
                },
                new ProductFlawType()
                {
                    InspectionLocation = moldInspection,
                    Decision = "Reject",
                    Reason = "Warping"
                },
                new ProductFlawType()
                {
                    InspectionLocation = paintInspection,
                    Decision = "Reject",
                    Reason = "Primer defect"
                },
                new ProductFlawType()
                {
                    InspectionLocation = paintInspection,
                    Decision = "Rework",
                    Reason = "Drip mark"
                },
                new ProductFlawType()
                {
                    InspectionLocation = paintInspection,
                    Decision = "Rework",
                    Reason = "Final coat flaw"
                },
                new ProductFlawType()
                {
                    InspectionLocation = assemblyInspection,
                    Decision = "Reject",
                    Reason = "Broken shell"
                },
                new ProductFlawType()
                {
                    InspectionLocation = assemblyInspection,
                    Decision = "Rework",
                    Reason = "Broken Axle"
                },
                new ProductFlawType()
                {
                    InspectionLocation = assemblyInspection,
                    Decision = "Rework",
                    Reason = "Tangled string"
                },
                new ProductFlawType()
                {
                    InspectionLocation = assemblyInspection,
                    Decision = "Rework",
                    Reason = "Final coat flaw"
                }
            };
        }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        private ICollection<Product> Products { get; set; }

        private ICollection<InspectionStation> Stations { get; set; }

        private ICollection<ProductFlawType> Flaws { get; set; }

        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(PrestigeContext context)
        {
            Seed(context, this.Products);
            Seed(context, this.Stations);
        }

        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context">The context.</param>
        /// <param name="entities">The entities.</param>
        private void Seed<T>(PrestigeContext context, IEnumerable<T> entities) where T : class
        {
            var set = context.Set<T>();

            foreach (var entity in entities)
            {
                set.Add(entity);
            }
        }
    }
}
