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
    /// Debug Database Initialization for Prestige.
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

            this.Stations = new Collection<ProductionStation>()
            {
                new ProductionStation()
                {
                    Description = "Mold process",
                    Identifier = "MOLD"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 1 queue",
                    Identifier = "QUEUE_INSPECTION_1"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 1",
                    Identifier = "INSPECTION_1"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 1 scrap",
                    Identifier = "INSPECTION_1_SCRAP"
                },
                new ProductionStation()
                {
                    Description = "Paint process queue",
                    Identifier = "QUEUE_PAINT"
                },
                new ProductionStation()
                {
                    Description = "Paint",
                    Identifier = "PAINT"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 2 queue",
                    Identifier = "QUEUE_INSPECTION_2"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 2",
                    Identifier = "INSPECTION_2"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 2 rework",
                    Identifier = "INSPECTION_2_REWORK"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 2 scrap",
                    Identifier = "INSPECTION_2_SCRAP"
                },
                new ProductionStation()
                {
                    Description = "Assembly process queue",
                    Identifier = "QUEUE_ASSEMBLY"
                },
                new ProductionStation()
                {
                    Description = "Assembly process",
                    Identifier = "ASSEMBLY"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 3 queue",
                    Identifier = "QUEUE_INSPECTION_3"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 3",
                    Identifier = "INSPECTION_3"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 3 rework",
                    Identifier = "INSPECTION_3_REWORK"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 3 scrap",
                    Identifier = "INSPECTION_3_SCRAP"
                },
                new ProductionStation()
                {
                    Description = "Packaging process",
                    Identifier = "PACKAGE"
                }
            };

            this.Flaws = new Collection<ProductFlawType>()
            {
                new ProductFlawType()
                {
                    Decision = "Reject",
                    Reason = "Inconsistent thickness",
                    Identifier = "INCONSISTENT_THICKNESS"
                },
                new ProductFlawType()
                {
                    Decision = "Reject",
                    Reason = "Pitting",
                    Identifier = "PITTING"
                },
                new ProductFlawType()
                {
                    Decision = "Reject",
                    Reason = "Warping",
                    Identifier = "WARPING"
                },
                new ProductFlawType()
                {
                    Decision = "Reject",
                    Reason = "Primer defect",
                    Identifier = "PRIMER_DEFECT"
                },
                new ProductFlawType()
                {
                    Decision = "Rework",
                    Reason = "Drip mark",
                    Identifier = "DRIP_MARK"
                },
                new ProductFlawType()
                {
                    Decision = "Rework",
                    Reason = "Final coat flaw",
                    Identifier = "FINAL_COAT_FLAW"
                },
                new ProductFlawType()
                {
                    Decision = "Reject",
                    Reason = "Broken shell",
                    Identifier = "BROKEN_SHELL"
                },
                new ProductFlawType()
                {
                    Decision = "Rework",
                    Reason = "Broken Axle",
                    Identifier = "BROKEN_AXLE"
                },
                new ProductFlawType()
                {
                    Decision = "Rework",
                    Reason = "Tangled string",
                    Identifier = "TANGLED_STRING"
                },
                new ProductFlawType()
                {
                    Decision = "Rework",
                    Reason = "Final coat flaw",
                    Identifier = "FINAL_COAT_FLAW"
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

        /// <summary>
        /// Gets or sets the stations.
        /// </summary>
        /// <value>
        /// The stations.
        /// </value>
        private ICollection<ProductionStation> Stations { get; set; }

        /// <summary>
        /// Gets or sets the flaws.
        /// </summary>
        /// <value>
        /// The flaws.
        /// </value>
        private ICollection<ProductFlawType> Flaws { get; set; }

        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(PrestigeContext context)
        {
            Seed(context, this.Products);
            Seed(context, this.Stations);
            Seed(context, this.Flaws);
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
