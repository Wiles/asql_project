using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Prestige.DB;
using Prestige.Repositories;
using Ninject.Modules;
using System.Data.Entity;
using Ninject.Web.Common;

namespace Prestige.Web
{
    /// <summary>
    /// Module for injecting repository classes.
    /// </summary>
    public class RepositoryNinjectModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IProductRepository>().To<ProductRepository>();
        }
    }
}