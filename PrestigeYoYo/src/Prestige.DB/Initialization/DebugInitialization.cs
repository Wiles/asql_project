/// DebugInitalization.cs
/// Thomas Kempton 2012
///

namespace Prestige.DB
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Linq;
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
            var user = new Role() { Name = "User" };
            var admin = new Role() { Name = "Administrator" };

            this.Roles = new Collection<Role>() { user, admin };

            this.Users = new Collection<User>()
            {
                new User()
                {
                    UserName = "root",
                    Password = "63a9f0ea7bb98050796b649e85481845",
                    Roles = new Collection<Role>() { admin }
                },
                new User()
                {
                    UserName = "noob",
                    Password = "9cb4afde731e9eadcda4506ef7c65fa2",
                    Roles = new Collection<Role>() { user }
                }
            };

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
                    Identifier = "MOLD",
                    StationType = "Production"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 1 queue",
                    Identifier = "QUEUE_INSPECTION_1",
                    StationType = "Queue"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 1",
                    Identifier = "INSPECTION_1",
                    StationType = "Inspection"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 1 scrap",
                    Identifier = "INSPECTION_1_SCRAP",
                    StationType = "Scrap"
                },
                new ProductionStation()
                {
                    Description = "Paint process queue",
                    Identifier = "QUEUE_PAINT",
                    StationType = "Queue"
                },
                new ProductionStation()
                {
                    Description = "Paint",
                    Identifier = "PAINT",
                    StationType = "Production"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 2 queue",
                    Identifier = "QUEUE_INSPECTION_2",
                    StationType = "Queue"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 2",
                    Identifier = "INSPECTION_2",
                    StationType = "Inspection"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 2 rework",
                    Identifier = "INSPECTION_2_REWORK",
                    StationType = "Rework"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 2 scrap",
                    Identifier = "INSPECTION_2_SCRAP",
                    StationType = "Scrap"
                },
                new ProductionStation()
                {
                    Description = "Assembly process queue",
                    Identifier = "QUEUE_ASSEMBLY",
                    StationType = "Queue"
                },
                new ProductionStation()
                {
                    Description = "Assembly process",
                    Identifier = "ASSEMBLY",
                    StationType = "Production"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 3 queue",
                    Identifier = "QUEUE_INSPECTION_3",
                    StationType = "Queue"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 3",
                    Identifier = "INSPECTION_3",
                    StationType = "Inspection"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 3 rework",
                    Identifier = "INSPECTION_3_REWORK",
                    StationType = "Rework"
                },
                new ProductionStation()
                {
                    Description = "Inspection station 3 scrap",
                    Identifier = "INSPECTION_3_SCRAP",
                    StationType = "Scrap"
                },
                new ProductionStation()
                {
                    Description = "Packaging process",
                    Identifier = "PACKAGE",
                    StationType = "Complete"
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
                }
            };

            var days = new string[]
            {
                "MONDAY",
                "TUESDAY",
                "WEDNESDAY",
                "THURSDAY",
                "FRIDAY",
                "SATURDAY",
                "SUNDAY"
            };

            this.Schedule = new Collection<Schedule>();
            var random = new Random();
            var products = this.Products.ToArray();

            for (int d = 0; d < 7; d++)
            {
                for (int h = 0; h < 24; h +=4 )
                {
                    var product = products[random.Next(products.Length)];
                    for (int i = 0; i < 4; i++)
                    {
                        this.Schedule.Add(new Schedule()
                        {
                            Day = days[d],
                            Hour = (h + i),
                            Product = product
                        });
                    }
                }
            }
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
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        private ICollection<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        private ICollection<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the schedule.
        /// </summary>
        /// <value>
        /// The schedule.
        /// </value>
        private ICollection<Schedule> Schedule { get; set; }

        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(PrestigeContext context)
        {
            Seed(context, this.Roles);
            Seed(context, this.Users);
            Seed(context, this.Products);
            Seed(context, this.Stations);
            Seed(context, this.Flaws);
            Seed(context, this.Schedule);
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
